namespace FamilyHub.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddAlbumIdToPicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlbumId",
                table: "Pictures",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_AlbumId",
                table: "Pictures",
                column: "AlbumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_PictureAlbums_AlbumId",
                table: "Pictures",
                column: "AlbumId",
                principalTable: "PictureAlbums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_PictureAlbums_AlbumId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_AlbumId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "AlbumId",
                table: "Pictures");
        }
    }
}
