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
        private IConfiguration _config;


        public SubUserController(SpendingAppDbContext context, IConfiguration configuration)
        {

            _subUserService = new SubUserService(context);
            _config = configuration;

        }


        [Authorize(Policy = "email")]
        [HttpPost("add")]
        public IActionResult AddSubUser()
        {
            String name = HttpContext.Request.Form["name"];
            try
            {
                var email = User.FindFirst("email")?.Value;
                _subUserService.CreateSubUserForEmail(email, name);
                return StatusCode(200, GenerateJSONWebToken(email));
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }

        }

        [Authorize(Policy = "subusers")]
        [HttpPost("remove")]
        public IActionResult RemoveSubUser()
        {
            String id = HttpContext.Request.Form["subuserid"];
            var email = User.FindFirst("email")?.Value;
            var subUsersString = User.FindFirst("subusers")?.Value;
            String[]? subusers = JsonSerializer.Deserialize<String[]>(subUsersString);
            if (!subusers.Contains(id))
            {
                return StatusCode(403, "Invalid subuserid");
            }
            try
            {
                _subUserService.DeleteSubUserByID(id);
                return StatusCode(200, GenerateJSONWebToken(email));
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }

        }

        [Authorize(Policy = "subusers")]
        [HttpPost("rename")]
        public IActionResult RenameSubUser()
        {
            String id = HttpContext.Request.Form["subuserid"];
            String name = HttpContext.Request.Form["name"];
            var email = User.FindFirst("email")?.Value;
            var subUsersString = User.FindFirst("subusers")?.Value;
            String[]? subusers = JsonSerializer.Deserialize<String[]>(subUsersString);
            if (!subusers.Contains(id))
            {
                return StatusCode(403, "Invalid subuserid");
            }
            try
            {
                _subUserService.RenameSubUserByID(id,name);
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

    }
}