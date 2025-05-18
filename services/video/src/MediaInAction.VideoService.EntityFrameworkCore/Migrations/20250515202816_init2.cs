using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaInAction.VideoService.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EpisodeAlias_Episodes_EpisodeId",
                table: "EpisodeAlias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EpisodeAlias",
                table: "EpisodeAlias");

            migrationBuilder.DropIndex(
                name: "IX_EpisodeAlias_EpisodeId",
                table: "EpisodeAlias");

            migrationBuilder.RenameTable(
                name: "EpisodeAlias",
                newName: "EpisodeAliases");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EpisodeAliases",
                table: "EpisodeAliases",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ToBeMappeds_Alias",
                table: "ToBeMappeds",
                column: "Alias",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_SeriesId_SeasonNum_EpisodeNum",
                table: "Episodes",
                columns: new[] { "SeriesId", "SeasonNum", "EpisodeNum" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EpisodeAliases_EpisodeId_IdType_IdValue",
                table: "EpisodeAliases",
                columns: new[] { "EpisodeId", "IdType", "IdValue" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EpisodeAliases_Episodes_EpisodeId",
                table: "EpisodeAliases",
                column: "EpisodeId",
                principalTable: "Episodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EpisodeAliases_Episodes_EpisodeId",
                table: "EpisodeAliases");

            migrationBuilder.DropIndex(
                name: "IX_ToBeMappeds_Alias",
                table: "ToBeMappeds");

            migrationBuilder.DropIndex(
                name: "IX_Episodes_SeriesId_SeasonNum_EpisodeNum",
                table: "Episodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EpisodeAliases",
                table: "EpisodeAliases");

            migrationBuilder.DropIndex(
                name: "IX_EpisodeAliases_EpisodeId_IdType_IdValue",
                table: "EpisodeAliases");

            migrationBuilder.RenameTable(
                name: "EpisodeAliases",
                newName: "EpisodeAlias");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EpisodeAlias",
                table: "EpisodeAlias",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_EpisodeAlias_EpisodeId",
                table: "EpisodeAlias",
                column: "EpisodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EpisodeAlias_Episodes_EpisodeId",
                table: "EpisodeAlias",
                column: "EpisodeId",
                principalTable: "Episodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
