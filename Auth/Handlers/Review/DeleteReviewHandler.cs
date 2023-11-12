using MediatR;
using Microsoft.AspNetCore.Mvc;
using Auth.Handlers.Review.Requests;
using DAL.Auth.Repository.Interfaces;

namespace Auth.Handlers.Review
{
    public class DeleteReviewHandler : ControllerBase, IRequestHandler<DeleteReviewRequest, IActionResult>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ILikeRepository _likeRepository;
        private readonly IRatingRepository _ratingRepository;

        public DeleteReviewHandler(IRatingRepository ratingRepository, ILikeRepository likeRepository, IReviewRepository reviewRepository)
        {
            _ratingRepository = ratingRepository;
            _likeRepository = likeRepository;
            _reviewRepository = reviewRepository;
        }

        public async Task<IActionResult> Handle(DeleteReviewRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var review = await _reviewRepository.GetByReviewId(request.id);

                if (review != null)
                {
                    var likes = await _likeRepository.GetLikesByReviewId(request.id);

                    foreach (var like in likes)
                    {
                        await _likeRepository.DeleteLike(like);
                    }

                    var ratings = await _ratingRepository.GetRatingsByReviewId(request.id);

                    foreach (var rating in ratings)
                    {
                        await _ratingRepository.DeleteRating(rating);
                    }

                    await _reviewRepository.DeleteReview(review);

                    await _reviewRepository.SaveChanges();
                }
                else
                {
                    return BadRequest(new
                    {
                        IsSuccess = false,
                        Errors = new List<string> { "Review not found." },
                        Data = ""
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Failed to delete review." },
                    Data = ""
                });
            }

            return Ok();
        }
    }
}
