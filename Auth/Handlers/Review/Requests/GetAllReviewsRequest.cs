using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Handlers.Review.Requests
{
    public record GetAllReviewsRequest() : IRequest<IActionResult>;
}
