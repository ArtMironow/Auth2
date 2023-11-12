using Auth.Handlers.Accounts.Requests;
using FluentValidation;

namespace Auth.Validations.AccountsValidations
{
    public class LoginRequestValidation : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidation()
        {
            RuleFor(x => x.userToLoginDto.Email).NotEmpty().WithMessage("Email is required.");
            RuleFor(x => x.userToLoginDto.Email).EmailAddress().WithMessage("Email is incorrect");
            RuleFor(x => x.userToLoginDto.Password).NotEmpty().WithMessage("Password is required.");
        }
    }
}
