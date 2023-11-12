using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Handlers.Like.Requests
{
    public record GetAllUsersLikesRequest(string email) : IRequest<IActionResult>;
}
