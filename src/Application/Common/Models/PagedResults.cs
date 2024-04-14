using Microsoft.EntityFrameworkCore;

namespace SampleApp.Application.Common.Models;

public class PagedResults<T>
{
    public IReadOnlyCollection<T> Items { get; }
    public int TotalCount { get; }
    public int TotalPages { get; }
    public int PageNumber { get; }

    public PagedResults(IReadOnlyCollection<T> items, int count, int pageNumber, int pageSize)
    {
        Items = items;

        TotalCount = count;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        PageNumber = Math.Min(1, pageNumber);
    }

    public static async Task<PagedResults<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();

        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResults<T>(items, count, pageNumber, pageSize);
    }
}
