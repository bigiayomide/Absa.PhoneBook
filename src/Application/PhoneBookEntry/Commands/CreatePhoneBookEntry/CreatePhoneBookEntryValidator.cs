using Absa.Application.TodoItems.Commands.CreatePhoneBookEntry;
using FluentValidation;

namespace Absa.Application.TodoItems.Commands.CreatePhoneBookEntry
{
    public class CreatePhoneBookEntryCommandValidator : AbstractValidator<CreatePhoneBookEntryCommand>
    {
        public CreatePhoneBookEntryCommandValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(200)
                .NotEmpty();

            RuleFor(v => v.Number)
            .MaximumLength(200)
            .NotEmpty();
        }
    }
}
