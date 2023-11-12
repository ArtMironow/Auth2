using Auth.Handlers.Accounts.Requests;
using FluentValidation;

namespace Auth.Validations.AccountsValidations
{
    public class ForgotPasswordRequestValidation : AbstractValidator<ForgotPasswordRequest>
    {
        public ForgotPasswordRequestValidation()
        {
            RuleFor(x => x.forgotPasswordDto.Email).NotEmpty();
            RuleFor(x => x.forgotPasswordDto.Email).EmailAddress();
            RuleFor(x => x.forgotPasswordDto.ClientURI).NotEmpty();
        }
    }
}
