using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodNeighborHouse.TimeCard.Data.Migrations
{
    public partial class Reconciliation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reconciliations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    In = table.Column<Guid>(nullable: false),
                    Out = table.Column<Guid>(nullable: false),
                    ApprovedBy = table.Column<int>(nullable: false),
                    ApprovedOn = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Difference = table.Column<long>(type: "BIGINT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reconciliations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reconciliations_Punches_In",
                        column: x => x.In,
                        principalTable: "Punches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Reconciliations_Punches_Out",
                        column: x => x.Out,
                        principalTable: "Punches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

        }
        

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
             name: "Reconciliations");

        }
    }
}
