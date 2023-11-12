using Auth.Entities.DataTransferObjects;
using Auth.Handlers.Accounts.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Register([FromBody] UserToRegisterDto userToRegisterDto)
        {
            return await _mediator.Send(new RegisterRequest(userToRegisterDto));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserToLoginDto userToLoginDto)
        {
            return await _mediator.Send(new LoginRequest(userToLoginDto));
        }

        [HttpGet("accountinfo/{userName}")]
        [Authorize]
        public async Task<IActionResult> AccountInfo(string userName)
        {
            return await _mediator.Send(new AccountInfoRequest(userName));
        }

        [HttpPost("changesettings")]
        [Authorize]
        public async Task<IActionResult> ChangeSettings([FromBody] ChangeSettingsDto changeSettingsDto)
        {
            return await _mediator.Send(new ChangeSettingsRequest(changeSettingsDto));
        }

        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            return await _mediator.Send(new ForgotPasswordRequest(forgotPasswordDto));
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            return await _mediator.Send(new ResetPasswordRequest(resetPasswordDto));
        }

        [HttpPost("externallogin")]
        public async Task<IActionResult> ExternalLogin([FromBody] ExternalAuthDto externalAuthDto)
        {
            return await _mediator.Send(new ExternalLoginRequest(externalAuthDto));
        }

        [HttpGet("getall")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            return await _mediator.Send(new GetAllUsersRequest());
        }
    }
}
