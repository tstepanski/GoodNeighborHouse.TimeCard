using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodNeighborHouse.TimeCard.Data.Migrations
{
    public partial class PunchesToDepartments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.AlterColumn<Guid>(
                name: "DepartmentId",
                table: "Punches",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("b4240b56-6ff2-e911-9ae8-d0c637a95ae1"), "Dental" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("b5240b56-6ff2-e911-9ae8-d0c637a95ae1"), "Human Services" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("b6240b56-6ff2-e911-9ae8-d0c637a95ae1"), "Medical" });

            migrationBuilder.CreateIndex(
                name: "IX_Punches_DepartmentId",
                table: "Punches",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Punches_Departments_DepartmentId",
                table: "Punches",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Punches_Departments_DepartmentId",
                table: "Punches");

            migrationBuilder.DropIndex(
                name: "IX_Punches_DepartmentId",
                table: "Punches");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("b4240b56-6ff2-e911-9ae8-d0c637a95ae1"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("b5240b56-6ff2-e911-9ae8-d0c637a95ae1"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("b6240b56-6ff2-e911-9ae8-d0c637a95ae1"));

            migrationBuilder.AlterColumn<Guid>(
                name: "DepartmentId",
                table: "Punches",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), "Dental" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000002"), "Human Services" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000003"), "Medical" });
        }
    }
}
