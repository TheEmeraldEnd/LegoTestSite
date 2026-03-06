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
            return MySQLConnectionManager.GetSetGalleryDetails();
        }
    }
}
