using Auth.Handlers.Rating.Requests;
using FluentValidation;

namespace Auth.Validations.RatingValidations
{
    public class GetRatingByEmailAndReviewIdRequestValidation : AbstractValidator<GetRatingByEmailAndReviewIdRequest>
    {
        public GetRatingByEmailAndReviewIdRequestValidation() 
        {
            RuleFor(x => x.getRatingByEmailAndReviewIdRequestDto.Email).NotEmpty();
            RuleFor(x => x.getRatingByEmailAndReviewIdRequestDto.ReviewId).NotEmpty();
        }
    }
}
