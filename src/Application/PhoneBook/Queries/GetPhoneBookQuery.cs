using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Absa.Application.Common.Interfaces;
using Absa.Application.PhoneBook;
using Absa.Application.PhoneBookEntry;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.PhoneBook.Queries
{
    public class GetPhoneBookQuery : IRequest<PhoneBookVm>
    {
    }

    public class GetTodosQueryHandler : IRequestHandler<GetPhoneBookQuery, PhoneBookVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTodosQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PhoneBookVm> Handle(GetPhoneBookQuery request, CancellationToken cancellationToken)
        {
            return new PhoneBookVm
            {

                PhoneBooks = await _context.PhoneBooks
                    .AsNoTracking()
                    .Select(x => new PhoneBookDto()
                    {
                        Name = x.Name,
                        Id = x.Id,
                        PhoneBookEntriesCount = x.PhoneBookEntries.Count,
                        PhoneBookEntries = _mapper.Map<List<PhoneBookEntryDto>>(x.PhoneBookEntries)
                    })
                    .OrderBy(t => t.Name)
                    .ToListAsync(cancellationToken)

            };

        }
    }
}