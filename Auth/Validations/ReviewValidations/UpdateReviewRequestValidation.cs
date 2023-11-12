using Auth.Handlers.Review.Requests;
using FluentValidation;

namespace Auth.Validations.ReviewValidations
{
    public class UpdateReviewRequestValidation : AbstractValidator<UpdateReviewRequest>
    {
        public UpdateReviewRequestValidation() 
        {
            RuleFor(x => x.updateReviewRequestDto.Id).NotEmpty();
        }
    }
}
