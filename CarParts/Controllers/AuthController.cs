using CarParts.Dto.User;
using CarParts.Repoistory.AuthRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CarParts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserDto userDto)
        {
            var result = await _authRepository.Register(userDto);
            if (result is null)
                return BadRequest();
            else
                return Ok(result);
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginUserDto userDto)
        {
            var result = await _authRepository.Login(userDto);
            if (result is null)
                return Unauthorized();
            else
                return Ok(result);
        }
    }
}
