using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DALModels = DAL.Auth.Models;
using Auth.Handlers.Like.Requests;
using DAL.Auth.Repository.Interfaces;

namespace Auth.Handlers.Like
{
    public class CreateLikeHandler : ControllerBase, IRequestHandler<CreateLikeRequest, IActionResult>
    {
        private readonly UserManager<DALModels.User> _userManager;
        private readonly ILikeRepository _likeRepository;

        public CreateLikeHandler(UserManager<DALModels.User> userManager, ILikeRepository likeRepository)
        {
            _userManager = userManager;
            _likeRepository = likeRepository;
        }

        public async Task<IActionResult> Handle(CreateLikeRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.createLikeRequestDto.Email);

            if (user == null)
            {
                return BadRequest(new
                {
                    IsSuccess = false,
                    Errors = new List<string> { "User not found." },
                    Data = ""
                });
            }

            if (!(await _likeRepository.GetLikeByUserAndReviewIds(user.Id, request.createLikeRequestDto.ReviewId) == null))
            {
                return BadRequest(new
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Like already exists." },
                    Data = ""
                });
            }

            var like = new DALModels.Like()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                ReviewId = new Guid(request.createLikeRequestDto.ReviewId)
            };

            try
            {
                await _likeRepository.CreateLike(like);
                await _likeRepository.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Failed to create like." },
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
    }
}
