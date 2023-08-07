using cat_mash_api.Domain.Models;
using cat_mash_api.Domain.Models.DTOs;
using cat_mash_api.Domain.Models.Filters;
using cat_mash_api.Domain.Models.Requests.Cats;

namespace cat_mash_api.Business.Interfaces
{
    public interface ICatBusiness
    {
        Task<T> GetCatByIdAsync<T>(string id) where T : CatDTO;

        Task<PagedList<T>> GetCatsListAsync<T>(PagingParams pagingParams,
            SortParams sortParams, CatFilters filters) where T : CatDTO;

        Task<T> PostCatAsync<T>(CatPOST cat) where T : CatDTO;

        Task<T> UpdateCatAsync<T>(string id, CatPUT cat) where T : CatDTO;

        Task<bool> DeleteCatAsync(string id);
    }
}
