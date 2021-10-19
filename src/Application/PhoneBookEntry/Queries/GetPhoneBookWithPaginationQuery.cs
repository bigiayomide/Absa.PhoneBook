using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Absa.Application.Common.Interfaces;
using Absa.Application.Common.Mappings;
using Absa.Application.Common.Models;
using Absa.Application.PhoneBookEntry;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Absa.Application.PhoneBook.Queries
{
    public class GetPhoneBookEntryWithPaginationQuery : IRequest<PaginatedList<PhoneBookEntryDto>>
    {
        public int ListId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetPhoneBookEntryWithPaginationQueryHandler : IRequestHandler<GetPhoneBookEntryWithPaginationQuery, PaginatedList<PhoneBookEntryDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPhoneBookEntryWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<PhoneBookEntryDto>> Handle(GetPhoneBookEntryWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.PhoneBookEntries
                // .Where(x => x.ListId == request.ListId)
                .OrderBy(x => x.Name)
                .ProjectTo<PhoneBookEntryDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}