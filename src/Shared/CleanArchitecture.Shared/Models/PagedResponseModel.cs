using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Shared.Models;

public class PagedResponseModel<T> : ResponseModel<T>
{
    public int PageSize { get; private set; }
    public int PageIndex { get; private set; }
    public int TotalPages { get; private set; }

    public PagedResponseModel(T data, int totalPages, int pageIndex, int pageSize)
    {
        Data = data;
        StatusCode = 200;
        PageSize = pageSize;
        IsSuccessful = true;
        PageIndex = pageIndex;
        TotalPages = totalPages;
    }

    public static PagedResponseModel<T> CreateAsync(T data, int totalPages, int pageIndex, int pageSize)
        => new(data, totalPages, pageIndex, pageSize);

    public static async Task<PagedResponseModel<List<T>>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
    {
        var totalPages = source.Count();
        var data = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PagedResponseModel<List<T>>(data, totalPages, pageIndex, pageSize);
    }
}
