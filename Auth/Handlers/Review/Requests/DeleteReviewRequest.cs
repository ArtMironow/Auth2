using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Handlers.Review.Requests
{
    public record DeleteReviewRequest(string id) : IRequest<IActionResult>;
}
