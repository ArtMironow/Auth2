using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Handlers.Review.Requests
{
    public record GetReviewsByEmailRequest(string email) : IRequest<IActionResult>;
}
