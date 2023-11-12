using MediatR;
using Auth.Handlers.Accounts.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DAL.Auth.Models;
using Auth.Entities.DataTransferObjects;
using System.Text.Json.Nodes;

namespace Auth.Handlers.Accounts
{
    public class AccountInfoHandler : ControllerBase, IRequestHandler<AccountInfoRequest, IActionResult>
    {
        private readonly UserManager<User> _userManager;

        public AccountInfoHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Handle(AccountInfoRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.userName);

            if (user == null)
            {
                return Unauthorized(new ResponseDto
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Invalid authentication" },
                    Data = null
                });
            }

            var jsonObject = new JsonObject();
            jsonObject["Nickname"] = user.Nickname;
            jsonObject["Email"] = user.Email;

            return Ok(new AccountInfoResponseDto
            {
                IsSuccess = true,
                Errors = null,
                Data = jsonObject
            });
        }
    }
}
