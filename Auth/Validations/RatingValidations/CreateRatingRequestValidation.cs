using Auth.Handlers.Rating.Requests;
using FluentValidation;

namespace Auth.Validations.RatingValidations
{
    public class CreateRatingRequestValidation : AbstractValidator<CreateRatingRequest>
    {
        public CreateRatingRequestValidation() 
        {
            RuleFor(x => x.createRatingRequestDto.Value).NotEmpty();
            RuleFor(x => x.createRatingRequestDto.Email).NotEmpty();
            RuleFor(x => x.createRatingRequestDto.ReviewId).NotEmpty();
        }
    }
}
