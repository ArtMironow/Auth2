using MediatR;
using Auth.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Handlers.Rating.Requests
{
    public record GetRatingByEmailAndReviewIdRequest(GetRatingByEmailAndReviewIdRequestDto getRatingByEmailAndReviewIdRequestDto) : IRequest<IActionResult>;
}
