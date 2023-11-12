using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DAL.Auth.Models;
using Auth.Handlers.Like.Requests;
using DAL.Auth.Repository.Interfaces;

namespace Auth.Handlers.Like
{
    public class GetAllUsersLikesHandler : ControllerBase, IRequestHandler<GetAllUsersLikesRequest, IActionResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly ILikeRepository _likeRepository;

        public GetAllUsersLikesHandler(UserManager<User> userManager, ILikeRepository likeRepository)
        {
            _userManager = userManager;
            _likeRepository = likeRepository;
        }

        public async Task<IActionResult> Handle(GetAllUsersLikesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.email);

                if (user == null)
                {
                    return BadRequest(new
                    {
                        IsSuccess = false,
                        Errors = new List<string> { "User not found." },
                        Data = ""
                    });
                }

                var likes = await _likeRepository.GetAllLikesThatWereGivenToUserByUserId(user.Id);
                int allUserLikes = likes.Count();

                return Ok(new
                {
                    IsSuccess = true,
                    Errors = "",
                    Data = allUserLikes
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
