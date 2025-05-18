using MediaInAction.VideoService.SeriesNs;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace MediaInAction.VideoService.EntityFrameworkCore;

[ConnectionStringName(VideoServiceDbProperties.ConnectionStringName)]
public interface IVideoServiceDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
    DbSet<Series> Seriess { get; }
}