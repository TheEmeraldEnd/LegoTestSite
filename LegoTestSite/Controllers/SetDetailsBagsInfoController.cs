using LegoTestSite.DatabaseAccessors;
using LegoTestSite.DatabaseAccessors.DatabaseConnectionManagers;
using Microsoft.AspNetCore.Mvc;

namespace LegoTestSite.Controllers
{
    [Route("[controller]")]
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
            return DatabaseAccessorStatic.GetSetDetailsBagsInfo(setID);
        }
    }
}
