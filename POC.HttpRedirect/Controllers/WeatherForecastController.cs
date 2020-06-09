using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace POC.HttpRedirect.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public void Get()
        {

            HttpContext.Response.Headers.Clear();
            HttpContext.Response.Headers.Add("Authorization", "Basic dXNlcjpiaXRuYW1p");

            //HttpContext.Response.Headers.Add("Cookie", "JSESSIONID=8C51DD71CD21EA44F72566583DFD220A");


            // HttpContext.Response.Cookies.Append(
            //"JSESSIONID",
            //"8C51DD71CD21EA44F72566583DFD220A",
            //new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(1), HttpOnly = true, Secure = false }
            //     );

            var cookie = "";

            HttpContext.Request.Cookies.TryGetValue("JSESSIONID",out cookie);


            HttpContext.Response.Redirect($"http://127.0.0.1:8080/jasperserver/rest_v2/reportExecutions/{"18229ebc-9089-4f83-84a8-e0d30decf26d"}/exports/{"7a617cf9-082f-4863-bd04-fa6fc6a10be7"}/outputResource");            
        }
    }
}
