using Auth.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Auth.Handlers.Like.Requests;

namespace Auth.Controllers
{
    [Route("api/likes")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LikeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("createlike")]
        [Authorize]
        public async Task<IActionResult> CreateLike([FromBody] CreateLikeRequestDto createLikeRequestDto)
        {
            return await _mediator.Send(new CreateLikeRequest(createLikeRequestDto));
        }

        [HttpDelete("deletelike/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteLike(string id)
        {
            return await _mediator.Send(new DeleteLikeRequest(id));
        }

        [HttpPost("isliked")]
        [Authorize]
        public async Task<IActionResult> IsLiked([FromBody] GetLikeRequestDto getLikeRequestDto)
        {
            return await _mediator.Send(new IsLikedRequest(getLikeRequestDto));
        }

        [HttpGet("getallreviewlikes/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllReviewLikes(string id)
        {
            return await _mediator.Send(new GetAllReviewLikesRequest(id));
        }

        [HttpGet("getalluserslikes/{email}")]
        [Authorize]
        public async Task<IActionResult> GetAllUsersLikes(string email)
        {
            return await _mediator.Send(new GetAllUsersLikesRequest(email));
        }
    }
}
