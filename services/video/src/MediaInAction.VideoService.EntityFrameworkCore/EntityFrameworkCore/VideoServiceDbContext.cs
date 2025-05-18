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
            b.HasIndex(q => new { q.Name, q.FirstAiredYear });
        });
        
        modelBuilder.Entity<Series>()
            .HasIndex(b => new {b.Name, b.FirstAiredYear})
            .IsUnique();
        
        modelBuilder.Entity<SeriesAlias>(b =>
        {
            b.ToTable(VideoServiceDbProperties.DbTablePrefix + "SeriesAliases",
                VideoServiceDbProperties.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props

            b.Property<Guid>("SeriesId").IsRequired();
            b.Property(q => q.IdValue).IsRequired();
            b.Property(q => q.IdType).IsRequired();
        });
        
        modelBuilder.Entity<SeriesAlias>()
            .HasIndex(b => new {b.SeriesId, b.IdType, b.IdValue})
            .IsUnique();
        
        modelBuilder.Entity<Movie>(b =>
        {
            b.ToTable(VideoServiceDbProperties.DbTablePrefix + "Movies", VideoServiceDbProperties.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(q => q.Name).HasMaxLength(SeriesConstants.PaymentStatusMaxLength);
            b.Navigation(q => q.MovieAliases).UsePropertyAccessMode(PropertyAccessMode.Property);
            b.HasIndex(q => new { q.Name, q.FirstAiredYear });
        });
        
        modelBuilder.Entity<Movie>()
            .HasIndex(b => new {b.Name, b.FirstAiredYear})
            .IsUnique();
        
        modelBuilder.Entity<MovieAlias>(b =>
        {
            b.ToTable(VideoServiceDbProperties.DbTablePrefix + "MovieAliases",
                VideoServiceDbProperties.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props

            b.Property<Guid>("MovieId").IsRequired();
            b.Property(q => q.IdValue).IsRequired();
            b.Property(q => q.IdType).IsRequired();
        });

        modelBuilder.Entity<MovieAlias>()
            .HasIndex(b => new {b.MovieId, b.IdType, b.IdValue})
            .IsUnique();
        
        // episode
        modelBuilder.Entity<Episode>(b =>
        {
            b.ToTable(VideoServiceDbProperties.DbTablePrefix + "Episodes", VideoServiceDbProperties.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Navigation(q => q.EpisodeAliases).UsePropertyAccessMode(PropertyAccessMode.Property);
            b.HasIndex(q => new { q.SeriesId, q.SeasonNum, q.EpisodeNum });
        });
        
        modelBuilder.Entity<Episode>()
            .HasIndex(b => new {b.SeriesId, b.SeasonNum, b.EpisodeNum})
            .IsUnique();
        
        modelBuilder.Entity<EpisodeAlias>(b =>
        {
            b.ToTable(VideoServiceDbProperties.DbTablePrefix + "EpisodeAliases",
                VideoServiceDbProperties.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property<Guid>("EpisodeId").IsRequired();
            b.Property(q => q.IdValue).IsRequired();
            b.Property(q => q.IdType).IsRequired();
        });

        modelBuilder.Entity<EpisodeAlias>()
            .HasIndex(b => new {b.EpisodeId, b.IdType, b.IdValue})
            .IsUnique();
        
        // mapper
        
        modelBuilder.Entity<ToBeMapped>(b =>
        {
            b.ToTable(VideoServiceDbProperties.DbTablePrefix + "ToBeMappeds",
                VideoServiceDbProperties.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
        });

        modelBuilder.Entity<ToBeMapped>()
            .HasIndex(b => new {b.Alias})
            .IsUnique();
    }
}
