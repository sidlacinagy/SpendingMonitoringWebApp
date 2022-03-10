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
        public IActionResult AddSpending([FromForm] String product, [FromForm] String productCategory, [FromForm] int price,
            [FromForm] String date, [FromForm] String subuserid)
        {
            

            DateTime dateTime = DateTime.Parse(date);
            var subUsersString = User.FindFirst("subusers")?.Value;
            String[]? subusers = JsonSerializer.Deserialize<String[]>(subUsersString);
            if (!subusers.Contains(subuserid))
            {
                return StatusCode(403, JsonSerializer.Serialize("Invalid subuserid"));
            }
            try
            {
                _spendingService.AddSpendingToSubUser(subuserid, product, productCategory, price, dateTime);
                return StatusCode(200, JsonSerializer.Serialize("Success"));
            }
            catch (Exception e)
            {
                return StatusCode(400, JsonSerializer.Serialize(e.Message));
            }

        }


        [Authorize(Policy = "subusers")]
        [HttpPost("update")]
        public IActionResult UpdateSpending([FromForm] String product, [FromForm] String productCategory, [FromForm] int price,
            [FromForm] String date, [FromForm] String spendingId)
        {

            DateTime dateTime = DateTime.Parse(HttpContext.Request.Form["date"]);
            var email = User.FindFirst("email")?.Value;
            try
            {
                _spendingService.UpdateSpendingById(email,spendingId,product,productCategory,price,dateTime);
                return StatusCode(200, JsonSerializer.Serialize("Success"));
            }
            catch (Exception e)
            {
                return StatusCode(400, JsonSerializer.Serialize(e.Message));
            }

        }

        [Authorize(Policy = "subusers")]
        [HttpPost("remove")]
        public IActionResult RemoveSpending([FromForm] String spendingId)
        {
            var email = User.FindFirst("email")?.Value;
            try
            {
                _spendingService.RemoveSpendingById(email,spendingId);
                return StatusCode(200, JsonSerializer.Serialize("Success"));
            }
            catch (Exception e)
            {
                return StatusCode(400, JsonSerializer.Serialize(e.Message));
            }

        }

        [Authorize(Policy = "subusers")]
        [HttpPost("subuser")]
        public IActionResult GetSubUserSpending([FromForm] String subUserId)
        {
            var subUsersString = User.FindFirst("subusers")?.Value;
            String[]? subusers = JsonSerializer.Deserialize<String[]>(subUsersString);
            if (subusers.Contains(subUserId))
            {
                return StatusCode(400, JsonSerializer.Serialize("Invalid subuser"));
            }

            return StatusCode(200, JsonSerializer.Serialize(_spendingService.GetAllSpendingBySubUser(subUserId)));

        }

        [Authorize(Policy = "subuser")]
        [HttpGet("user")]
        public IActionResult GetUserSpending()
        {

            var email = User.FindFirst("email")?.Value;
            return StatusCode(200, JsonSerializer.Serialize(_spendingService.GetAllSpendingByUser(email)));

        }

    }
}