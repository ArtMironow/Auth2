using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DAL.Auth.Models;
using Auth.Entities.DataTransferObjects;
using Auth.Features;
using Auth.Handlers.Review.Requests;
using Auth.Enums;
using DAL.Auth.Repository.Interfaces;
using ImgbbApi.Interfaces;

namespace Auth.Handlers.Review
{
    public class CreateReviewHandler : ControllerBase, IRequestHandler<CreateReviewRequest, IActionResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly IReviewRepository _reviewRepository;
        private readonly IImgbbApiClient _imgbbApiClient;

        public CreateReviewHandler(UserManager<User> userManager, IReviewRepository reviewRepository, IImgbbApiClient imgbbApiClient)
        {
            _userManager = userManager;
            _reviewRepository = reviewRepository;
            _imgbbApiClient = imgbbApiClient;
        }

        public async Task<IActionResult> Handle(CreateReviewRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.createReviewRequestDto.Email);

            if (user == null)
            {
                return Unauthorized(new ResponseDto
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Invalid authentication" },
                    Data = null
                });
            }

            var resultReview = new DAL.Auth.Models.Review()
            {
                Id = Guid.NewGuid(),
                Title = request.createReviewRequestDto.Title,
                Description = request.createReviewRequestDto.Description,
                ReviewText = request.createReviewRequestDto.ReviewText,
                Theme = request.createReviewRequestDto.Theme,
                Image = await _imgbbApiClient.UploadImage(CropImageFeature.CropImage(request.createReviewRequestDto.Image, (int)ImageSizeEnum.Default)),
                Created = DateTime.Parse(DateTime.Now.ToString()).ToUniversalTime(),
                Link = LinkGeneratorFeature.GenerateLink(),
                UserId = user.Id,
            };

            try
            {
                await _reviewRepository.CreateReview(resultReview);
                await _reviewRepository.SaveChanges();
            }
            catch
            {
                return BadRequest(new
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Failed to create review." },
                    Data = ""
                });
            }

            return Ok();
        }
    }
}
