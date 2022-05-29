namespace Fronius.Models {
    public class Paged<T> {
        public Paged(IReadOnlyCollection<T> items, int totalCount) {
            TotalCount = totalCount;
            Items = items;
        }

        public int TotalCount { get; }
        public IReadOnlyCollection<T> Items { get; }
    }
}
