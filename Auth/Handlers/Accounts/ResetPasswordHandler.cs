using MediatR;
using Auth.Handlers.Accounts.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DAL.Auth.Models;
using Auth.Entities.DataTransferObjects;

namespace Auth.Handlers.Accounts
{
    public class ResetPasswordHandler : ControllerBase, IRequestHandler<ResetPasswordRequest, IActionResult>
    {
        private readonly UserManager<User> _userManager;

        public ResetPasswordHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.resetPasswordDto.Email);

            if (user == null)
            {
                return Unauthorized(new ResponseDto
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Invalid authentication" },
                    Data = null
                });
            }

            var result = await _userManager.ResetPasswordAsync(user, request.resetPasswordDto.Token, request.resetPasswordDto.Password);

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

            return Ok();
        }
    }
}
