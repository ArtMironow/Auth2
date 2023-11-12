using Xunit;
using Moq;
using Microsoft.AspNetCore.Identity;
using DAL.Auth.Models;
using Auth.Features;
using Auth.Handlers.Accounts.Requests;
using Auth.Entities.DataTransferObjects;
using Auth.Handlers.Accounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace Auth.Tests
{
    public class LoginHandlerTests
    {
        [Fact]
        public async Task LoginHandlerResultStatusCodeOk()
        {
            // Arrange
            var userManagerMock = GetUserManager();

            userManagerMock
                .Setup(userManager => userManager.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(GetUser());
            userManagerMock
                .Setup(userManager => userManager.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            var mockJwt = new Mock<IJwtHandler>();
            mockJwt.Setup(jwtHandler => jwtHandler.GenerateToken(It.IsAny<User>())).ReturnsAsync(It.IsAny<string>());

            var userToLoginDto = new UserToLoginDto() { Email = It.IsAny<string>(), Password = It.IsAny<string>() };
            var loginRequest = new LoginRequest(userToLoginDto);
            var handler = new LoginHandler(userManagerMock.Object, mockJwt.Object);

            // Act           
            var result = await handler.Handle(loginRequest, new CancellationToken());

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task LoginHandlerResultStatusCodeUnautorized()
        {
            // Arrange
            var userManagerMock = GetUserManager();

            userManagerMock
                .Setup(userManager => userManager.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(GetUser());
            userManagerMock
                .Setup(userManager => userManager.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            var mockJwt = new Mock<IJwtHandler>();
            mockJwt.Setup(jwtHandler => jwtHandler.GenerateToken(It.IsAny<User>())).ReturnsAsync(It.IsAny<string>());

            var userToLoginDto = new UserToLoginDto() { Email = It.IsAny<string>(), Password = It.IsAny<string>() };
            var loginRequest = new LoginRequest(userToLoginDto);
            var handler = new LoginHandler(userManagerMock.Object, mockJwt.Object);

            // Act           
            var result = await handler.Handle(loginRequest, new CancellationToken());

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
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

        private static User GetUser()
        {
            return new User
            {
                Email = "1@gmail.com",
                Nickname = "Nickname"
            };
        }
    }
}
