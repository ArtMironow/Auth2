using Auth.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Auth.Handlers.Review.Requests;

namespace Auth.Controllers
{
    [Route("api/review")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReviewController(IMediator mediator)
        {           
            _mediator = mediator;
        }

        [HttpGet("getreview/{id}")]
        [Authorize]
        public async Task<IActionResult> GetReviewById(string id)
        {
            return await _mediator.Send(new GetReviewByIdRequest(id));
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllReviews()
        {
            return await _mediator.Send(new GetAllReviewsRequest());
        }

        [HttpGet("getbyemail/{email}")]
        [Authorize]
        public async Task<IActionResult> GetReviewsByEmail(string email)
        {
            return await _mediator.Send(new GetReviewsByEmailRequest(email));
        }

        [HttpPost("createreview")]
        [Authorize]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewRequestDto createReviewRequestDto)
        {
            return await _mediator.Send(new CreateReviewRequest(createReviewRequestDto));
        }

        [HttpPost("updatereview")]
        [Authorize]
        public async Task<IActionResult> UpdateReview([FromBody] UpdateReviewRequestDto updateReviewRequestDto)
        {               
            return await _mediator.Send(new UpdateReviewRequest(updateReviewRequestDto));
        }

        [HttpDelete("deletereview/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteReview(string id)
        {  
            return await _mediator.Send(new DeleteReviewRequest(id));
        }
    }
}
