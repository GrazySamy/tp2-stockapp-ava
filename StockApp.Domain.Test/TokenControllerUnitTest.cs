using CleanArchMvc.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StockApp.API.Controllers;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.Domain.Test
{
    public class TokenControllerUnitTest
    {

        [Fact]
        public async Task Login_ValidCredentials_ReturnsOk()
        {
            // Arrange
            var authServiceMock = new Mock<IAuthService>();
            var tokenController = new TokenController(authServiceMock.Object);

            authServiceMock.Setup(service => service.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new TokenResponseDTO
            {
                Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoic2FtYW50YUBnbWFpbC5jb20iLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE3MTkxOTM5NjMsImV4cCI6MTcxOTE5NzU2MywiaWF0IjoxNzE5MTkzOTYzfQ.RbN6Id6lxaGaXdyWVDIGcsVaKZp9IC1cSG1LsYRw9K0",
                Expiration = DateTime.UtcNow.AddMinutes(60)
            });

            var userLoginDto = new UserLoginDTO
            {
                Username = "samanta@gmail.com",
                Password = "fatec@2024"
            };

            // Act
            var result = await tokenController.Login(userLoginDto) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<TokenResponseDTO>(result.Value);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var authServiceMock = new Mock<IAuthService>();
            var tokenController = new TokenController(authServiceMock.Object);

            authServiceMock.Setup(service => service.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new UnauthorizedAccessException());

            var userLoginDto = new UserLoginDTO
            {
                Username = "samanta",
                Password = "fatec@2024"
            };

            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => tokenController.Login(userLoginDto));
            
        }
    }
}
