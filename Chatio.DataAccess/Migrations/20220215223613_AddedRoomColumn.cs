using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chatio.DataAccess.Migrations
{
    public partial class AddedRoomColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentUser",
                table: "Messages");

            migrationBuilder.AddColumn<string>(
                name: "Room",
                table: "Messages",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Room",
                table: "Messages");

            migrationBuilder.AddColumn<bool>(
                name: "CurrentUser",
                table: "Messages",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
