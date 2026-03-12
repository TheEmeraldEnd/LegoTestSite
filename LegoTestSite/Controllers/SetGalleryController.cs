using LegoTestSite.DatabaseAccessors;
using LegoTestSite.DatabaseAccessors.DatabaseConnectionManagers;
using Microsoft.AspNetCore.Mvc;

namespace LegoTestSite.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SetGalleryController : ControllerBase
    {
        private readonly ILogger<SetGalleryController> _logger;
        public SetGalleryController(ILogger<SetGalleryController> logger)
        {
            _logger = logger;

        }

        [HttpGet(Name = "GetSetGallery")]
        public string GetSetGallery()
        {
            string result = "";
            try
            {
                result =DatabaseAccessorStatic.GetSetGallery();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Source}: {ex.Message}");
            }
            return result;
        }
    }
}
