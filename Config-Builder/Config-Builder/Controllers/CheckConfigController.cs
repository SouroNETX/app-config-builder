using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Config_Builder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckConfigController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CheckConfigController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("JsonFromApp")]

        public IActionResult DataFromApp()
        {
            var appConfig = Services.AppConfig.GetAppConfig.JsonFromApp();

            return Ok(appConfig);
        }

        [HttpGet("JsonFromDatabase")]

        public IActionResult DataFromDatabase()
        {
            var appConfig = Services.AppConfig.GetAppConfig.JsonFromDatabase();

            return Ok(appConfig);
        }

        [HttpGet("MergedJson")]

        public IActionResult MergedData()
        {
            var appConfig = Services.AppConfig.GetAppConfig.JsonMerger();

            return Ok(appConfig);
        }

        [HttpGet("CheckCustomConfig")]

        public IActionResult CustomConfig()
        {
            return Ok(_configuration["SecretKey"]);
        }
    }
}
