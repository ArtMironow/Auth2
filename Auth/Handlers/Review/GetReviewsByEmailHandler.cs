using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DAL.Auth.Models;
using Auth.Handlers.Review.Requests;
using Auth.Entities.Models;
using DAL.Auth.Repository.Interfaces;

namespace Auth.Handlers.Review
{
    public class GetReviewsByEmailHandler : ControllerBase, IRequestHandler<GetReviewsByEmailRequest, IActionResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly IReviewRepository _reviewRepository;
        private readonly ILikeRepository _likeRepository;
        private readonly IRatingRepository _ratingRepository;

        public GetReviewsByEmailHandler(UserManager<User> userManager, IReviewRepository reviewRepository, ILikeRepository likeRepository, IRatingRepository ratingRepository)
        {
            _userManager = userManager;
            _reviewRepository = reviewRepository;
            _likeRepository = likeRepository;
            _ratingRepository = ratingRepository;
        }

        public async Task<IActionResult> Handle(GetReviewsByEmailRequest request, CancellationToken cancellationToken)
        {
            IList<EnhancedReview> resultReviews = new List<EnhancedReview>();
            IList<DAL.Auth.Models.Review> wholeReviews = new List<DAL.Auth.Models.Review>();

            var user = await _userManager.FindByEmailAsync(request.email);

            try
            {
                wholeReviews = await _reviewRepository.GetReviewsByUserId(user.Id);

                foreach (var review in wholeReviews)
                {
                    var reviewRatings = await _ratingRepository.GetRatingsByReviewId(review.Id.ToString());
                    var resultRating = reviewRatings.Average(x => x.Value);

                    var allReviewLikes = await _likeRepository.GetLikesByReviewId(review.Id.ToString());
                    var likeCount = allReviewLikes.Count();

                    var resultReview = new EnhancedReview()
                    {
                        Id = review.Id,
                        Nickname = user.Nickname,
                        Title = review.Title,
                        Description = review.Description,
                        ReviewText = review.ReviewText,
                        Theme = review.Theme,
                        Image = review.Image,
                        Created = review.Created,
                        Link = review.Link,
                        UserId = review.UserId,
                        Rating = resultRating,
                        LikesCount = likeCount
                    };

                    resultReviews.Add(resultReview);
                }
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
                Data = resultReviews
            });
        }
    }
}
