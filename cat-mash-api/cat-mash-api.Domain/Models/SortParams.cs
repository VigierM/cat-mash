namespace cat_mash_api.Domain.Models
{
    public class SortParams
    {
        public const string ORDER_BY_ASC = "ASC";
        public const string ORDER_BY_DESC = "DESC";
        public static readonly List<string> AuthorizedOrder = new List<string>()
        {
            ORDER_BY_ASC,
            ORDER_BY_DESC
        };

        public string SortColumnName { get; set; } = "Id";

        public string SortOrder { get; set; } = ORDER_BY_ASC;

        public SortParams() { }

        public SortParams(string sortColumnName, string sortOrder)
        {
            SortColumnName = sortColumnName;
            SortOrder = sortOrder;
        }

        public string GetSortExpression(List<string> authorizedSortFields)
        {
            var localSortOrder = SortOrder;
            if (!AuthorizedOrder.Contains(localSortOrder.ToUpper()))
            {
                localSortOrder = "";
            }

            var localSortField = SortColumnName;
            var localAuthorizedFields = authorizedSortFields.Select(f => f.ToUpper()).ToList();
            if (!localAuthorizedFields.Contains(localSortField.ToUpper()))
            {
                localSortField = authorizedSortFields.First();
            }

            return $"{localSortField} {localSortOrder}";
        }
    }
}
