using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
                return StatusCode(200, _subUserService.GetSubUsersByID(email));
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }

        }

        [Authorize(Policy = "email")]
        [HttpPost("remove")]
        public IActionResult RemoveSubUser()
        {
            int id = Int32.Parse(HttpContext.Request.Form["subuserid"]);
            try
            {
                var email = User.FindFirst("email")?.Value;
                if (!_subUserService.IsSubUserBelongToEmail(id, email))
                {
                    return StatusCode(401);
                }
                _subUserService.DeleteSubUserByID(id);
                return StatusCode(200, "Success");
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }

        }

    }
}