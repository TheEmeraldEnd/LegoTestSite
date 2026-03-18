using LegoTestSite.DatabaseAccessors;
using LegoTestSite.DatabaseAccessors.DatabaseConnectionManagers;
using Microsoft.AspNetCore.Mvc;

namespace LegoTestSite.Controllers
{
    [Route("Test/[controller]")]
    [ApiController]
    public class TestSetDetailsNotesInfoController : ControllerBase
    {
        private readonly ILogger<TestSetDetailsNotesInfoController> _logger;
        public TestSetDetailsNotesInfoController(ILogger<TestSetDetailsNotesInfoController> logger)
        {
            _logger = logger;

        }

        [HttpGet(Name = "TestGetSetDetailsNotesInfo")]
        public string TestGetSetDetailsNotesInfo(string setID)
        {
            if (!DatabaseErrorStatic.IsMainConnectable(_logger))
                return DatabaseErrorStatic.ErrorMessage;

            string mainResult = DatabaseAccessorStatic.MainGetSetDetailsNotesInfo(setID);
            string testResult = DatabaseAccessorStatic.TestGetSetDetailsNotesInfo(setID);

            if (mainResult != testResult)
            {
                _logger.LogError($"{nameof(TestSetDetailsNotesInfoController)} Test set {setID} didn't match main");
                return DatabaseErrorStatic.ErrorMessage;
            }
            else
            {
                _logger.LogInformation($"Test from {nameof(TestSetDetailsNotesInfoController)} was successful");
                return DatabaseErrorStatic.SuccessMessage;
            }
        }
    }
}
