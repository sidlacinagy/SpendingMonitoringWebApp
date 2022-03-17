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

    [Route("api/subuser")]
    [ApiController]
    public class SubUserController : Controller
    {


        private readonly SubUserService _subUserService;
        private readonly TokenService _tokenService;
        private IConfiguration _config;


        public SubUserController(SpendingAppDbContext context, IConfiguration configuration)
        {

            _tokenService = new TokenService(context);
            _subUserService = new SubUserService(context);
            _config = configuration;

        }


        [Authorize(Policy = "email")]
        [HttpPost("add")]
        public IActionResult AddSubUser([FromForm] String name)
        {
            try
            {
                var email = User.FindFirst("email")?.Value;
                _subUserService.CreateSubUserForEmail(email, name);

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
            catch (Exception e)
            {
                return StatusCode(400, JsonSerializer.Serialize(e.Message));
            }

        }

        [Authorize(Policy = "subusers")]
        [HttpGet("get-all")]
        public IActionResult GetAllSubuser()
        {
            try
            {
                var email = User.FindFirst("email")?.Value;
                SubUser[] subusers=_subUserService.GetSubUsersByID(email);
                return StatusCode(200, subusers);
            }
            catch (Exception e)
            {
                return StatusCode(400, JsonSerializer.Serialize(e.Message));
            }

        }

        [Authorize(Policy = "subusers")]
        [HttpPost("delete")]
        public IActionResult RemoveSubUser([FromForm] String subuserid)
        {
            var email = User.FindFirst("email")?.Value;
            var subUsersString = User.FindFirst("subusers")?.Value;
            String[]? subusers = JsonSerializer.Deserialize<String[]>(subUsersString);
            if (!subusers.Contains(subuserid))
            {
                return StatusCode(403, JsonSerializer.Serialize("Invalid subuserid"));
            }
            try
            {
                _subUserService.DeleteSubUserByID(subuserid);
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
            catch (Exception e)
            {
                return StatusCode(400, JsonSerializer.Serialize(e.Message));
            }

        }

        [Authorize(Policy = "subusers")]
        [HttpPost("rename")]
        public IActionResult RenameSubUser([FromForm] String subuserid, [FromForm] String name)
        {
            var email = User.FindFirst("email")?.Value;
            var subUsersString = User.FindFirst("subusers")?.Value;
            String[]? subusers = JsonSerializer.Deserialize<String[]>(subUsersString);
            if (!subusers.Contains(subuserid))
            {
                return StatusCode(403, JsonSerializer.Serialize("Invalid subuserid"));
            }
            try
            {
                _subUserService.RenameSubUserByID(subuserid, name);
                return StatusCode(200, JsonSerializer.Serialize("Success"));

            }
            catch (Exception e)
            {
                return StatusCode(400, JsonSerializer.Serialize(e.Message));
            }

        }

        private string GenerateJSONWebToken(String email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            string jsonString = JsonSerializer.Serialize(_subUserService.GetSubUsersIdByID(email));
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

    }
}