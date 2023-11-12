using MediatR;
using Auth.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Handlers.Review.Requests
{
    public record CreateReviewRequest(CreateReviewRequestDto createReviewRequestDto) : IRequest<IActionResult>;
}
