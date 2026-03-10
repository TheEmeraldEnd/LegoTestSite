using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LegoTestSite.Controllers
{
    //Not used for databases[Depriciated] 
    //Use 30707 as test
    [Route("Database/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly ILogger<DatabaseController> _logger;
        public DatabaseController(ILogger<DatabaseController> logger)
        {
            _logger = logger;
            
        }

        [HttpGet(Name = "GetTestValue")]
        public string Get()
        {
            LogLevel testLogLevel = LogLevel.Debug;
            EventId eventId = new EventId(37);
            string stateMessage = "Test";

            _logger.Log(testLogLevel, eventId, stateMessage);

            return "TestSuccessful";

            
        }
    }
}
