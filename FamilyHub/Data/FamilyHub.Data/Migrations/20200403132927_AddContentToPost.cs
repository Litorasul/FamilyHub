using Microsoft.EntityFrameworkCore.Migrations;

namespace FamilyHub.Data.Migrations
{
    public partial class AddContentToPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedEntityId",
                table: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "AssignedEntity",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedEntity",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "AssignedEntityId",
                table: "Posts",
                type: "int",
                nullable: true);
        }
    }
}
