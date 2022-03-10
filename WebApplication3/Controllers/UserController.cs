using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using WebApplication3.Model;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{

    [Route("api/user")]
    [ApiController]
    [EnableCors]
    public class UserController : Controller
    {
        

        private readonly UserService _userService;
        private readonly TokenService _tokenService;
        private readonly SubUserService _subUserService;
        private IConfiguration _config;
        private SmtpClient _smtpClient;


        public UserController(SpendingAppDbContext context,IConfiguration configuration)
        {
         
            _userService = new UserService(context);
            _tokenService = new TokenService(context);
            _subUserService = new SubUserService(context);
            _config = configuration;
            _smtpClient = new SmtpClient(_config["Smtp:Host"])
            {
                Port = int.Parse(_config["Smtp:Port"]),
                Credentials = new NetworkCredential(_config["Smtp:Username"], _config["Smtp:Password"]),
                EnableSsl = true,
            };

        }


        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromForm] String email, [FromForm] String password)
        {
            try
            {
                _userService.RegisterUser(email, password);
                String token=_tokenService.CreateAccountVerificationToken(email);
                MailMessage mailMessage = new MailMessage();
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = "SUBJECT";
                string body = System.IO.File.ReadAllText(Directory.GetCurrentDirectory() + "/Templates/VerifyEmail.html");
                body = body.Replace("%2$s", token);
                mailMessage.Body = body;
                mailMessage.From = new MailAddress("spendingapptest@gmail.com", "SpendingApp");
                mailMessage.To.Add(new MailAddress(email, email));
                _smtpClient.Send(mailMessage);
                return StatusCode(200, JsonSerializer.Serialize("Success"));
            }
            catch(Exception e) {
                return StatusCode(400, JsonSerializer.Serialize(e.Message));
            }
            
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromForm] String email, [FromForm] String password)
        {
            try
            {
                if (_userService.LogInUser(email, password))
                {
                    CookieOptions jwtoptions = new CookieOptions();
                    jwtoptions.Expires = DateTime.Now.AddDays(7);
                    jwtoptions.HttpOnly = true;
                    string JWTToken = GenerateJSONWebToken(email);
                    Response.Cookies.Append("auth-token", JWTToken, jwtoptions);
                    string refreshtoken = _tokenService.GenerateRefreshToken(JWTToken);
                    CookieOptions refreshoptions = new CookieOptions();
                    refreshoptions.Expires = DateTime.Now.AddDays(7);
                    refreshoptions.HttpOnly = true;
                    refreshoptions.Path = "/api/user/refreshtoken";
                    Response.Cookies.Append("refresh-token", refreshtoken, refreshoptions);
                    return Ok(JsonSerializer.Serialize("Success"));
                }
                return StatusCode(400, JsonSerializer.Serialize("Unsuccessful login"));
            }
            catch (Exception e)
            {
                return StatusCode(400, JsonSerializer.Serialize(e.Message));
            }

        }
        [AllowAnonymous]
        [HttpPost("getResetToken")]
        public IActionResult PasswordReset([FromForm] String email)
        {
            try
            {
                String token=_tokenService.CreatePwRecoveryToken(email);
                MailMessage mailMessage = new MailMessage();
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = "SUBJECT";
                string body = System.IO.File.ReadAllText(Directory.GetCurrentDirectory()+"/Templates/ResetEmail.html");
                body=body.Replace("%1$s", token);
                mailMessage.Body = body;
                mailMessage.From = new MailAddress("spendingapptest@gmail.com", "SpendingApp");
                mailMessage.To.Add(new MailAddress(email, email));
                _smtpClient.Send(mailMessage);
                return StatusCode(200, JsonSerializer.Serialize("Success"));
            }
            catch (Exception e)
            {
                return StatusCode(400, JsonSerializer.Serialize(e.Message));
            }

        }
        [AllowAnonymous]
        [HttpPost("resetPassword")]
        public IActionResult Reset([FromForm] String password, [FromForm] String token)
        {
            try
            {
                String email=_tokenService.GetEmailByResetToken(token);
                _userService.ChangePassword(email, password);
                return StatusCode(200, JsonSerializer.Serialize("Success"));
            }
            catch (Exception e)
            {
                return StatusCode(400, JsonSerializer.Serialize(e.Message));
            }

        }
        [AllowAnonymous]
        [HttpPost("verify")]
        public IActionResult Verify([FromForm] String token)
        {
            try
            {
                String email = _tokenService.GetEmailByVerificationToken(token);
                _userService.VerifyUser(email);
                return StatusCode(200, JsonSerializer.Serialize("Success"));
            }
            catch (Exception e)
            {
                return StatusCode(400, JsonSerializer.Serialize(e.Message));
            }
        }
        [AllowAnonymous]
        [HttpGet("refreshtoken")]
        public IActionResult RefreshToken()
        {
            string? usedJWTToken = HttpContext.Request.Cookies["auth-token"];
            string? refreshToken = HttpContext.Request.Cookies["refresh-token"];
            //var email = User.FindFirst("email")?.Value;
            if (usedJWTToken==null || refreshToken==null)
            {
                return StatusCode(403, "Invalid/no token");
            }
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadToken(usedJWTToken) as JwtSecurityToken;
            var email = token.Claims.First(claim => claim.Type == "email").Value;
            if(email==null)
            {
                return StatusCode(403, JsonSerializer.Serialize("Invalid/no token"));
            }
            if (_tokenService.UseRefreshToken(usedJWTToken, refreshToken))
            {
                CookieOptions jwtoptions = new CookieOptions();
                jwtoptions.Expires = DateTime.Now.AddDays(7);
                jwtoptions.HttpOnly = true;
                string JWTToken = GenerateJSONWebToken(email);
                Response.Cookies.Append("auth-token", JWTToken, jwtoptions);

                string refreshtoken = _tokenService.GenerateRefreshToken(JWTToken);
                CookieOptions refreshoptions = new CookieOptions();
                refreshoptions.Expires = DateTime.Now.AddDays(7);
                refreshoptions.HttpOnly = true;
                refreshoptions.Path = "/api/user/refreshtoken";
                Response.Cookies.Append("refresh-token", refreshtoken, refreshoptions);
                return StatusCode(200, JsonSerializer.Serialize("Success"));
            }

            return StatusCode(400, JsonSerializer.Serialize("Invalid tokens"));
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
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);
                
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        
        [HttpGet("getToken")]
        [Authorize(Policy = "email")]
        public IActionResult TestAuth()
        {
            var email = User.FindFirst("email")?.Value;
            var subusers = User.FindFirst("subusers")?.Value;
            var asd=JsonSerializer.Deserialize<String[]>(subusers);
            return StatusCode(200, asd);
        }

    }
}