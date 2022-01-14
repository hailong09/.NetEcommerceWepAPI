using backend.Dtos;
using backend.Extensions;
using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{

    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository repository;
        // private readonly IConfiguration configuration;
        private readonly IJWTRepository jwt;

        public UserController(IUserRepository repository, IJWTRepository jwt)
        {
            this.repository = repository;
            this.jwt = jwt;
           

        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> RegisterAsync([FromBody] RegisterDto registerDto)
        {
            if (registerDto is null)
            {
                return BadRequest("User infomation may not correct!");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            User user = new()
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Password = passwordHash,
                CreateDate = DateTime.UtcNow
            };

            await repository.Register(user);
            var token = jwt.CreateToken(user);
            return CreatedAtAction(nameof(RegisterAsync), new { id = user.Id }, new
            {
                token,
            });
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginAsync([FromBody] LoginDto loginDto)
        {

            var user = await repository.GetUser(loginDto.Email);
            if (user is null)
            {
                return Unauthorized("Password/Email is not correct!");
            }

            bool isVerified = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);
            if (!isVerified)
            {
                return Unauthorized("Password/Email is not correct!");
            }

            var token = jwt.CreateToken(user);

            return token;

        }




    }
}