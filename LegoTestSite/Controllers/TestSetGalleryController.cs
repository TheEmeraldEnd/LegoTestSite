using LegoTestSite.DatabaseAccessors;
using LegoTestSite.DatabaseAccessors.DatabaseConnectionManagers;
using Microsoft.AspNetCore.Mvc;

namespace LegoTestSite.Controllers
{
    [Route("Test/[controller]")]
    [ApiController]
    public class TestSetGalleryController : ControllerBase
    {
        private readonly ILogger<TestSetGalleryController> _logger;
        public TestSetGalleryController(ILogger<TestSetGalleryController> logger)
        {
            _logger = logger;

        }

        [HttpGet(Name = "TestGetSetGallery")]
        public string TestGetSetGallery()
        {
            if (!DatabaseErrorStatic.IsMainConnectable(_logger))
                return DatabaseErrorStatic.ErrorMessage;

            string mainResult = DatabaseAccessorStatic.MainGetSetGallery();
            string testResult = DatabaseAccessorStatic.TestGetSetGallery();

            if (!mainResult.Contains(testResult))
            {
                _logger.LogError($"{nameof(TestSetGalleryController)} main set gallery didn't contain test gallery");
                return DatabaseErrorStatic.ErrorMessage;
            }
            else
            {
                _logger.LogInformation($"Test from {nameof(TestSetGalleryController)} was successful");
                return DatabaseErrorStatic.SuccessMessage;
            }
        }
    }
}
