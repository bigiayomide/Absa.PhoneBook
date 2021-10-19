using System.Threading;
using System.Threading.Tasks;
using Absa.Application.Common.Interfaces;
using Absa.Application.PhoneBook.Commands.CreatePhoneBook;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.PhoneBook.Commands.CreatePhoneBook
{

    public class CreatePhoneBookCommandValidator : AbstractValidator<CreatePhoneBookCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreatePhoneBookCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(200).WithMessage("Name must not exceed 200 characters.")
                .MustAsync(BeUniqueName).WithMessage("The specified name already exists.");
        }

        public async Task<bool> BeUniqueName(string title, CancellationToken cancellationToken)
        {
            return await _context.PhoneBooks
                .AllAsync(l => l.Name != title, cancellationToken);
        }
    }
}