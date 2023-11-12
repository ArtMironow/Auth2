using MediatR;
using Auth.Handlers.Accounts.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using DAL.Auth.Models;
using Serilog;
using Auth.Entities.DataTransferObjects;

namespace Auth.Handlers.Accounts
{
    public class RegisterHandler : ControllerBase, IRequestHandler<RegisterRequest, IActionResult> 
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public RegisterHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request.userToRegisterDto);
            var result = await _userManager.CreateAsync(user, request.userToRegisterDto.Password);

            if (!result.Succeeded)
            {
                Log.Warning("Identity. Create user failed.");

                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new ResponseDto
                {
                    IsSuccess = false,
                    Errors = errors,
                    Data = null
                });
            }

            return StatusCode(201);
        }
    }
}
