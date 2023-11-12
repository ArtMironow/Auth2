using Auth.Handlers.Rating.Requests;
using FluentValidation;

namespace Auth.Validations.RatingValidations
{
    public class UpdateRatingRequestValidation : AbstractValidator<UpdateRatingRequest>
    {
        public UpdateRatingRequestValidation() 
        {
            RuleFor(x => x.updateRatingRequestDto.Id).NotEmpty();
            RuleFor(x => x.updateRatingRequestDto.Value).NotEmpty();
        }
    }
}
