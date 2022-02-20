using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using WebApplication3.Model;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{

    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        

        private readonly UserService _userService;
        private readonly TokenService _tokenService;
        private readonly SubUserService _subUserService;
        private IConfiguration _config;    


        public UserController(SpendingAppDbContext context,IConfiguration configuration)
        {
         
            _userService = new UserService(context);
            _tokenService = new TokenService(context);
            _subUserService = new SubUserService(context);
            _config = configuration;

        }


        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register()
        {
            String email = HttpContext.Request.Form["email"];
            String password = HttpContext.Request.Form["password"];
            try
            {
                _userService.RegisterUser(email, password);
                String token=_tokenService.CreateAccountVerificationToken(email);
                //send email
                return StatusCode(200, "Success");
            }
            catch(Exception e) {
                return StatusCode(400,e.Message);
            }
            
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login()
        {
            String email = HttpContext.Request.Form["email"];
            String password = HttpContext.Request.Form["password"];
            try
            {
                if (_userService.LogInUser(email, password))
                {
                    return StatusCode(200, GenerateJSONWebToken(email));
                }
                return StatusCode(400, "Unsuccessful login");
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }

        }
        [AllowAnonymous]
        [HttpPost("getResetToken")]
        public IActionResult PasswordReset()
        {
            String email = HttpContext.Request.Form["email"];
            try
            {
                String token=_tokenService.CreatePwRecoveryToken(email);
                //send email
                return StatusCode(200, "Success");
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }

        }
        [AllowAnonymous]
        [HttpPost("resetPassword")]
        public IActionResult Reset()
        {
            String password = HttpContext.Request.Form["password"];
            String token= HttpContext.Request.Form["token"];
            try
            {
                String email=_tokenService.GetEmailByResetToken(token);
                _userService.ChangePassword(email, password);
                return StatusCode(200, "Success");
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }

        }
        [AllowAnonymous]
        [HttpPost("verify")]
        public IActionResult Verify()
        {
            String token = HttpContext.Request.Form["token"];
            try
            {
                String email = _tokenService.GetEmailByVerificationToken(token);
                _userService.VerifyUser(email);
                return StatusCode(200, "Success");
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }


        private string GenerateJSONWebToken(String email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            string jsonString = JsonSerializer.Serialize(_subUserService.GetSubUsersByID(email));
            var claims = new Claim[]
            {
                new Claim("email", email),
                new Claim("subusers",jsonString)
            };
            var token = new JwtSecurityToken(
              issuer: _config["Jwt:Issuer"],
              audience: _config["Jwt:Issuer"],
              claims: claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);
                
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        
        [HttpGet("getToken")]
        [Authorize(Policy = "email")]
        public int TestAuth()
        {
            var email = User.FindFirst("email")?.Value;
            var subusers = User.FindFirst("subusers")?.Value;
            var asd=JsonSerializer.Deserialize<int[]>(subusers);
            return asd[0];
        }


    }
}