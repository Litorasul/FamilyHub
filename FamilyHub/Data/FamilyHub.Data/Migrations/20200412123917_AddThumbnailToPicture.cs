using Microsoft.EntityFrameworkCore.Migrations;

namespace FamilyHub.Data.Migrations
{
    public partial class AddThumbnailToPicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThumbUrl",
                table: "Pictures",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbUrl",
                table: "Pictures");
        }
    }
}
