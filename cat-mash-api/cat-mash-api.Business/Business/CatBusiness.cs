using AutoMapper;
using cat_mash_api.Business.Interfaces;
using cat_mash_api.Database.Shared.Interfaces;
using cat_mash_api.Domain.Models;
using cat_mash_api.Domain.Models.DTOs;
using cat_mash_api.Domain.Models.Filters;
using cat_mash_api.Domain.Models.Requests.Cats;

namespace cat_mash_api.Business.Business
{
    public class CatBusiness : ICatBusiness
    {
        public ICatManager _catManager;
        private readonly IMapper _mapper;

        public CatBusiness(ICatManager catManager, IMapper mapper)
        {
            _catManager = catManager;
            _mapper = mapper;
        }

        /*
         * Cats
        */

        public async Task<T> GetCatByIdAsync<T>(string id) where T : CatDTO
        {
            var cat = await _catManager.GetCatByIdAsync(id);

            return _mapper.Map<T>(cat);
        }

        public async Task<PagedList<T>> GetCatsListAsync<T>(PagingParams pagingParams, SortParams sortParams, CatFilters filters) where T : CatDTO
        {
            var cats = await _catManager.GetCatsListAsync(filters, pagingParams, sortParams);

            return _mapper.Map<PagedList<T>>(cats);
        }

        public async Task<T> PostCatAsync<T>(CatPOST cat) where T : CatDTO
        {
            var newCat = await _catManager.CreateCatAsync(cat);

            return _mapper.Map<T>(newCat);
        }

        public async Task<T> UpdateCatAsync<T>(string id, CatPUT cat) where T : CatDTO
        {
            var updatedCat = await _catManager.UpdateCatAsync(id, cat);

            return _mapper.Map<T>(updatedCat);
        }

        public async Task<bool> DeleteCatAsync(string id)
        {
            return await _catManager.DeleteCatAsync(id);
        }

        /*
         * Cat votes
        */
        public async Task<bool> PostCatVoteAsync(string id)
        {
            return await _catManager.AttachCatVoteAsync(id);
        }
    }
}
