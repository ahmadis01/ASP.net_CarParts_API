using CarParts.SqlServer.DataBase;
using CarParts.Dto.User;
using CarParts.Models.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarParts.Repoistory.AuthRepository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly CarPartContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;

        public AuthRepository(CarPartContext context, UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = configuration;
        }
        public Task<User> CurrentUser()
        {
            throw new NotImplementedException();
        }

        public async Task<string> Login(LoginUserDto userDto)
        {
            User user = await _userManager.FindByNameAsync(userDto.UserName);
            var result = await _signInManager.PasswordSignInAsync(userDto.UserName, userDto.Password, false, true);
            if (result.Succeeded)
            {
                var token = CreateToken(user);
                return token;
            }
            else
                return null;
        }

        public void Logout()
        {
            _signInManager.SignOutAsync();
        }

        public async Task<string> Register(RegisterUserDto userDto)
        {
            User user = new User();
            user.UserName = userDto.UserName;
            user.PhoneNumber = userDto.PhonNumber;
            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (result.Succeeded)
                return CreateToken(user);
            else
                return null;
        }
        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _config.GetSection("JwtKey").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
