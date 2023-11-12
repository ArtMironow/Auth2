using MediatR;
using Auth.Handlers.Accounts.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DAL.Auth.Models;
using Auth.Entities.DataTransferObjects;
using Microsoft.AspNetCore.WebUtilities;
using Auth.Services.EmailService;

namespace Auth.Handlers.Accounts
{
    public class ForgotPasswordHandler : ControllerBase, IRequestHandler<ForgotPasswordRequest, IActionResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordHandler(UserManager<User> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> Handle(ForgotPasswordRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.forgotPasswordDto.Email);

            if (user == null)
            {
                return Unauthorized(new ResponseDto
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Invalid authentication" },
                    Data = null
                });
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var param = new Dictionary<string, string?>
            {
                { "token", token },
                { "email", request.forgotPasswordDto.Email }
            };

            var callback = QueryHelpers.AddQueryString(request.forgotPasswordDto.ClientURI, param);

            var message = new Message(new string[] { user.Email }, "Reset password token", callback, null);
            await _emailSender.SendEmailAsync(message);

            return Ok();
        }
    }
}
