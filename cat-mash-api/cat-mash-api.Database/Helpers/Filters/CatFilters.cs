using cat_mash_api.Database.Shared.EntityModels;
using cat_mash_api.Domain.Models;
using CatsFilters = cat_mash_api.Domain.Models.Filters.CatFilters;

namespace cat_mash_api.Database.Helpers.Filters
{
    public static class CatFilters
    {
        public static IQueryable<Cat> FilterCats(this IQueryable<Cat> source,
            CatsFilters filters)
        {
            return source;
        }

        public static IQueryable<Cat> SortCats(this IQueryable<Cat> source,
            SortParams sort)
        {
            return source;
        }
    }
}
