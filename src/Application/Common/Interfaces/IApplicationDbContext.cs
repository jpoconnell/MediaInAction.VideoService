using VideoService2.Domain.Entities;

namespace VideoService2.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Series> SeriesList { get; }

    DbSet<SeriesAlias> SeriesAliases { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
