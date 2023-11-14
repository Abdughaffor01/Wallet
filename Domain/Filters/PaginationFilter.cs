namespace Domain.Filters;
public class PaginationFilter
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public PaginationFilter(int pageNumber, int pageSize)
    {
        if (PageNumber <= 0) PageNumber = 1;
        else PageNumber = pageNumber;
        if (PageSize <= 0) PageSize = 10;
        else PageSize = pageSize;
    }
}
