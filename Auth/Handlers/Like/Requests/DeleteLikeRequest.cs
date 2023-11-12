using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Handlers.Like.Requests
{
    public record DeleteLikeRequest(string id) : IRequest<IActionResult>;
}
