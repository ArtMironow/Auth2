using MediatR;
using Auth.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Handlers.Accounts.Requests
{
    public record ExternalLoginRequest(ExternalAuthDto externalAuthDto) : IRequest<IActionResult>;
}
