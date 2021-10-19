using System.Threading;
using System.Threading.Tasks;
using Absa.Application.Common.Exceptions;
using Absa.Application.Common.Interfaces;
using MediatR;

namespace Application.PhoneBook.Commands.UpdatePhoneBook
{


    public class UpdatePhoneBookCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class UpdateTodoListCommandHandler : IRequestHandler<UpdatePhoneBookCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTodoListCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdatePhoneBookCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.PhoneBooks.FindAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(PhoneBook), request.Id);
            }

            entity.Name = request.Name;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}