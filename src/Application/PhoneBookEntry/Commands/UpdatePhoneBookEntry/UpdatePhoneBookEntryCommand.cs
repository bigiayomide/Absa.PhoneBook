using Absa.Application.Common.Exceptions;
using Absa.Application.Common.Interfaces;
using Absa.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Absa.Application.PhoneBookEntry.Commands.UpdatePhoneBookEntry
{
    public class UpdatePhoneBookEntryCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int PhoneBookId { get; set; }

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
            var canFindPhoneBook = await _context.PhoneBooks.Where(x => x.Id == request.PhoneBookId).AnyAsync(cancellationToken: cancellationToken);
            var entity = await _context.PhoneBookEntries.FindAsync(request.Id);

            if (!canFindPhoneBook)
                throw new NotFoundException("No Id is Associated to that PhoneBook");

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoItem), request.Id);
            }

            entity.Name = request.Name;
            entity.Number = request.Number;
            entity.PhoneBookId = request.PhoneBookId;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
