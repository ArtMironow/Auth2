using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DAL.Auth.Models;
using Auth.Handlers.Review.Requests;
using Auth.Entities.Models;
using DAL.Auth.Repository.Interfaces;

namespace Auth.Handlers.Review
{
    public class GetReviewByIdHandler : ControllerBase, IRequestHandler<GetReviewByIdRequest, IActionResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly IReviewRepository _reviewRepository;
        private readonly ILikeRepository _likeRepository;
        private readonly IRatingRepository _ratingRepository;

        public GetReviewByIdHandler(UserManager<User> userManager, IReviewRepository reviewRepository, ILikeRepository likeRepository, IRatingRepository ratingRepository)
        {
            _userManager = userManager;
            _reviewRepository = reviewRepository;
            _likeRepository = likeRepository;
            _ratingRepository = ratingRepository;
        }

        public async Task<IActionResult> Handle(GetReviewByIdRequest request, CancellationToken cancellationToken)
        {
            var enhancedReview = new EnhancedReview();

            try
            {
                var review = await _reviewRepository.GetByReviewId(request.id);

                if (review == null)
                {
                    return BadRequest(new
                    {
                        IsSuccess = false,
                        Errors = new List<string> { "There is no such review." },
                        Data = ""
                    });
                }

                var reviewRatings = await _ratingRepository.GetRatingsByReviewId(review.Id.ToString());
                var resultRating = reviewRatings.Average(x => x.Value);

                var allReviewLikes = await _likeRepository.GetLikesByReviewId(review.Id.ToString());
                var likeCount = allReviewLikes.Count();

                var currentUser = await _userManager.FindByIdAsync(review.UserId);

                enhancedReview.Id = review.Id;
                enhancedReview.Nickname = currentUser.Nickname;
                enhancedReview.Title = review.Title;
                enhancedReview.Description = review.Description;
                enhancedReview.ReviewText = review.ReviewText;
                enhancedReview.Theme = review.Theme;
                enhancedReview.Image = review.Image;
                enhancedReview.Created = review.Created;
                enhancedReview.Link = review.Link;
                enhancedReview.UserId = review.UserId;
                enhancedReview.Rating = resultRating;
                enhancedReview.LikesCount = likeCount;
            }
            catch
            {
                return BadRequest(new
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Something went wrong." },
                    Data = ""
                });
            }

            return Ok(new
            {
                IsSuccess = true,
                Errors = "",
                Data = enhancedReview
            });
        }
    }
}
