using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO userRegisterDto)
        {
            var registeredUser = (await _userRepository.GetByUsername(userRegisterDto.Username) != null);
            if (registeredUser)
            {
                return BadRequest("Usuário já registrado");
            }

            var user = new User
            {
                Username = userRegisterDto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userRegisterDto.Password),
                Role = userRegisterDto.Role
            };

            await _userRepository.Add(user);
            return Ok();
        }
    }
}
