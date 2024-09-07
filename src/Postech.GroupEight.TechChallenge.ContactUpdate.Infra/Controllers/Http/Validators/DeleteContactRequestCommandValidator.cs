using FluentValidation;
using Postech.GroupEight.TechChallenge.ContactDelete.Core.ValueObjects;
using Postech.GroupEight.TechChallenge.ContactDelete.Infra.Controllers.Http.Commands;

namespace Postech.GroupEight.TechChallenge.ContactDelete.Infra.Controllers.Http.Validators
{
    public class DeleteContactRequestCommandValidator : AbstractValidator<DeleteContactRequestCommand>
    {
        public DeleteContactRequestCommandValidator()
        {
            AddRuleForContactId();
        }

        private void AddRuleForContactId()
        {
            RuleFor(command => command.ContactId).NotEmpty().WithMessage("The contact identifier must be provided");
        }
    }
}