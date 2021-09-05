using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using distribution_plaza.Data.Repository;
using distribution_plaza.Dtos;
using distribution_plaza.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace distribution_plaza.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthRepository repo,IConfiguration configuration)
        {
            _repo = repo;
            _configuration = configuration;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userDto)
        {
            userDto.UserName = userDto.UserName.ToLower();
            if (await _repo.UserExists(userDto.UserName))
                return BadRequest("Username already exists");
            var userToCreate = new User
            {
                UserName = userDto.UserName,
                UserRole = userDto.UserRole,
                SelectedBay = userDto.SelectedBay
            };
            await _repo.Register(userToCreate, userDto.Password);
            return StatusCode(201);
        }
        [HttpPost("login")]

        public async Task<IActionResult> Login(LoginDto dto)
        {
            var userFromRepo = await _repo.Login(dto.UserName, dto.Password);
            if (userFromRepo==null)
                return Unauthorized();
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.UserName),
            };
            var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor=new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler=new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new
            {
                token=tokenHandler.WriteToken(token)
            });
        }
    }
}