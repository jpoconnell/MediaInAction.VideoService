﻿// <auto-generated />
using System;
using MediaInAction.VideoService.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Volo.Abp.EntityFrameworkCore;

#nullable disable

namespace MediaInAction.VideoService.Migrations
{
    [DbContext(typeof(VideoServiceDbContext))]
    [Migration("20240108173415_init1")]
    partial class init1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("_Abp_DatabaseProvider", EfCoreDatabaseProvider.PostgreSql)
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MediaInAction.VideoService.EpisodeAliasNs.EpisodeAlias", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EpisodeId")
                        .HasColumnType("uuid");

                    b.Property<string>("IdType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("IdValue")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EpisodeId");

                    b.ToTable("EpisodeAliases");
                });

            modelBuilder.Entity("MediaInAction.VideoService.EpisodeNs.Episode", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("AiredDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("AltEpisodeId")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("ConcurrencyStamp");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("CreationTime");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("uuid")
                        .HasColumnName("CreatorId");

                    b.Property<string>("EpisodeName")
                        .HasColumnType("text");

                    b.Property<int>("EpisodeNum")
                        .HasColumnType("integer");

                    b.Property<int>("EventStatus")
                        .HasColumnType("integer");

                    b.Property<string>("ExtraProperties")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ExtraProperties");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("LastModificationTime");

                    b.Property<Guid?>("LastModifierId")
                        .HasColumnType("uuid")
                        .HasColumnName("LastModifierId");

                    b.Property<int>("MediaStatus")
                        .HasColumnType("integer");

                    b.Property<string>("SeasonEpisode")
                        .HasColumnType("text");

                    b.Property<int>("SeasonNum")
                        .HasColumnType("integer");

                    b.Property<Guid>("SeriesId")
                        .HasColumnType("uuid");

                    b.Property<string>("Source")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Episodes");
                });

            modelBuilder.Entity("MediaInAction.VideoService.FileEntryNs.FileEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("CleanFileName")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("ConcurrencyStamp");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("CreationTime");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("uuid")
                        .HasColumnName("CreatorId");

                    b.Property<string>("Directory")
                        .HasColumnType("text");

                    b.Property<Guid>("EpisodeLink")
                        .HasColumnType("uuid");

                    b.Property<string>("ExternalId")
                        .HasColumnType("text");

                    b.Property<string>("Extn")
                        .HasColumnType("text");

                    b.Property<string>("ExtraProperties")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ExtraProperties");

                    b.Property<string>("FileName")
                        .HasColumnType("text");

                    b.Property<int>("FileStatus")
                        .HasColumnType("integer");

                    b.Property<bool>("IsMapped")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("LastModificationTime");

                    b.Property<Guid?>("LastModifierId")
                        .HasColumnType("uuid")
                        .HasColumnName("LastModifierId");

                    b.Property<int>("ListName")
                        .HasColumnType("integer");

                    b.Property<int>("MediaType")
                        .HasColumnType("integer");

                    b.Property<int>("Sequence")
                        .HasColumnType("integer");

                    b.Property<Guid>("SeriesLink")
                        .HasColumnType("uuid");

                    b.Property<string>("Server")
                        .HasColumnType("text");

                    b.Property<long>("Size")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("FileEntries");
                });

            modelBuilder.Entity("MediaInAction.VideoService.MovieAliasNs.MovieAlias", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("IdType")
                        .HasColumnType("text");

                    b.Property<string>("IdValue")
                        .HasColumnType("text");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.ToTable("MovieAliases");
                });

            modelBuilder.Entity("MediaInAction.VideoService.MovieNs.Movie", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("ConcurrencyStamp");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("CreationTime");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("uuid")
                        .HasColumnName("CreatorId");

                    b.Property<int>("EventStatus")
                        .HasColumnType("integer");

                    b.Property<string>("ExtraProperties")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ExtraProperties");

                    b.Property<int>("FirstAiredYear")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("LastModificationTime");

                    b.Property<Guid?>("LastModifierId")
                        .HasColumnType("uuid")
                        .HasColumnName("LastModifierId");

                    b.Property<int>("MediaStatus")
                        .HasColumnType("integer");

                    b.Property<int>("MovieStatus")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MediaInAction.VideoService.SeriesAliasNs.SeriesAlias", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("IdType")
                        .HasColumnType("text");

                    b.Property<string>("IdValue")
                        .HasColumnType("text");

                    b.Property<Guid>("SeriesId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SeriesId");

                    b.ToTable("SeriesAliases");
                });

            modelBuilder.Entity("MediaInAction.VideoService.SeriesNs.Series", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("ConcurrencyStamp");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("CreationTime");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("uuid")
                        .HasColumnName("CreatorId");

                    b.Property<int>("EventStatus")
                        .HasColumnType("integer");

                    b.Property<string>("ExtraProperties")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ExtraProperties");

                    b.Property<int>("FirstAiredYear")
                        .HasColumnType("integer");

                    b.Property<string>("ImageName")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("LastModificationTime");

                    b.Property<Guid?>("LastModifierId")
                        .HasColumnType("uuid")
                        .HasColumnName("LastModifierId");

                    b.Property<int>("MediaStatus")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Name", "FirstAiredYear")
                        .IsUnique();

                    b.ToTable("SeriesList", (string)null);
                });

            modelBuilder.Entity("MediaInAction.VideoService.ToBeMappedNs.ToBeMapped", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Alias")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("ConcurrencyStamp");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("CreationTime");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("uuid")
                        .HasColumnName("CreatorId");

                    b.Property<string>("ExtraProperties")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ExtraProperties");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("LastModificationTime");

                    b.Property<Guid?>("LastModifierId")
                        .HasColumnType("uuid")
                        .HasColumnName("LastModifierId");

                    b.Property<bool>("Processed")
                        .HasColumnType("boolean");

                    b.Property<int>("Tries")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Alias")
                        .IsUnique();

                    b.ToTable("ToBeMappeds", (string)null);
                });

            modelBuilder.Entity("MediaInAction.VideoService.TorrentsNs.Torrent", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<long>("Added")
                        .HasColumnType("bigint");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<double>("CompleteTime")
                        .HasColumnType("double precision");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("ConcurrencyStamp");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("CreationTime");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("uuid")
                        .HasColumnName("CreatorId");

                    b.Property<string>("DownloadLocation")
                        .HasColumnType("text");

                    b.Property<Guid>("EpisodeLink")
                        .HasColumnType("uuid");

                    b.Property<string>("ExtraProperties")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ExtraProperties");

                    b.Property<string>("Hash")
                        .HasColumnType("text");

                    b.Property<bool>("IsMapped")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSeed")
                        .HasColumnType("boolean");

                    b.Property<string>("Label")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("LastModificationTime");

                    b.Property<Guid?>("LastModifierId")
                        .HasColumnType("uuid")
                        .HasColumnName("LastModifierId");

                    b.Property<Guid>("MediaLink")
                        .HasColumnType("uuid");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<bool>("Paused")
                        .HasColumnType("boolean");

                    b.Property<double>("Ratio")
                        .HasColumnType("double precision");

                    b.Property<int>("TorrentStatus")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Torrents");
                });

            modelBuilder.Entity("MediaInAction.VideoService.TraktRequestNs.TraktRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Command")
                        .HasColumnType("text");

                    b.Property<DateTime>("CompleteTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("ConcurrencyStamp");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("CreationTime");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("uuid")
                        .HasColumnName("CreatorId");

                    b.Property<string>("ExtraProperties")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ExtraProperties");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("LastModificationTime");

                    b.Property<Guid?>("LastModifierId")
                        .HasColumnType("uuid")
                        .HasColumnName("LastModifierId");

                    b.HasKey("Id");

                    b.ToTable("TraktRequests");
                });

            modelBuilder.Entity("MediaInAction.VideoService.TraktRequestNs.TraktRequestItem", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<int>("Season")
                        .HasColumnType("integer");

                    b.Property<string>("Slug")
                        .HasColumnType("text");

                    b.Property<Guid?>("TraktRequestId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TraktRequestId");

                    b.ToTable("TraktRequestItem");
                });

            modelBuilder.Entity("MediaInAction.VideoService.EpisodeAliasNs.EpisodeAlias", b =>
                {
                    b.HasOne("MediaInAction.VideoService.EpisodeNs.Episode", null)
                        .WithMany("EpisodeAliases")
                        .HasForeignKey("EpisodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MediaInAction.VideoService.MovieAliasNs.MovieAlias", b =>
                {
                    b.HasOne("MediaInAction.VideoService.MovieNs.Movie", null)
                        .WithMany("MovieAliases")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MediaInAction.VideoService.SeriesAliasNs.SeriesAlias", b =>
                {
                    b.HasOne("MediaInAction.VideoService.SeriesNs.Series", null)
                        .WithMany("SeriesAliases")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MediaInAction.VideoService.TraktRequestNs.TraktRequestItem", b =>
                {
                    b.HasOne("MediaInAction.VideoService.TraktRequestNs.TraktRequest", null)
                        .WithMany("RequestItems")
                        .HasForeignKey("TraktRequestId");
                });

            modelBuilder.Entity("MediaInAction.VideoService.EpisodeNs.Episode", b =>
                {
                    b.Navigation("EpisodeAliases");
                });

            modelBuilder.Entity("MediaInAction.VideoService.MovieNs.Movie", b =>
                {
                    b.Navigation("MovieAliases");
                });

            modelBuilder.Entity("MediaInAction.VideoService.SeriesNs.Series", b =>
                {
                    b.Navigation("SeriesAliases");
                });

            modelBuilder.Entity("MediaInAction.VideoService.TraktRequestNs.TraktRequest", b =>
                {
                    b.Navigation("RequestItems");
                });
#pragma warning restore 612, 618
        }
    }
}
