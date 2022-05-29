namespace Fronius.Models {
    public class Pager {
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 5;
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }

        public int Skip => PageNumber * PageSize;
        public string SortCriteria {
            get {
                return $"{SortBy} {SortOrder ?? "asc"}";
            }
        }

        public override string ToString() {
            return $"page {PageNumber}, limit: {PageSize}, sort by: {SortBy}, sort order: {SortOrder}";
        }
    }
}
