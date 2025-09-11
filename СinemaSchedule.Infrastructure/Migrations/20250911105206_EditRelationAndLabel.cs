using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace СinemaSchedule.Migrations
{
    /// <inheritdoc />
    public partial class EditRelationAndLabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Genre_GenreEntityId",
                table: "Movie");

            migrationBuilder.DropIndex(
                name: "IX_Movie_GenreEntityId",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "GenreEntityId",
                table: "Movie");

            migrationBuilder.AlterColumn<string>(
                name: "AgeLimit",
                table: "Movie",
                type: "text",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "smallint");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Movie",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Movie");

            migrationBuilder.AlterColumn<byte>(
                name: "AgeLimit",
                table: "Movie",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "GenreEntityId",
                table: "Movie",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movie_GenreEntityId",
                table: "Movie",
                column: "GenreEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Genre_GenreEntityId",
                table: "Movie",
                column: "GenreEntityId",
                principalTable: "Genre",
                principalColumn: "Id");
        }
    }
}
