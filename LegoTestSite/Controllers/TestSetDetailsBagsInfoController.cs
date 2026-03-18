using LegoTestSite.DatabaseAccessors;
using LegoTestSite.DatabaseAccessors.DatabaseConnectionManagers;
using Microsoft.AspNetCore.Mvc;

namespace LegoTestSite.Controllers
{
    [Route("Test/[controller]")]
    [ApiController]
    public class TestSetDetailsBagsInfoController : ControllerBase
    {
        private readonly ILogger<TestSetDetailsBagsInfoController> _logger;
        public TestSetDetailsBagsInfoController(ILogger<TestSetDetailsBagsInfoController> logger)
        {
            _logger = logger;

        }

        [HttpGet(Name = "TestGetSetBagDetailsInfo")]
        public string TestGetSetDetailsBagsInfo(string setID)
        {
            if (!DatabaseErrorStatic.IsMainConnectable(_logger))
                return DatabaseErrorStatic.ErrorMessage;

            string mainResult = DatabaseAccessorStatic.MainGetSetDetailsBagsInfo(setID);
            string testResult = DatabaseAccessorStatic.TestGetSetDetailsBagsInfo(setID);

            if (mainResult != testResult)
            {
                _logger.LogError($"{nameof(TestSetDetailsBagsInfoController)} Test set {setID} didn't match main");
                return DatabaseErrorStatic.ErrorMessage;
            }
            else
            {
                _logger.LogInformation($"Test from {nameof(TestSetDetailsBagsInfoController)} was successful");
                return DatabaseErrorStatic.SuccessMessage;
            }
        }
    }
}
