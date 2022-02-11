using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WebApplication3.Model;

namespace WebApplication3.Controllers
{


    

  


    [ApiController]
    [Route("[controller]")]
    public class WeathersForecastController : ControllerBase
    {
        
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"};

        private readonly ILogger<WeathersForecastController> _logger;
        private readonly SpendingAppDbContext _dbContext;


        public WeathersForecastController(ILogger<WeathersForecastController> logger,SpendingAppDbContext context)
        {
            _logger = logger;
            _dbContext = context;
   
        }

        

        [HttpGet(Name = "GetWeatherForecast")]
        public String Get()
        {
            User user = new User() { email = "asd@gmail.com" };
            _dbContext.Remove(user);
            _dbContext.SaveChanges();
            
            return "";
  
        }
    }
}