namespace Domain.Wrappers;
public class PaginationResonse<T>:Response<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages { get; set; }
    public PaginationResonse(T data,int pageNumber,int pageSize,int totalRecords):base(data)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalRecords = totalRecords;
        TotalPages=(int)Math.Ceiling((double)TotalRecords/pageSize);
    }
}
