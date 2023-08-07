namespace cat_mash_api.Domain.Models
{
    public class PagingParams
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }
}
