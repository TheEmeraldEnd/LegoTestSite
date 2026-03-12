using LegoTestSite.DatabaseAccessors;
using Microsoft.AspNetCore.Mvc;

namespace LegoTestSite.Controllers
{
    [Route("")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<SetDetailsController> _logger;
        public ErrorController(ILogger<SetDetailsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetErrorInstruction")]
        public string GetErrorInstruction()
        {
            _logger.LogWarning("Attempt to access invalid portion of website");
            return "Error";
        }
    }
}
