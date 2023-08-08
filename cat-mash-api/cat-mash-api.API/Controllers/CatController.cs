using cat_mash_api.Business.Interfaces;
using cat_mash_api.Domain.Models;
using cat_mash_api.Domain.Models.DTOs;
using cat_mash_api.Domain.Models.Filters;
using cat_mash_api.Domain.Models.Requests.Cats;
using Microsoft.AspNetCore.Mvc;

namespace cat_mash_api.Controllers
{
    [ApiController]
    [Route("api/cats")]
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
        public async Task<IActionResult> GetCatsAsync([FromQuery] PagingParams paging,[FromQuery] SortParams sort,
            [FromQuery] CatFilters filters)
        {
            return Ok(await _catBusiness.GetCatsListAsync<CatDTO>(paging, sort, filters));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCatByIdAsync([FromRoute] string id)
        {
            return Ok(await _catBusiness.GetCatByIdAsync<CatDTO>(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostCategoryAsync([FromBody] CatPOST cat)
        {
            return Ok(await _catBusiness.PostCatAsync<CatDTO>(cat));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryAsync([FromRoute] string id, [FromBody] CatPUT cat)
        {
            return Ok(await _catBusiness.UpdateCatAsync<CatDTO>(id, cat));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] string id)
        {
            return Ok(await _catBusiness.DeleteCatAsync(id));
        }

        [HttpGet("mashup")]
        public async Task<IActionResult> GetCatsMashupAsync()
        {
            return Ok(await _catBusiness.GetCatMashupAsync<CatDTO>());
        }

        [HttpPost("{id}/votes")]
        public async Task<IActionResult> PostCatVoteAsync([FromRoute] string id)
        {
            return Ok(await _catBusiness.PostCatVoteAsync(id));
        }
    }
}
