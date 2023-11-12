using Auth.Handlers.Accounts.Requests;
using FluentValidation;

namespace Auth.Validations.AccountsValidations
{
    public class RegisterRequestValidation : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidation()
        {
            RuleFor(x => x.userToRegisterDto.Email).NotEmpty().WithMessage("Email is required.");
            RuleFor(x => x.userToRegisterDto.Email).EmailAddress().WithMessage("Email is incorrect");
            RuleFor(x => x.userToRegisterDto.Password).NotEmpty().WithMessage("Password is required.");
            RuleFor(x => x.userToRegisterDto.ConfirmPassword).Equal(x => x.userToRegisterDto.Password).WithMessage("The password and confirmation password do not match.");
        }
    }
}
