namespace cat_mash_api.Domain.Models
{
    public class PagedList<T>
    {
        public int CurrentPage { get; set; }

        public int TotalCount { get; set; }

        public List<T> Data { get; set; } = new List<T>();

        public PagedList() { }

        public PagedList(PagingParams paging) => CurrentPage = paging != null ? paging.PageNumber : 1;
    }
}
