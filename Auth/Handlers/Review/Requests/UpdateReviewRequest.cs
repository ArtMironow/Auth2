using MediatR;
using Auth.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Handlers.Review.Requests
{
    public record UpdateReviewRequest(UpdateReviewRequestDto updateReviewRequestDto) : IRequest<IActionResult>;
}
