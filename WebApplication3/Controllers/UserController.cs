using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebApplication3.Model;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{

    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        

        private readonly UserService _userService;
        private readonly TokenService _tokenService;


        public UserController(SpendingAppDbContext context)
        {
         
            _userService = new UserService(context);
            _tokenService = new TokenService(context);

        }


        
        [HttpPost("register")]
        public IActionResult Register()
        {
            String email = HttpContext.Request.Form["email"];
            String password = HttpContext.Request.Form["password"];
            try
            {
                _userService.RegisterUser(email, password);
                _tokenService.CreateAccountVerificationToken(email);
                //send email
                return StatusCode(200, "Success");
            }
            catch(Exception e) {
                return StatusCode(400,e.Message);
            }
            
        }
        
        [HttpPost("login")]
        public IActionResult Login()
        {
            String email = HttpContext.Request.Form["email"];
            String password = HttpContext.Request.Form["password"];
            try
            {
                if (_userService.LogInUser(email, password))
                {
                    return StatusCode(200, "Success");
                }
                return StatusCode(400, "Unsuccessful login");
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }

        }

        [HttpPost("getResetToken")]
        public IActionResult PasswordReset()
        {
            String email = HttpContext.Request.Form["email"];
            try
            {
                _tokenService.CreatePwRecoveryToken(email);
                //send email
                return StatusCode(200, "Success");
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }

        }

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


    }
}