namespace Application.Dto;

public class PaginationDefault<T> 
{
    public int Page;
    public int PageSize;
    public int TotalCount;
    public int TotalPages;
    public List<T> Data;
};