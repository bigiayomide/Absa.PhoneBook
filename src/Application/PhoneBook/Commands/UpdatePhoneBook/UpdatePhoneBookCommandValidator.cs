using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Absa.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.PhoneBook.Commands.UpdatePhoneBook
{
    public class UpdatePhoneBookCommandValidator : AbstractValidator<UpdatePhoneBookCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdatePhoneBookCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(200).WithMessage("Name must not exceed 200 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("The specified Name already exists.");
        }

        public async Task<bool> BeUniqueTitle(UpdatePhoneBookCommand model, string name, CancellationToken cancellationToken)
        {
            return await _context.PhoneBooks
                .Where(l => l.Id != model.Id)
                .AllAsync(l => l.Name != name, cancellationToken);
        }
    }

}