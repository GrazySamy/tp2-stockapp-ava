using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using StockApp.Application.DTOs;
using StockApp.Application.Services;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;

namespace StockApp.Domain.Test
{
    public class ServicoAutenticacaoUnitTest
    {
        [Fact]
        public async Task AuthenticateAsync_ValidCredentials_ReturnsToken()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var configurationMock = new Mock<IConfiguration>();

            var authService = new AuthService(userRepositoryMock.Object, configurationMock.Object);

            userRepositoryMock.Setup(repo => repo.GetByUsername("samanta@gmail.com")).ReturnsAsync(new User
            {
                Username = "samanta@gmail.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("fatec@2024"),
                Role = "Admin"
            });

            configurationMock.Setup(config => config["Jwt:SecretKey"]).Returns("samantagrazielaleite@fatec@tp-ava@c#");
            configurationMock.Setup(config => config["Jwt:Issuer"]).Returns("samantafatec");
            configurationMock.Setup(config => config["Jwt:Audience"]).Returns("https://grazysamy.com.br/");
            configurationMock.Setup(config => config["Jwt:ExpiryMinutes"]).Returns("60");

            // Act
            var result = await authService.AuthenticateAsync("samanta@gmail.com", "fatec@2024");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TokenResponseDTO>(result);
        }

        [Fact]
        public async Task AuthenticateAsync_ValidCredentials_ReturnsNull()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var configurationMock = new Mock<IConfiguration>();

            var authService = new AuthService(userRepositoryMock.Object, configurationMock.Object);

            userRepositoryMock.Setup(repo => repo.GetByUsername("samanta@gmail.com")).ReturnsAsync(new User
            {
                Username = "samanta@gmail.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("fatec@2024"),
                Role = "Admin"
            });

            configurationMock.Setup(config => config["Jwt:SecretKey"]).Returns("samantagrazielaleite@fatec@tp-ava@c#");
            configurationMock.Setup(config => config["Jwt:Issuer"]).Returns("samantafatec");
            configurationMock.Setup(config => config["Jwt:Audience"]).Returns("https://grazysamy.com.br/");
            configurationMock.Setup(config => config["Jwt:ExpiryMinutes"]).Returns("60");

            // Act
            var result = await authService.AuthenticateAsync("samanta", "fatec@2024");

            // Assert
            Assert.Null(result);
        }


    }
}
