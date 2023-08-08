using cat_mash_api.Database.Shared.EntityModels;
using cat_mash_api.Domain.Models;
using cat_mash_api.Domain.Models.Filters;
using cat_mash_api.Domain.Models.Requests.Cats;

namespace cat_mash_api.Database.Shared.Interfaces
{
    public interface ICatManager
    {
        // Cats
        Task<PagedList<Cat>> GetCatsListAsync(CatFilters filters, PagingParams paging, SortParams sort);
        Task<Cat> GetCatByIdAsync(string id);
        Task<Cat> CreateCatAsync(CatPOST cat);
        Task<Cat> UpdateCatAsync(string catId, CatPUT cat);
        Task<bool> DeleteCatAsync(string id);

        // Cat votes
        Task<bool> AttachCatVoteAsync(string id);
    }
}
