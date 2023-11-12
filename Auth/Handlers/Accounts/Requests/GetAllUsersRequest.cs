using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Handlers.Accounts.Requests
{
    public record GetAllUsersRequest() : IRequest<IActionResult>;
}
