using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Handlers.Accounts.Requests
{
    public record AccountInfoRequest(string userName) : IRequest<IActionResult>;
}
