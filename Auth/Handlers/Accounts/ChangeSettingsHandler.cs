using Auth.Handlers.Accounts.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DAL.Auth.Models;
using Auth.Entities.DataTransferObjects;
using Auth.Features;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Handlers.Accounts
{
    public class ChangeSettingsHandler : ControllerBase, IRequestHandler<ChangeSettingsRequest, IActionResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtHandler _jwtHandler;

        public ChangeSettingsHandler(UserManager<User> userManager, IJwtHandler jwtHandler)
        {
            _userManager = userManager;
            _jwtHandler = jwtHandler;
        }

        public async Task<IActionResult> Handle(ChangeSettingsRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.changeSettingsDto.Email);

            if (user == null)
            {
                return Unauthorized(new ResponseDto
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Invalid authentication" },
                    Data = null
                });
            }

            IdentityResult result;

            if (!request.changeSettingsDto.OldPassword.IsNullOrEmpty() && !request.changeSettingsDto.Password.IsNullOrEmpty() &&
                !request.changeSettingsDto.ConfirmPassword.IsNullOrEmpty())
            {
                if (!await _userManager.CheckPasswordAsync(user, request.changeSettingsDto.OldPassword))
                {
                    return BadRequest(new ResponseDto
                    {
                        IsSuccess = false,
                        Errors = new List<string> { "Invalid password" },
                        Data = null
                    });
                }

                result = await _userManager.ChangePasswordAsync(user, request.changeSettingsDto.OldPassword, request.changeSettingsDto.Password);

                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description);
                    return BadRequest(new ResponseDto
                    {
                        IsSuccess = false,
                        Errors = errors,
                        Data = null
                    });
                }
            }

            user.Nickname = request.changeSettingsDto.Nickname;
            result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new ResponseDto
                {
                    IsSuccess = false,
                    Errors = errors,
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
