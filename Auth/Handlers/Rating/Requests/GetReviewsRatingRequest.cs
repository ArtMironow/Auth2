using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Handlers.Rating.Requests
{
    public record GetReviewsRatingRequest(string id) : IRequest<IActionResult>;
}
