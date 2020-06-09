using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var baseAddress = new Uri("http://127.0.0.1:8080");
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            {
                client.DefaultRequestHeaders.Add("Authorization", "Basic dXNlcjpiaXRuYW1p");

                cookieContainer.Add(baseAddress, new Cookie("JSESSIONID", "E29A51927750105B0492B7F5B15E2DE2"));

                var result = await client.GetAsync($"/jasperserver/rest_v2/reportExecutions/1ef68a16-6990-4619-acf9-0c00b309487d/exports/c0be5ecb-68ed-4aeb-87da-ae7e00eef4c7/outputResource");

                if (result.IsSuccessStatusCode)
                {
                    return File(await result.Content.ReadAsByteArrayAsync(), "application/pdf", "nogueiranervosinho.pdf");
                }

            }
            return default;           



        }
    }
}
