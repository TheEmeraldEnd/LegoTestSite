using LegoTestSite.DatabaseAccessors;
using LegoTestSite.DatabaseAccessors.DatabaseConnectionManagers;
using Microsoft.AspNetCore.Mvc;

namespace LegoTestSite.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SetDetailsNotesInfoController : ControllerBase
    {
        private readonly ILogger<SetDetailsNotesInfoController> _logger;
        public SetDetailsNotesInfoController(ILogger<SetDetailsNotesInfoController> logger)
        {
            _logger = logger;

        }

        [HttpGet(Name = "GetSetDetailsNotesInfo")]
        public string GetSetDetailsNotesInfo(string setID)
        {
            return DatabaseAccessorStatic.GetSetDetailsNotesInfo(setID);
        }
    }
}
