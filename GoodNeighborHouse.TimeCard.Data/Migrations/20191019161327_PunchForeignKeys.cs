using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodNeighborHouse.TimeCard.Data.Migrations
{
    public partial class PunchForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "LastUpdatedBy",
                table: "Punches",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "Punches",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Punches_CreatedBy",
                table: "Punches",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Punches_LastUpdatedBy",
                table: "Punches",
                column: "LastUpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Punches_Volunteers_CreatedBy",
                table: "Punches",
                column: "CreatedBy",
                principalTable: "Volunteers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Punches_Volunteers_LastUpdatedBy",
                table: "Punches",
                column: "LastUpdatedBy",
                principalTable: "Volunteers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Punches_Volunteers_CreatedBy",
                table: "Punches");

            migrationBuilder.DropForeignKey(
                name: "FK_Punches_Volunteers_LastUpdatedBy",
                table: "Punches");

            migrationBuilder.DropIndex(
                name: "IX_Punches_CreatedBy",
                table: "Punches");

            migrationBuilder.DropIndex(
                name: "IX_Punches_LastUpdatedBy",
                table: "Punches");

            migrationBuilder.AlterColumn<Guid>(
                name: "LastUpdatedBy",
                table: "Punches",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "Punches",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid));
        }
    }
}
