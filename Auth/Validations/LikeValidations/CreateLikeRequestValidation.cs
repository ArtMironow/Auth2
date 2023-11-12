using Auth.Handlers.Like.Requests;
using FluentValidation;

namespace Auth.Validations.LikeValidations
{
    public class CreateLikeRequestValidation : AbstractValidator<CreateLikeRequest>
    {
        public CreateLikeRequestValidation()
        {
            RuleFor(x => x.createLikeRequestDto.Email).NotEmpty();
            RuleFor(x => x.createLikeRequestDto.ReviewId).NotEmpty();
        }
    }
}
