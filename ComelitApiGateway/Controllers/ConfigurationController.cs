using ComelitApiGateway.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComelitApiGateway.Controllers
{
    [ApiController]
    [Route("Configurations")]
    public class ConfigurationController(IConfiguration config) : BaseController(config)
    {
        /// <summary>
        /// Get the Vedo Key, passed as environment variable
        /// </summary>
        /// <returns></returns>
        [HttpGet("vedo-key")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult GetVedoKey()
        {
            Console.WriteLine($"Requested VEDO_KEY: {config["VEDO_KEY"]}");
            return Ok(config["VEDO_KEY"]);
        }
    }
}
