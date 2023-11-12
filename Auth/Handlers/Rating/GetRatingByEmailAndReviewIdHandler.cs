using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DAL.Auth.Models;
using Auth.Handlers.Rating.Requests;
using DAL.Auth.Repository.Interfaces;

namespace Auth.Handlers.Rating
{
    public class GetRatingByEmailAndReviewIdHandler : ControllerBase, IRequestHandler<GetRatingByEmailAndReviewIdRequest, IActionResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly IRatingRepository _ratingRepository;

        public GetRatingByEmailAndReviewIdHandler(UserManager<User> userManager, IRatingRepository ratingRepository)
        {
            _userManager = userManager;
            _ratingRepository = ratingRepository;
        }

        public async Task<IActionResult> Handle(GetRatingByEmailAndReviewIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.getRatingByEmailAndReviewIdRequestDto.Email);

                if (user == null)
                {
                    return BadRequest(new
                    {
                        IsSuccess = false,
                        Errors = new List<string> { "User not found." },
                        Data = ""
                    });
                }

                var rating = await _ratingRepository.GetRatingByUserAndReviewIds(user.Id, request.getRatingByEmailAndReviewIdRequestDto.ReviewId);

                if (rating == null)
                {
                    return Ok(new
                    {
                        isSuccess = true,
                        Errors = "",
                        Data = ""
                    });
                }

                return Ok(new
                {
                    isSuccess = true,
                    Errors = "",
                    Data = rating
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Failed to create Rating." },
                    Data = ""
                });
            }
        }
    }
}
