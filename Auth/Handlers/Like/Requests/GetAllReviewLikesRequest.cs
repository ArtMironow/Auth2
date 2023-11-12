using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Handlers.Like.Requests
{
    public record GetAllReviewLikesRequest(string id) : IRequest<IActionResult>;
}
