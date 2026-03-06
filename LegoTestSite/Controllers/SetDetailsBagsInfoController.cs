using Microsoft.AspNetCore.Mvc;

namespace LegoTestSite.Controllers
{
    [Route("SetDetails/[controller]")]
    [ApiController]
    public class SetDetailsBagsInfoController : ControllerBase
    {
        private readonly ILogger<SetDetailsBagsInfoController> _logger;
        public SetDetailsBagsInfoController(ILogger<SetDetailsBagsInfoController> logger)
        {
            _logger = logger;

        }

        [HttpGet(Name = "GetSetBagDetailsInfo")]
        public string GetSetDetailsBagsInfo(string setID)
        {
            return MySQLConnectionManager.GetSetDetailsBagsInfo(setID);
        }
    }
}
