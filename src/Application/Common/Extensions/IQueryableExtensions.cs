using SampleApp.Application.Common.Models;

namespace SampleApp.Application.Common.Extensions;

public static class IQueryableExtensions
{
    public static async Task<PagedResults<TResult>> AsPagedResultsAsync<TResult>(this IQueryable<TResult> source, int pageNumber, int pageSize) =>
        await PagedResults<TResult>.CreateAsync(source, pageNumber, pageSize);
}
