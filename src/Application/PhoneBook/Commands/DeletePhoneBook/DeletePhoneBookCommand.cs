using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Absa.Application.Common.Exceptions;
using Absa.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.PhoneBook.Commands.DeletePhoneBook
{
    public class DeletePhoneBookCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeletePhoneBookCommandHandler : IRequestHandler<DeletePhoneBookCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeletePhoneBookCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeletePhoneBookCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.PhoneBooks
                .Where(l => l.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(PhoneBook), request.Id);
            }

            _context.PhoneBooks.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}