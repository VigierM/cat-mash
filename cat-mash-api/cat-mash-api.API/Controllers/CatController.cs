using cat_mash_api.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace cat_mash_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CatController : Controller
    {
        private readonly ILogger<CatController> _logger;
        private readonly ICatBusiness _catBusiness;

        public CatController(ILogger<CatController> logger, ICatBusiness catBusiness)
        {
            _logger = logger;
            _catBusiness = catBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> GetCatsAsync()
        {
            return Ok("Hello world");
        }
    }
}
