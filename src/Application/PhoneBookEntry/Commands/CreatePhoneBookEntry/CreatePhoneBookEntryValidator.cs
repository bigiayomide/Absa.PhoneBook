using FluentValidation;

namespace Absa.Application.PhoneBookEntry.Commands.CreatePhoneBookEntry
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
