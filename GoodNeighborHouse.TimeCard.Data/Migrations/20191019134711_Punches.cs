using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodNeighborHouse.TimeCard.Data.Migrations
{
    public partial class Punches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Punches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    VolunteerId = table.Column<Guid>(nullable: false),
                    DepartmentId = table.Column<Guid>(nullable: false),
                    IsClockIn = table.Column<bool>(type: "BIT", nullable: false),
                    PunchTime = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    IsDeleted = table.Column<bool>(type: "BIT", nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    LastUpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Punches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Punches_Volunteers_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Punches_VolunteerId",
                table: "Punches",
                column: "VolunteerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Punches");
        }
    }
}
