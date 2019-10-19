using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodNeighborHouse.TimeCard.Data.Migrations
{
    public partial class UpdateVolunteerModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PIN",
                table: "Volunteers");

            migrationBuilder.AddColumn<bool>(
                name: "IsGroup",
                table: "Volunteers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Volunteers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Volunteers",
                type: "nvarchar(250)",
                maxLength: 6,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGroup",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Volunteers");

            migrationBuilder.AddColumn<string>(
                name: "PIN",
                table: "Volunteers",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "");
        }
    }
}
