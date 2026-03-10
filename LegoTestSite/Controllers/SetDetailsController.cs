using LegoTestSite.DatabaseAccessors;
using LegoTestSite.DatabaseAccessors.DatabaseConnectionManagers;
using Microsoft.AspNetCore.Mvc;

namespace LegoTestSite.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SetDetailsController : ControllerBase
    {
        private readonly ILogger<SetDetailsController> _logger;
        public SetDetailsController(ILogger<SetDetailsController> logger)
        {
            _logger = logger;

        }

        [HttpGet(Name = "GetSetDetails")]
        public string GetSetDetails(string setID)
        {
            return DatabaseAccessorStatic.GetSetDetails(setID);
        }
    }
}
