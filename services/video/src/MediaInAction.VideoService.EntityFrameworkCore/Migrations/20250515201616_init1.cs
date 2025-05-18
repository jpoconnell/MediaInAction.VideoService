using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaInAction.VideoService.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Episodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SeriesId = table.Column<Guid>(type: "uuid", nullable: false),
                    SeasonNum = table.Column<int>(type: "integer", nullable: false),
                    EpisodeNum = table.Column<int>(type: "integer", nullable: false),
                    EpisodeStatus = table.Column<int>(type: "integer", nullable: false),
                    AiredDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EpisodeName = table.Column<string>(type: "text", nullable: true),
                    AltEpisodeId = table.Column<string>(type: "text", nullable: true),
                    SeasonEpisode = table.Column<string>(type: "text", nullable: true),
                    Source = table.Column<string>(type: "text", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    FirstAiredYear = table.Column<int>(type: "integer", nullable: false),
                    MovieStatus = table.Column<int>(type: "integer", nullable: false),
                    ImageName = table.Column<string>(type: "text", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seriess",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    FirstAiredYear = table.Column<int>(type: "integer", nullable: false),
                    SeriesStatus = table.Column<int>(type: "integer", nullable: false),
                    ImageName = table.Column<string>(type: "text", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seriess", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ToBeMappeds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Alias = table.Column<string>(type: "text", nullable: true),
                    FromService = table.Column<int>(type: "integer", nullable: false),
                    FromId = table.Column<string>(type: "text", nullable: true),
                    Tries = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToBeMappeds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EpisodeAlias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EpisodeId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdType = table.Column<string>(type: "text", nullable: false),
                    IdValue = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EpisodeAlias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EpisodeAlias_Episodes_EpisodeId",
                        column: x => x.EpisodeId,
                        principalTable: "Episodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieAliases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MovieId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdType = table.Column<string>(type: "text", nullable: false),
                    IdValue = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieAliases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieAliases_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeriesAliases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SeriesId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdType = table.Column<string>(type: "text", nullable: false),
                    IdValue = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeriesAliases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeriesAliases_Seriess_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Seriess",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EpisodeAlias_EpisodeId",
                table: "EpisodeAlias",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieAliases_MovieId_IdType_IdValue",
                table: "MovieAliases",
                columns: new[] { "MovieId", "IdType", "IdValue" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_Name_FirstAiredYear",
                table: "Movies",
                columns: new[] { "Name", "FirstAiredYear" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SeriesAliases_SeriesId_IdType_IdValue",
                table: "SeriesAliases",
                columns: new[] { "SeriesId", "IdType", "IdValue" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seriess_Name_FirstAiredYear",
                table: "Seriess",
                columns: new[] { "Name", "FirstAiredYear" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EpisodeAlias");

            migrationBuilder.DropTable(
                name: "MovieAliases");

            migrationBuilder.DropTable(
                name: "SeriesAliases");

            migrationBuilder.DropTable(
                name: "ToBeMappeds");

            migrationBuilder.DropTable(
                name: "Episodes");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Seriess");
        }
    }
}
