namespace Fronius.Models {
    public class Pager {
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 5;
        public string? Filter { get; set; }
        public string? SortOrder { get; set; }

        public int Skip => PageNumber * PageSize;
    }
}
