using Absa.Application.Common.Interfaces;
using Absa.Domain.Entities;
using Absa.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Absa.Application.TodoItems.Commands.CreatePhoneBookEntry
{
    public class CreatePhoneBookEntryCommand : IRequest<int>
    {
        public int PhoneBookId { get; set; }
        public string Number { get; set; }

        public string Name { get; set; }
    }

    public class CreatePhoneBookEntryCommandHandler : IRequestHandler<CreatePhoneBookEntryCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreatePhoneBookEntryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreatePhoneBookEntryCommand request, CancellationToken cancellationToken)
        {
            var entity = new Absa.Domain.Entities.PhoneBookEntry
            {
                Name = request.Name,
                Number = request.Number
            };

            _context.PhoneBookEntries.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
