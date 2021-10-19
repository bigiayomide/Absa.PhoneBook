using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Absa.Application.Common.Interfaces;
using Absa.Application.PhoneBook;
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
                    .ProjectTo<PhoneBookDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.Name)
                    .ToListAsync(cancellationToken)
            };

        }
    }
}