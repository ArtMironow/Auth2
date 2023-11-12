using Auth.Handlers.Accounts.Requests;
using FluentValidation;

namespace Auth.Validations.AccountsValidations
{
    public class ResetPasswordRequestValidation : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestValidation()
        {
            RuleFor(x => x.resetPasswordDto.Password).NotEmpty().WithMessage("Password is required.");
            RuleFor(x => x.resetPasswordDto.ConfirmPassword).Equal(x => x.resetPasswordDto.Password).WithMessage("The password and confirmation password do not match.");
        }
    }
}
