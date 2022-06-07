using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestAPICompanyDemo.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestAPICompanyDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly DBContextCompany _context;

        public AuthController(IConfiguration configuration, DBContextCompany context)
        {
            _configuration = configuration;
            _context = context;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(string name, string password)
        {
            var user = await GetUser(name, password);
            if (user != null)
            {
                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.UserId + ""),
                        new Claim("UserName", user.UserName),
                        new Claim("EmployeeId", user.CurentEmployeeId + ""),
                    };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> registerUser(User user)
        {
            var newUser = new User
            {
                UserName = user.UserName,
                Employee = null,
                CreatedDate = DateTime.UtcNow,
                CurentEmployeeId = user.CurentEmployeeId,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role,
                UserId = user.UserId,
            };
            _context.users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(newUser);
        }
        private async Task<User> GetUser(string email, string password)
        {
            return await _context.users.FirstOrDefaultAsync(u => u.UserName == email && u.Password == password);
        }
    }
}
