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

    [Route("api/spending")]
    [ApiController]
    public class SpendingController : Controller
    {



        private readonly SpendingService _spendingService;
        private IConfiguration _config;


        public SpendingController(SpendingAppDbContext context, IConfiguration configuration)
        {

            _spendingService = new SpendingService(context);
            _config = configuration;

        }


        [Authorize(Policy = "subusers")]
        [HttpPost("add")]
        public IActionResult AddSpending()
        {
            
            String product = HttpContext.Request.Form["product"];
            String productCategory = HttpContext.Request.Form["productCategory"];
            int price = Int32.Parse(HttpContext.Request.Form["price"]);
            DateTime date = DateTime.Parse(HttpContext.Request.Form["date"]);
            String subuserid = HttpContext.Request.Form["subuserid"];

            var subUsersString = User.FindFirst("subusers")?.Value;
            String[]? subusers = JsonSerializer.Deserialize<String[]>(subUsersString);
            if (!subusers.Contains(subuserid))
            {
                return StatusCode(403, "Invalid subuserid");
            }


            try
            {
                _spendingService.AddSpendingToSubUser(subuserid, product, productCategory, price, date);
                //send email
                return StatusCode(200, "Success");
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }

        }


        [Authorize(Policy = "subusers")]
        [HttpPost("update")]
        public IActionResult UpdateSpending()
        {

            String product = HttpContext.Request.Form["product"];
            String productCategory = HttpContext.Request.Form["productCategory"];
            int price = Int32.Parse(HttpContext.Request.Form["price"]);
            DateTime date = DateTime.Parse(HttpContext.Request.Form["date"]);
            String spendingId = HttpContext.Request.Form["spendingid"];

            var email = User.FindFirst("email")?.Value;


            try
            {
                _spendingService.UpdateSpendingById(email,spendingId,product,productCategory,price,date);
                //send email
                return StatusCode(200, "Success");
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }

        }

        [Authorize(Policy = "subusers")]
        [HttpPost("remove")]
        public IActionResult RemoveSpending()
        {

            String spendingId = HttpContext.Request.Form["spendingid"];
            var email = User.FindFirst("email")?.Value;

            try
            {
                _spendingService.RemoveSpendingById(email,spendingId);
                //send email
                return StatusCode(200, "Success");
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }

        }

        [Authorize(Policy = "subusers")]
        [HttpPost("subuser")]
        public IActionResult GetSubUserSpending()
        {

            String subUserId = HttpContext.Request.Form["subuserid"];
            var subUsersString = User.FindFirst("subusers")?.Value;
            String[]? subusers = JsonSerializer.Deserialize<String[]>(subUsersString);
            if (subusers.Contains(subUserId))
            {
                return StatusCode(400, "Invalid subuser");
            }

            return StatusCode(200, _spendingService.GetAllSpendingBySubUser(subUserId));

        }

        [Authorize(Policy = "subuser")]
        [HttpPost("user")]
        public IActionResult GetUserSpending()
        {

            var email = User.FindFirst("email")?.Value;
            

            return StatusCode(200, _spendingService.GetAllSpendingByUser(email));

        }



    }
}