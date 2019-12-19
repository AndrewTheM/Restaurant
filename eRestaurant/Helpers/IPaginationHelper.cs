namespace eRestaurant.Helpers
{
    public interface IPaginationHelper
    {
        public int CurrentPage { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages { get; }
        public bool HasPrevious { get; }
        public bool HasNext { get; }
    }
}