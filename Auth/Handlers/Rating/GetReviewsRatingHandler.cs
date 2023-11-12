using MediatR;
using Microsoft.AspNetCore.Mvc;
using Auth.Handlers.Rating.Requests;
using DAL.Auth.Repository.Interfaces;

namespace Auth.Handlers.Rating
{
    public class GetReviewsRatingHandler : ControllerBase, IRequestHandler<GetReviewsRatingRequest, IActionResult>
    {
        private readonly IRatingRepository _ratingRepository;

        public GetReviewsRatingHandler(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public async Task<IActionResult> Handle(GetReviewsRatingRequest request, CancellationToken cancellationToken)
        {
            double? rating = 0;

            try
            {
                var wholeRatings = await _ratingRepository.GetRatingsByReviewId(request.id);

                rating = wholeRatings.Average(x => x.Value);
            }
            catch
            {
                return BadRequest(new
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Something went wrong." },
                    Data = ""
                });
            }
            return Ok(new
            {
                IsSuccess = true,
                Errors = "",
                Data = rating
            });
        }
    }
}
