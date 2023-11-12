using Auth.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Auth.Handlers.Rating.Requests;

namespace Auth.Controllers
{
    [Route("api/ratings")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RatingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("createrating")]
        [Authorize]
        public async Task<IActionResult> CreateRating([FromBody] CreateRatingRequestDto createRatingRequestDto)
        {
            return await _mediator.Send(new CreateRatingRequest(createRatingRequestDto));
        }

        [HttpPost("updaterating")]
        [Authorize]
        public async Task<IActionResult> UpdateRating([FromBody] UpdateRatingRequestDto updateRatingRequestDto)
        {
            return await _mediator.Send(new UpdateRatingRequest(updateRatingRequestDto));
        }

        [HttpDelete("deleterating/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteRating(string id)
        {
            return await _mediator.Send(new DeleteRatingRequest(id));
        }

        [HttpGet("getreviewsrating/{id}")]
        [Authorize]
        public async Task<IActionResult> GetReviewsRating(string id)
        {
            return await _mediator.Send(new GetReviewsRatingRequest(id));
        }

        [HttpPost("getratingbyemailandreviewid")]
        [Authorize]
        public async Task<IActionResult> GetRatingByEmailAndReviewId([FromBody] GetRatingByEmailAndReviewIdRequestDto getRatingByEmailAndReviewIdRequestDto)
        {
            return await _mediator.Send(new GetRatingByEmailAndReviewIdRequest(getRatingByEmailAndReviewIdRequestDto));
        }

    }
}
