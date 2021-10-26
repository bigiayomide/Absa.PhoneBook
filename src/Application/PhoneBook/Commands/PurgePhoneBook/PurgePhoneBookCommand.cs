using System.Threading;
using System.Threading.Tasks;
using Absa.Application.Common.Interfaces;
using Absa.Application.Common.Security;
using MediatR;

namespace Application.PhoneBook.Commands.PurgePhoneBook
{

    [Authorize(Roles = "Administrator")]
    [Authorize(Policy = "CanPurge")]
    public class PurgePhoneBookCommand : IRequest
    {
    }

    public class PurgePhoneBookCommandHandler : IRequestHandler<PurgePhoneBookCommand>
    {
        private readonly IApplicationDbContext _context;

        public PurgePhoneBookCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(PurgePhoneBookCommand request, CancellationToken cancellationToken)
        {
            _context.PhoneBooks.RemoveRange(_context.PhoneBooks);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}