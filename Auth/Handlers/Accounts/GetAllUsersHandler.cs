using MediatR;
using Auth.Handlers.Accounts.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DAL.Auth.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.Handlers.Accounts
{
    public class GetAllUsersHandler : ControllerBase, IRequestHandler<GetAllUsersRequest, IActionResult>
    {
        private readonly UserManager<User> _userManager;

        public GetAllUsersHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
        {
            var users = await _userManager.Users
               .Select(x => new { Id = x.Id, Email = x.Email, Nickname = x.Nickname })
               .ToListAsync();

            return Ok(new
            {
                IsSuccess = true,
                Errors = "",
                Data = users
            });
        }
    }
}
