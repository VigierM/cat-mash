using cat_mash_api.Database.Shared.EntityModels;
using cat_mash_api.Domain.Models;
using CatsFilters = cat_mash_api.Domain.Models.Filters.CatFilters;
using System.Linq.Dynamic.Core;

namespace cat_mash_api.Database.Helpers.Filters
{
    public static class CatFilters
    {
        private static List<string> AuthorizedFields = new List<string> { "Id", "Votes"};
        public static IQueryable<Cat> FilterCats(this IQueryable<Cat> source,
            CatsFilters filters)
        {
            return source;
        }

        public static IQueryable<Cat> SortCats(this IQueryable<Cat> source,
            SortParams sort)
        {
            switch (sort.SortColumnName)
            {
                case "Id":
                    return source.OrderBy(sort.GetSortExpression(AuthorizedFields));
                case "Votes":
                    return sort.SortOrder == "ASC" ? source.OrderBy(c => c.Votes.Count()) 
                                                    : source.OrderByDescending(c => c.Votes.Count());
                default:
                    return source;
            }
        }
    }
}
