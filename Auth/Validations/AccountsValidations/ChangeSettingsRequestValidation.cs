using Auth.Handlers.Accounts.Requests;
using FluentValidation;

namespace Auth.Validations.AccountsValidations
{
    public class ChangeSettingsRequestValidation : AbstractValidator<ChangeSettingsRequest>
    {
        public ChangeSettingsRequestValidation()
        {
            RuleFor(x => x.changeSettingsDto.Email).NotEmpty().WithMessage("Email is required.");
            RuleFor(x => x.changeSettingsDto.Email).EmailAddress().WithMessage("Email is incorrect.");
            RuleFor(x => x.changeSettingsDto.ConfirmPassword).Equal(x => x.changeSettingsDto.Password).WithMessage("The password and confirmation password do not match.");
        }
    }
}
