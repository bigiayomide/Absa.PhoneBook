using Absa.Application.Common.Exceptions;
using Absa.Application.Common.Interfaces;
using Absa.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Absa.Application.TodoItems.Commands.DeletePhoneBookEntry
{
    public class DeletePhoneBookEntryCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeletePhoneBookEntryCommandHandler : IRequestHandler<DeletePhoneBookEntryCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeletePhoneBookEntryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeletePhoneBookEntryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.PhoneBookEntries.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.PhoneBookEntry), request.Id);
            }

            _context.PhoneBookEntries.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
