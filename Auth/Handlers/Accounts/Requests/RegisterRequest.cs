using MediatR;
using Auth.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Handlers.Accounts.Requests
{
    public record RegisterRequest(UserToRegisterDto userToRegisterDto) : IRequest<IActionResult>;
}
