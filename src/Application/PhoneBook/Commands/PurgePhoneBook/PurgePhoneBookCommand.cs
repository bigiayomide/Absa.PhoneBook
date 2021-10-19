using System.Threading;
using System.Threading.Tasks;
using Absa.Application.Common.Interfaces;
using Absa.Application.Common.Security;
using Absa.Application.TodoLists.Commands.PurgeTodoLists;
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
            _context.TodoLists.RemoveRange(_context.TodoLists);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}