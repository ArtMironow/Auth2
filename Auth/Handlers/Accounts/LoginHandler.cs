using MediatR;
using Auth.Handlers.Accounts.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DAL.Auth.Models;
using Auth.Entities.DataTransferObjects;
using Auth.Features;

namespace Auth.Handlers.Accounts
{
    public class LoginHandler : ControllerBase, IRequestHandler<LoginRequest, IActionResult>
    {
        private readonly UserManager<User> _userManager;       
        private readonly IJwtHandler _jwtHandler;
        
        public LoginHandler(UserManager<User> userManager, IJwtHandler jwtHandler)
        {
            _userManager = userManager;
            _jwtHandler = jwtHandler;
        }

        public async Task<IActionResult> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.userToLoginDto.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, request.userToLoginDto.Password))
            {
                return Unauthorized(new ResponseDto
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Invalid authentication" },
                    Data = null
                });
            }

            var token = await _jwtHandler.GenerateToken(user);

            return Ok(new ResponseDto
            {
                IsSuccess = true,
                Errors = null,
                Data = token
            });
        }
    }
}
