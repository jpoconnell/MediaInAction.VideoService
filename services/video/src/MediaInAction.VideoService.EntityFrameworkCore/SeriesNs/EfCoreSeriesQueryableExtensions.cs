using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MediaInAction.VideoService.SeriesNs;

public static class EfCoreSeriesQueryableExtensions
{
    public static IQueryable<Series> IncludeDetails(this IQueryable<Series> queryable, bool include = true)
    {
        return !include
            ? queryable
            : queryable
                .Include(q => q.SeriesAliases);
    }
}