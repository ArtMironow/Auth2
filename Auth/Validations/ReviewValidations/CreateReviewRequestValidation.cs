using Auth.Handlers.Review.Requests;
using FluentValidation;

namespace Auth.Validations.ReviewValidations
{
    public class CreateReviewRequestValidation : AbstractValidator<CreateReviewRequest>
    {
        public CreateReviewRequestValidation() 
        {
            RuleFor(x => x.createReviewRequestDto.Email).NotEmpty();
        }
    }
}
