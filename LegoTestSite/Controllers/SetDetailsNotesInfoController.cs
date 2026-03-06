using Microsoft.AspNetCore.Mvc;

namespace LegoTestSite.Controllers
{
    [Route("SetDetails/[controller]")]
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
            return MySQLConnectionManager.GetSetDetailsNotesInfo(setID);
        }
    }
}
