using MediatR;
using Microsoft.AspNetCore.Mvc;
using DAL.Auth.Repository.Interfaces;
using Auth.Handlers.Like.Requests;

namespace Auth.Handlers.Like
{
    public class DeleteLikeHandler : ControllerBase, IRequestHandler<DeleteLikeRequest, IActionResult>
    {
        private readonly ILikeRepository _likeRepository;

        public DeleteLikeHandler(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }

        public async Task<IActionResult> Handle(DeleteLikeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var like = await _likeRepository.GetLike(request.id);

                if (like != null)
                {
                    await _likeRepository.DeleteLike(like);
                    await _likeRepository.SaveChanges();
                }
                else
                {
                    return BadRequest(new
                    {
                        IsSuccess = false,
                        Errors = new List<string> { "Like not found." },
                        Data = ""
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Failed to delete Like." },
                    Data = ""
                });
            }

            return Ok();
        }
    }
}
