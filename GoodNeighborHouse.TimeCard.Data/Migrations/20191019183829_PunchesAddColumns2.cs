using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodNeighborHouse.TimeCard.Data.Migrations
{
    public partial class PunchesAddColumns2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedBy",
                table: "Punches",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdatedBy",
                table: "Punches");
        }
    }
}
