using Absa.Application.Common.Exceptions;
using Absa.Application.Common.Interfaces;
using Absa.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Absa.Application.PhoneBookEntry.Commands.UpdatePhoneBookEntry
{
    public class UpdatePhoneBookEntryCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }
    }

    public class UpdatePhoneBookEntryCommandHandler : IRequestHandler<UpdatePhoneBookEntryCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdatePhoneBookEntryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdatePhoneBookEntryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.PhoneBookEntries.FindAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoItem), request.Id);
            }

            entity.Name = request.Name;
            entity.Number = request.Number;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
