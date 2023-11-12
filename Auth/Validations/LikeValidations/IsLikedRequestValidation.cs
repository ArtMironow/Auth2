using Auth.Handlers.Like.Requests;
using FluentValidation;

namespace Auth.Validations.LikeValidations
{
    public class IsLikedRequestValidation : AbstractValidator<IsLikedRequest>
    {
        public IsLikedRequestValidation()
        {
            RuleFor(x => x.getLikeRequestDto.Email).NotEmpty();
            RuleFor(x => x.getLikeRequestDto.ReviewId).NotEmpty();
        }
    }
}
