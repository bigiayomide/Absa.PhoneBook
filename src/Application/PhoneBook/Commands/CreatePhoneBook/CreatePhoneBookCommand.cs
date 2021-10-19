using System.Threading;
using System.Threading.Tasks;
using Absa.Application.Common.Interfaces;
using MediatR;
using Absa.Domain.Entities;

namespace Absa.Application.PhoneBook.Commands.CreatePhoneBook
{

    public class CreatePhoneBookCommand : IRequest<int>
    {
        public string Name { get; set; }
    }

    public class CreatePhoneBookCommandHandler : IRequestHandler<CreatePhoneBookCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreatePhoneBookCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreatePhoneBookCommand request, CancellationToken cancellationToken)
        {
            var entity = new Absa.Domain.Entities.PhoneBook
            {
                Name = request.Name
            };

            _context.PhoneBooks.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }

}