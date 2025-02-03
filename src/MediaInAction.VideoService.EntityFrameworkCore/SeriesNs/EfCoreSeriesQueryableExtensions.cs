using System.Linq;
using MediaInAction.VideoService.SeriesNs;
using Microsoft.EntityFrameworkCore;

public static class EfCoreSeriesQueryableExtensions
{
    public static IQueryable<Series> IncludeDetails(this IQueryable<Series> queryable, bool include = true)
    {
        return !include
            ? queryable
            : queryable
                .Include(q => q.SeriesStatus);
    }
}