using MediatR;
using Auth.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Handlers.Like.Requests
{
    public record CreateLikeRequest(CreateLikeRequestDto createLikeRequestDto) : IRequest<IActionResult>;
}
