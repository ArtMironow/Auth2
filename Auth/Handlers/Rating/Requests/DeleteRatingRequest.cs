using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Handlers.Rating.Requests
{
    public record DeleteRatingRequest(string id) : IRequest<IActionResult>;
}
