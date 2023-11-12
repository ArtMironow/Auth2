using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DAL.Auth.Models;
using Auth.Handlers.Like.Requests;
using DAL.Auth.Repository.Interfaces;

namespace Auth.Handlers.Like
{
    public class IsLikedHandler : ControllerBase, IRequestHandler<IsLikedRequest, IActionResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly ILikeRepository _likeRepository;

        public IsLikedHandler(UserManager<User> userManager, ILikeRepository likeRepository)
        {
            _userManager = userManager;
            _likeRepository = likeRepository;
        }

        public async Task<IActionResult> Handle(IsLikedRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.getLikeRequestDto.Email);

                if (user == null)
                {
                    return BadRequest(new
                    {
                        IsSuccess = false,
                        Errors = new List<string> { "User not found." },
                        Data = ""
                    });
                }

                var like = await _likeRepository.GetLikeByUserAndReviewIds(user.Id, request.getLikeRequestDto.ReviewId);

                if (like == null)
                {
                    return Ok(new
                    {
                        IsSuccess = true,
                        Errors = "",
                        Data = ""
                    });
                }

                return Ok(new
                {
                    isSuccess = true,
                    Errors = "",
                    Data = like
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Failed to get Like." },
                    Data = ""
                });
            }
        }
    }
}
