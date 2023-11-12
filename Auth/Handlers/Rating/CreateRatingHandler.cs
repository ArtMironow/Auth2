using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DALModels = DAL.Auth.Models;
using Auth.Handlers.Rating.Requests;
using DAL.Auth.Repository.Interfaces;

namespace Auth.Handlers.Rating
{
    public class CreateRatingHandler : ControllerBase, IRequestHandler<CreateRatingRequest, IActionResult>
    {
        private readonly UserManager<DALModels.User> _userManager;
        private readonly IRatingRepository _ratingRepository;

        public CreateRatingHandler(UserManager<DALModels.User> userManager, IRatingRepository ratingRepository)
        {
            _userManager = userManager;
            _ratingRepository = ratingRepository;
        }

        public async Task<IActionResult> Handle(CreateRatingRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.createRatingRequestDto.Email);

            if (user == null)
            {
                return BadRequest(new
                {
                    IsSuccess = false,
                    Errors = new List<string> { "User not found." },
                    Data = ""
                });
            }

            if (!(await _ratingRepository.GetRatingByUserAndReviewIds(user.Id, request.createRatingRequestDto.ReviewId) == null))
            {
                return BadRequest(new
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Rating already exists." },
                    Data = ""
                });
            }

            var rating = new DALModels.Rating()
            {
                Id = Guid.NewGuid(),
                Value = request.createRatingRequestDto.Value,
                UserId = user.Id,
                ReviewId = new Guid(request.createRatingRequestDto.ReviewId)
            };

            try
            {
                await _ratingRepository.CreateRating(rating);
                await _ratingRepository.SaveChanges();
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

            return Ok(new
            {
                isSuccess = true,
                Errors = "",
                Data = rating
            });
        }
    }
}
