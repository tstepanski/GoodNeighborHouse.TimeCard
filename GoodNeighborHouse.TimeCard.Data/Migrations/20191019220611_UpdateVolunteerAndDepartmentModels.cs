using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodNeighborHouse.TimeCard.Data.Migrations
{
    public partial class UpdateVolunteerAndDepartmentModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DOB",
                table: "Volunteers");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Volunteers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "Volunteers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DepartmentVolunteers",
                columns: table => new
                {
                    DepartmentId = table.Column<Guid>(nullable: false),
                    VolunteerId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWSEQUENTIALID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentVolunteers", x => new { x.DepartmentId, x.VolunteerId });
                    table.ForeignKey(
                        name: "FK_DepartmentVolunteers_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentVolunteers_Volunteers_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentVolunteers_VolunteerId",
                table: "DepartmentVolunteers",
                column: "VolunteerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentVolunteers");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Volunteers");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Volunteers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DOB",
                table: "Volunteers",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
