using MediatR;
using Auth.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Handlers.Accounts.Requests
{  
    public record ForgotPasswordRequest(ForgotPasswordDto forgotPasswordDto) : IRequest<IActionResult>;
}
