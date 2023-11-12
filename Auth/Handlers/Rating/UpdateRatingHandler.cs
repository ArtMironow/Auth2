using MediatR;
using Microsoft.AspNetCore.Mvc;
using Auth.Handlers.Rating.Requests;
using DAL.Auth.Repository.Interfaces;

namespace Auth.Handlers.Rating
{
    public class UpdateRatingHandler : ControllerBase, IRequestHandler<UpdateRatingRequest, IActionResult>
    {
        private readonly IRatingRepository _ratingRepository;

        public UpdateRatingHandler(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public async Task<IActionResult> Handle(UpdateRatingRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var currentRating = await _ratingRepository.GetRating(request.updateRatingRequestDto.Id);
                currentRating.Value = request.updateRatingRequestDto.Value;

                await _ratingRepository.UpdateRating(currentRating);
                await _ratingRepository.SaveChanges();

                return Ok(new
                {
                    isSuccess = true,
                    Errors = "",
                    Data = currentRating
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Failed to update rating." },
                    Data = ""
                });
            }
        }
    }
}
