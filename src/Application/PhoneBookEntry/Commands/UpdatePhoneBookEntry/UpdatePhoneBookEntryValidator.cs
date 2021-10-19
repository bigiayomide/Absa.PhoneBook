using Absa.Application.PhoneBookEntry.Commands.UpdatePhoneBookEntry;
using FluentValidation;

namespace Absa.Application.TodoItems.Commands.UpdateTodoItem
{
    public class UpdatePhoneBookEntryCommandValidator : AbstractValidator<UpdatePhoneBookEntryCommand>
    {
        public UpdatePhoneBookEntryCommandValidator()
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
