using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using MediaInAction.VideoService.EpisodeNs;
using MediaInAction.VideoService.MovieNs;
using MediaInAction.VideoService.SeriesNs;
using MediaInAction.VideoService.ToBeMappedNs;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace MediaInAction.VideoService.EntityFrameworkCore;

[ConnectionStringName(VideoServiceDbProperties.ConnectionStringName)]
public class VideoServiceDbContext : AbpDbContext<VideoServiceDbContext>, IVideoServiceDbContext
{
    public virtual DbSet<Series> Seriess { get; set; }
    public virtual DbSet<Episode> Episodes { get; set; }
    public virtual DbSet<Movie> Movies { get; set; }
    public virtual DbSet<ToBeMapped> ToBeMappeds { get; set; }
    public VideoServiceDbContext(DbContextOptions<VideoServiceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        /* Include modules to your migration db context */

        modelBuilder.ConfigureVideoService();
        /* Configure your own tables/entities inside here */


        modelBuilder.Entity<Series>(b =>
        {
            b.ToTable(VideoServiceDbProperties.DbTablePrefix + "Seriess", VideoServiceDbProperties.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(q => q.Name).HasMaxLength(SeriesConstants.PaymentStatusMaxLength);
            b.Navigation(q => q.SeriesAliases).UsePropertyAccessMode(PropertyAccessMode.Property);
            b.HasIndex(q => q.Name);
        });

        modelBuilder.Entity<SeriesAlias>(b =>
        {
            b.ToTable(VideoServiceDbProperties.DbTablePrefix + "SeriesAliases",
                VideoServiceDbProperties.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props

            b.Property<Guid>("SeriesId").IsRequired();
            b.Property(q => q.IdValue).IsRequired();
            b.Property(q => q.IdType).IsRequired();
        });
    }
}
