namespace Core.Models;

public class ApiResponse<T>
{
    public required ApiStatus Status {get; set;}
    public T? Data { get; set; }
}

public class ApiResponseWithPaging<T>
{
    public List<T>? DataSource { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
}

public class ApiStatus
{
    public int Code {get;set;} = 2000;
    public String Description { get; set; } = "";
}