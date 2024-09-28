using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyWebAPI.Data;
using MyWebAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly AppSettings _appSettings;

        public UserController(MyDbContext context, IOptionsMonitor<AppSettings> optionsMonitor)
        {
            _context = context;
            _appSettings = optionsMonitor.CurrentValue;
        }

        [HttpPost("Login")]
        public IActionResult Validate(LoginModel model)
        {
            var user = _context.NguoiDungs.SingleOrDefault(u =>u.UserName == model.UserName && u.Password==model.Password);
            if (user == null)// k có ng dùng
            {
                return Ok(new ApiRepose
                {
                    Success = false,
                    Message= "Invalid username/password"
                });
            }
            //cấp token

            return Ok(new ApiRepose
            {
                Success= true,
                Message= "Authenticate sucees",
                Data = GenerateToken(user)
            });
        }

        private string GenerateToken(NguoiDung nguoiDung)
        {
            var jwtToken = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name,nguoiDung.HoTen),
                    new Claim(ClaimTypes.Email,nguoiDung.Email),
                    new Claim("UserName",nguoiDung.UserName),
                    new Claim("iD",nguoiDung.Id.ToString()),

                    //roles
                    new Claim("TokenId", Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),SecurityAlgorithms.HmacSha256Signature)

            };
            var token = jwtToken.CreateToken(tokenDescription);
            return jwtToken.WriteToken(token);

        }

    }
}
