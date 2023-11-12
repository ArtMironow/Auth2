using MediatR;
using Microsoft.AspNetCore.Mvc;
using DAL.Auth.Repository.Interfaces;
using Auth.Handlers.Like.Requests;

namespace Auth.Handlers.Like
{
    public class GetAllReviewLikesHandler : ControllerBase, IRequestHandler<GetAllReviewLikesRequest, IActionResult>
    {
        private readonly ILikeRepository _likeRepository;

        public GetAllReviewLikesHandler(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }

        public async Task<IActionResult> Handle(GetAllReviewLikesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var allReviewLikes = await _likeRepository.GetLikesByReviewId(request.id);
                var likeCount = allReviewLikes.Count();

                return Ok(new
                {
                    IsSuccess = true,
                    Errors = "",
                    Data = likeCount
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Failed to get all likes." },
                    Data = ""
                });
            }
        }
    }
}
