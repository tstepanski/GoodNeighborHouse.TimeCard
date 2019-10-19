using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodNeighborHouse.TimeCard.Data.Migrations
{
    public partial class PunchRemoveFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastUpdatedBy",
                table: "Punches",
                type: "STRING",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Punches",
                type: "STRING",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateTable(
                name: "Recon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    In = table.Column<Guid>(nullable: false),
                    Out = table.Column<Guid>(nullable: false),
                    ApprovedBy = table.Column<int>(nullable: false),
                    ApprovedOn = table.Column<DateTime>(nullable: false),
                    Difference = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recon_Punches_In",
                        column: x => x.In,
                        principalTable: "Punches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recon_Punches_Out",
                        column: x => x.Out,
                        principalTable: "Punches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recon_In",
                table: "Recon",
                column: "In");

            migrationBuilder.CreateIndex(
                name: "IX_Recon_Out",
                table: "Recon",
                column: "Out");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recon");

            migrationBuilder.AlterColumn<Guid>(
                name: "LastUpdatedBy",
                table: "Punches",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "STRING",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "Punches",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "STRING",
                oldMaxLength: 100);
        }
    }
}
