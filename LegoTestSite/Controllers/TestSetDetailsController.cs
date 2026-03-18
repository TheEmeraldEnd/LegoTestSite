using LegoTestSite.DatabaseAccessors;
using LegoTestSite.DatabaseAccessors.DatabaseConnectionManagers;
using Microsoft.AspNetCore.Mvc;

namespace LegoTestSite.Controllers
{
    [Route("Test/[controller]")]
    [ApiController]
    public class TestSetDetailsController : ControllerBase
    {
        private readonly ILogger<TestSetDetailsController> _logger;
        public TestSetDetailsController(ILogger<TestSetDetailsController> logger)
        {
            _logger = logger;

        }

        [HttpGet(Name = "TestGetSetDetails")]
        public string TestGetSetDetails(string setID)
        {
            if (!DatabaseErrorStatic.IsMainConnectable(_logger))
                return DatabaseErrorStatic.ErrorMessage;

            string mainResult = DatabaseAccessorStatic.MainGetSetDetails(setID);
            string testResult = DatabaseAccessorStatic.TestGetSetDetails(setID);

            if (mainResult != testResult)
            {
                _logger.LogError($"{nameof(TestSetDetailsController)} Test set {setID} didn't match main");
                return DatabaseErrorStatic.ErrorMessage;
            }
            else
            {
                _logger.LogInformation($"Test from {nameof(TestSetDetailsController)} was successful");
                return DatabaseErrorStatic.SuccessMessage;
            }
        }
    }
}
