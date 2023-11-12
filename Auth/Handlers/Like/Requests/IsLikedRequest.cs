using MediatR;
using Auth.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Handlers.Like.Requests
{
    public record IsLikedRequest(GetLikeRequestDto getLikeRequestDto) : IRequest<IActionResult>;
}
