namespace CleanArchitecture.Application.Common.Pagination;

public abstract class PaginationFilter
{
    private int _pageSize = 50;
    private const int maxPageSize = 50;

    public string Search { get; set; }
    public int PageIndex { get; set; } = 1;

    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }
}
