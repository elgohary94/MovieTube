using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieTube.Migrations
{
    /// <inheritdoc />
    public partial class updateposterdatatype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Genres_GenreID",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "GenreID",
                table: "Movies",
                newName: "GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_Movies_GenreID",
                table: "Movies",
                newName: "IX_Movies_GenreId");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Poster",
                table: "Movies",
                type: "varbinary(MAX)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Genres_GenreId",
                table: "Movies",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Genres_GenreId",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "Movies",
                newName: "GenreID");

            migrationBuilder.RenameIndex(
                name: "IX_Movies_GenreId",
                table: "Movies",
                newName: "IX_Movies_GenreID");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Poster",
                table: "Movies",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(MAX)");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Genres_GenreID",
                table: "Movies",
                column: "GenreID",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
