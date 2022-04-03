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
 
            DateTime dateTime = DateTime.Parse(date).AddHours(6);
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
                System.Console.WriteLine(e.Message);
                return StatusCode(400, JsonSerializer.Serialize(e.Message));
            }

        }

        [Authorize(Policy = "subusers")]
        [HttpPost("get-categories")]
        public IActionResult GetCategories([FromForm] String subuserid)
        {
            var subUsersString = User.FindFirst("subusers")?.Value;
            String[]? subusers = JsonSerializer.Deserialize<String[]>(subUsersString);
            if (!subusers.Contains(subuserid))
            {
                return StatusCode(403, JsonSerializer.Serialize("Invalid subuserid"));
            }
            try
            { 
                return StatusCode(200, JsonSerializer.Serialize(_spendingService.GetCategories(subuserid)));
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

            DateTime dateTime = DateTime.Parse(date);
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
        [HttpPost("delete")]
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

        [Authorize(Policy = "subusers")]
        [HttpPost("get-spending-query")]
        public IActionResult GetSubUserSpendingByQuery([FromForm] String subuserid, [FromForm] String mindate, [FromForm] String maxdate,
            [FromForm] int minprice, [FromForm] int maxprice, [FromForm] List<String> categories, [FromForm] int page,[FromForm] String orderby)
        {
            DateTime mindateTime = DateTime.Parse(mindate).AddHours(6);
            DateTime maxdateTime = DateTime.Parse(maxdate).AddHours(6);
            var subUsersString = User.FindFirst("subusers")?.Value;
            var email = User.FindFirst("email")?.Value;
            String[]? subusers = JsonSerializer.Deserialize<String[]>(subUsersString);
            if (!subusers.Contains(subuserid))
            {
                return StatusCode(400, JsonSerializer.Serialize("Invalid subuser"));
            }

            return StatusCode(200, JsonSerializer.Serialize(_spendingService.GetAllSpendingByQuery(subuserid,mindateTime,maxdateTime,minprice,
                maxprice,categories,page,orderby)));

        }

        [Authorize(Policy = "subusers")]
        [HttpPost("get-spendingstatistic-query")]
        public IActionResult GetSubUserSpendingStatisticByQuery([FromForm] String mindate, [FromForm] String maxdate,
           [FromForm] int minprice, [FromForm] int maxprice, [FromForm] List<String> categories, [FromForm] List<String> subusersFilter, [FromForm] String groupby)
        {
            DateTime mindateTime = DateTime.Parse(mindate).AddHours(6);
            DateTime maxdateTime = DateTime.Parse(maxdate).AddHours(6);
            var subUsersString = User.FindFirst("subusers")?.Value;
            List<String>? subusers = JsonSerializer.Deserialize<List<String>>(subUsersString);
            if (subusersFilter.Count > 0)
            {
                List<String> realsubusers = new List<String>();
                foreach (var subuser in subusersFilter)
                {
                    if (subusers.Contains(subuser))
                    {
                        realsubusers.Add(subuser);
                    }
                }
                subusers = realsubusers;
            }
            
            return StatusCode(200, Newtonsoft.Json.JsonConvert.SerializeObject(_spendingService.GetAllSpendingStatisticsByQuery(subusers, mindateTime, maxdateTime, minprice,
                maxprice, categories, groupby)));

        }

        [Authorize(Policy = "subusers")]
        [HttpPost("get-spending")]
        public IActionResult GetSubUserSpendingByQuery([FromForm] String subuserid, [FromForm] int page)
        {
            var subUsersString = User.FindFirst("subusers")?.Value;
            String[]? subusers = JsonSerializer.Deserialize<String[]>(subUsersString);
            if (subusers.Contains(subuserid))
            {
                return StatusCode(400, JsonSerializer.Serialize("Invalid subuser"));
            }

            return StatusCode(200, JsonSerializer.Serialize(_spendingService.GetAllSpendingBySubUserAndPage(subuserid, page)));

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