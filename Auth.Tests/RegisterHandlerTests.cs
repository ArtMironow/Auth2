using Auth.Entities.DataTransferObjects;
using Auth.Handlers.Accounts;
using Auth.Handlers.Accounts.Requests;
using AutoMapper;
using DAL.Auth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Auth.Tests
{
    public class RegisterHandlerTests
    {
        [Fact]
        public async Task RegisterHandlerResultStatusCode201()
        {
            // Arrange
            var userManagerMock = GetUserManager();

            SetUserManagerReturnValue(userManagerMock, IdentityResult.Success);

            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(mapper => mapper.Map<User>(It.IsAny<UserToRegisterDto>()));

            var userToRegisterDto = GetUserToRegisterDto();

            var registerRequest = new RegisterRequest(userToRegisterDto);
            var handler = new RegisterHandler(userManagerMock.Object, mapperMock.Object);

            // Act
            var result = await handler.Handle(registerRequest, new CancellationToken());

            // Assert
            Assert.IsType<StatusCodeResult>(result);
        }

        [Fact]
        public async Task RegisterHandlerResultBadRequest()
        {
            var userManagerMock = GetUserManager();

            SetUserManagerReturnValue(userManagerMock, IdentityResult.Failed());

            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(mapper => mapper.Map<User>(It.IsAny<UserToRegisterDto>()));

            var userToRegisterDto = GetUserToRegisterDto();

            var registerRequest = new RegisterRequest(userToRegisterDto);
            var handler = new RegisterHandler(userManagerMock.Object, mapperMock.Object);

            // Act
            var result = await handler.Handle(registerRequest, new CancellationToken());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        private static Mock<UserManager<User>> GetUserManager()
        {
            return new Mock<UserManager<User>>(
            new Mock<IUserStore<User>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<IPasswordHasher<User>>().Object,
            new IUserValidator<User>[0],
            new IPasswordValidator<User>[0],
            new Mock<ILookupNormalizer>().Object,
            new Mock<IdentityErrorDescriber>().Object,
            new Mock<IServiceProvider>().Object,
            new Mock<ILogger<UserManager<User>>>().Object);
        }

        private static UserToRegisterDto GetUserToRegisterDto()
        {
            return new UserToRegisterDto()
            {
                Email = It.IsAny<string>(),
                Password = It.IsAny<string>(),
                ConfirmPassword = It.IsAny<string>()
            };
        }

        private static void SetUserManagerReturnValue(Mock<UserManager<User>> userManagerMock, IdentityResult identityResult)
        {
            userManagerMock
                .Setup(userManager => userManager.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(identityResult);
        }
    }
}
