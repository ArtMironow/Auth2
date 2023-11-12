using MediatR;
using Auth.Handlers.Accounts.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DAL.Auth.Models;
using Auth.Entities.DataTransferObjects;
using Auth.Features;

namespace Auth.Handlers.Accounts
{
    public class ExternalLoginHandler : ControllerBase, IRequestHandler<ExternalLoginRequest, IActionResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtHandler _jwtHandler;

        public ExternalLoginHandler(UserManager<User> userManager, IJwtHandler jwtHandler)
        {
            _userManager = userManager;
            _jwtHandler = jwtHandler;
        }

        public async Task<IActionResult> Handle(ExternalLoginRequest request, CancellationToken cancellationToken)
        {
            UserLoginInfo? info = null;
            var email = String.Empty;

            if (request.externalAuthDto.Provider == "FACEBOOK")
            {
                var facebookAccountDto = await _jwtHandler.VerifyFacebookToken(request.externalAuthDto);

                if (facebookAccountDto == null)
                    return BadRequest(new ResponseDto
                    {
                        IsSuccess = false,
                        Errors = new List<string> { "Invalid External Authentication." },
                        Data = null
                    });

                info = new UserLoginInfo(request.externalAuthDto.Provider, facebookAccountDto.Id, request.externalAuthDto.Provider);
                email = facebookAccountDto.Email;

            }

            if (request.externalAuthDto.Provider == "GOOGLE")
            {
                var payload = await _jwtHandler.VerifyGoogleToken(request.externalAuthDto);

                if (payload == null)
                {
                    return BadRequest(new ResponseDto
                    {
                        IsSuccess = false,
                        Errors = new List<string> { "Invalid External Authentication." },
                        Data = null
                    });
                }

                info = new UserLoginInfo(request.externalAuthDto.Provider, payload.Subject, request.externalAuthDto.Provider);
                email = payload.Email;
            }

            if (info == null && email == String.Empty)
            {
                return BadRequest(new ResponseDto
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Invalid External Authentication." },
                    Data = null
                });
            }

            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    user = new User { Email = email, UserName = email, Nickname = email };
                    await _userManager.CreateAsync(user);
                    await _userManager.AddLoginAsync(user, info);
                }
                else
                {
                    await _userManager.AddLoginAsync(user, info);
                }
            }

            if (user == null)
            {
                return BadRequest(new ResponseDto
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Invalid External Authentication." },
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
