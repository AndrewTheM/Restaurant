namespace eRestaurant.Helpers
{
    public interface IPaginationHelper
    {
        int CurrentPage { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int TotalPages { get; }
    }
}