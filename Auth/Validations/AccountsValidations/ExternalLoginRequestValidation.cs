using Auth.Handlers.Accounts.Requests;
using FluentValidation;

namespace Auth.Validations.AccountsValidations
{
    public class ExternalLoginRequestValidation : AbstractValidator<ExternalLoginRequest>
    {
        public ExternalLoginRequestValidation()
        {
            RuleFor(x => x.externalAuthDto.Provider).NotEmpty();
            RuleFor(x => x.externalAuthDto.IdToken).NotEmpty();
        }
    }
}
