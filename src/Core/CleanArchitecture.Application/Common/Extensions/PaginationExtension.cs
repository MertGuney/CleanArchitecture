namespace CleanArchitecture.Application.Common.Extensions;

public static class PaginationExtension
{
    public static IQueryable<T> Page<T>(this IQueryable<T> data, int pageIndex, int pageSize)
        => data.Skip((pageIndex - 1) * pageSize).Take(pageSize);

    public static int CountOfPages<T>(this IQueryable<T> data, int pageSize)
        => (data.Count() / pageSize) + ((data.Count() % pageSize) > 0 ? 1 : 0);
}
