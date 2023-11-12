using MediatR;
using Microsoft.AspNetCore.Mvc;
using Auth.Features;
using Auth.Handlers.Review.Requests;
using Auth.Enums;
using DAL.Auth.Repository.Interfaces;
using ImgbbApi.Interfaces;

namespace Auth.Handlers.Review
{
    public class UpdateReviewHandler : ControllerBase, IRequestHandler<UpdateReviewRequest, IActionResult>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IImgbbApiClient _imgbbApiClient;

        public UpdateReviewHandler(IReviewRepository reviewRepository, IImgbbApiClient imgbbApiClient)
        {
            _reviewRepository = reviewRepository;
            _imgbbApiClient = imgbbApiClient;
        }

        public async Task<IActionResult> Handle(UpdateReviewRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var currentReview = await _reviewRepository.GetByReviewId(request.updateReviewRequestDto.Id);

                currentReview.Title = request.updateReviewRequestDto.Title;
                currentReview.Description = request.updateReviewRequestDto.Description;
                currentReview.ReviewText = request.updateReviewRequestDto.ReviewText;
                currentReview.Theme = request.updateReviewRequestDto.Theme;
                currentReview.Image = await _imgbbApiClient.UploadImage(CropImageFeature.CropImage(request.updateReviewRequestDto.Image, (int)ImageSizeEnum.Default));

                await _reviewRepository.UpdateReview(currentReview);
                await _reviewRepository.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Failed to update review." },
                    Data = ""
                });
            }

            return Ok();
        }
    }
}
