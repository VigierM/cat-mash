using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using cat_mash_api.Database.Helpers.Filters;
using cat_mash_api.Database.Shared.EntityModels;
using cat_mash_api.Database.Shared.Interfaces;
using cat_mash_api.Domain.Models;
using Filters = cat_mash_api.Domain.Models.Filters.CatFilters;
using cat_mash_api.Domain.Models.Requests.Cats;

namespace cat_mash_api.Database.Manager
{
    public class CatManager : ICatManager
    {
        protected readonly CatMashDbContext _dbContext;
        protected readonly ILogger _logger;

        public CatManager(CatMashDbContext dbContext, ILogger<CatManager> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /*
         * Cats
        */
        public async Task<Cat> GetCatByIdAsync(string id)
        {
            var cat = _dbContext.Cats.Include(c => c.Votes)
                                        .FirstOrDefault(r => r.Id == id);

            return cat;
        }

        public async Task<PagedList<Cat>> GetCatsListAsync(Filters filters, PagingParams paging, SortParams sort)
        {
            PagedList<Cat> cats = new PagedList<Cat>();

            var query = _dbContext.Cats.Include(c => c.Votes)
                                        .FilterCats(filters)
                                        .SortCats(sort);


            cats.TotalCount = query.Count();
            cats.Data = query.Skip((paging.PageNumber - 1) * paging.PageSize)
                                .Take(paging.PageSize).ToList();

            return cats;
        }

        public async Task<Cat> CreateCatAsync(CatPOST cat)
        {
            var newCat = new Cat(cat);
            _logger.LogDebug(string.Format("Begin create entity: '{0}'.", typeof(Cat)));
            await _dbContext.AddAsync(newCat);

            try
            {
                int num = await _dbContext.SaveChangesAsync();
                _logger.LogDebug(string.Format("{0} row(s) created.", num));
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(string.Format("Failed to create entity: '{0}'.", typeof(Cat)), ex);
            }
            return newCat;
        }

        public async Task<Cat> UpdateCatAsync(string catId, CatPUT cat)
        {
            _logger.LogDebug(string.Format("Begin update entity: '{0}'.", typeof(Cat)));
            var update = await _dbContext.Cats.FirstOrDefaultAsync(c => c.Id == catId);

            if (!String.IsNullOrEmpty(catId) && null == update)
            {
                _logger.LogDebug(string.Format("Not found entity to update: '{0}'.", typeof(Cat)));
                return null;
            }

            try
            {
                update.Update(cat);

                int num = await _dbContext.SaveChangesAsync();
                _logger.LogDebug(string.Format("{0} row(s) updated.", num));
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(string.Format("Failed to update entity: '{0}'.", typeof(Cat)), ex);
            }
            return update;
        }

        public async Task<bool> DeleteCatAsync(string id)
        {
            _logger.LogDebug(string.Format("Begin delete entity: '{0}'.", typeof(Cat)));
            var cat = await _dbContext.Cats.FirstOrDefaultAsync(r => r.Id == id);

            if (cat == null)
            {
                _logger.LogDebug(string.Format("Entity: '{0}' to delete not found.", typeof(Cat)));
                return false;
            }
            try
            {
                _dbContext.Cats.Remove(cat);
                int num = await _dbContext.SaveChangesAsync();
                _logger.LogDebug(string.Format("{0} row(s) deleted.", num));
                return num > 0;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(string.Format("Failed to delete entity: '{0}'.", typeof(Cat)), ex);
            }
        }

        /*
         * Cat masup
        */
        public async Task<PagedList<Cat>> GetCatMashupAsync()
        {
            PagedList<Cat> cats = new PagedList<Cat>();

            var query = _dbContext.Cats.OrderBy(c => EF.Functions.Random())
                                        .Take(2);


            cats.TotalCount = query.Count();
            cats.Data = query.ToList();

            return cats;
        }

        /*
         * Cat votes
        */
        public async Task<bool> AttachCatVoteAsync(string id)
        {
            var catToVote = await _dbContext.Cats.FirstOrDefaultAsync(c => c.Id == id);
            if (null == catToVote)
            {
                _logger.LogDebug(string.Format("No entity found to vote to for: '{0}'.", typeof(Cat)));
                return false;
            }

            _logger.LogDebug(string.Format("Begin create entity: '{0}'.", typeof(Vote)));
            await _dbContext.AddAsync(new Vote { CatId = catToVote.Id });

            try
            {
                int num = await _dbContext.SaveChangesAsync();
                _logger.LogDebug(string.Format("{0} row(s) created.", num));
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(string.Format("Failed to create entity: '{0}'.", typeof(Cat)), ex);
            }
            return true;
        }
    }
}
