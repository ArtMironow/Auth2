using MediatR;
using Microsoft.AspNetCore.Mvc;
using Auth.Handlers.Rating.Requests;
using DAL.Auth.Repository.Interfaces;

namespace Auth.Handlers.Rating
{
    public class DeleteRatingHandler : ControllerBase, IRequestHandler<DeleteRatingRequest, IActionResult>
    {
        private readonly IRatingRepository _ratingRepository;

        public DeleteRatingHandler(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public async Task<IActionResult> Handle(DeleteRatingRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var rating = await _ratingRepository.GetRating(request.id);

                if (rating != null)
                {
                    await _ratingRepository.DeleteRating(rating);
                    await _ratingRepository.SaveChanges();
                }
                else
                {
                    return BadRequest(new
                    {
                        IsSuccess = false,
                        Errors = new List<string> { "Rating record not found." },
                        Data = ""
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Failed to delete Rating record." },
                    Data = ""
                });
            }

            return Ok();
        }
    }
}
