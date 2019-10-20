using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodNeighborHouse.TimeCard.Data.Migrations
{
	public partial class Initial : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Departments",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false,
						defaultValueSql: "NEWSEQUENTIALID()"),
					Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
				},
				constraints: table => { table.PrimaryKey("PK_Departments", x => x.Id); });

			migrationBuilder.CreateTable(
				name: "Organizations",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false,
						defaultValueSql: "NEWSEQUENTIALID()"),
					Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
				},
				constraints: table => { table.PrimaryKey("PK_Organizations", x => x.Id); });

			migrationBuilder.CreateTable(
				name: "Volunteers",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false,
						defaultValueSql: "NEWSEQUENTIALID()"),
					FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
					LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					OrganizationId = table.Column<Guid>(nullable: true),
					Username = table.Column<string>(type: "nvarchar(250)", maxLength: 6, nullable: false),
					IsPaid = table.Column<bool>(type: "bit", nullable: false),
					IsGroup = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table => { table.PrimaryKey("PK_Volunteers", x => x.Id); });

			migrationBuilder.CreateTable(
				name: "DepartmentVolunteers",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false,
						defaultValueSql: "NEWSEQUENTIALID()"),
					DepartmentId = table.Column<Guid>(nullable: false),
					VolunteerId = table.Column<Guid>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_DepartmentVolunteers", x => x.Id);
					table.ForeignKey(
						name: "FK_DepartmentVolunteers_Departments_DepartmentId",
						column: x => x.DepartmentId,
						principalTable: "Departments",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_DepartmentVolunteers_Volunteers_VolunteerId",
						column: x => x.VolunteerId,
						principalTable: "Volunteers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "Punches",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false,
						defaultValueSql: "NEWSEQUENTIALID()"),
					VolunteerId = table.Column<Guid>(nullable: false),
					DepartmentId = table.Column<Guid>(nullable: false),
					IsClockIn = table.Column<bool>(type: "BIT", nullable: false),
					PunchTime = table.Column<DateTime>(type: "DATETIME", nullable: false),
					IsDeleted = table.Column<bool>(type: "BIT", nullable: false),
					CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
					UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
					Quantity = table.Column<int>(nullable: false),
					LastUpdatedBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
					CreatedBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Punches", x => x.Id);
					table.ForeignKey(
						name: "FK_Punches_Departments_DepartmentId",
						column: x => x.DepartmentId,
						principalTable: "Departments",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Punches_Volunteers_VolunteerId",
						column: x => x.VolunteerId,
						principalTable: "Volunteers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "Reconciliations",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false,
						defaultValueSql: "NEWSEQUENTIALID()"),
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
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Reconciliations_Punches_Out",
						column: x => x.Out,
						principalTable: "Punches",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.InsertData(
				table: "Departments",
				columns: new[] {"Id", "Name"},
				values: new object[] {new Guid("b4240b56-6ff2-e911-9ae8-d0c637a95ae1"), "Dental"});

			migrationBuilder.InsertData(
				table: "Departments",
				columns: new[] {"Id", "Name"},
				values: new object[] {new Guid("b5240b56-6ff2-e911-9ae8-d0c637a95ae1"), "Human Services"});

			migrationBuilder.InsertData(
				table: "Departments",
				columns: new[] {"Id", "Name"},
				values: new object[] {new Guid("b6240b56-6ff2-e911-9ae8-d0c637a95ae1"), "Medical"});

			migrationBuilder.CreateIndex(
				name: "IX_DepartmentVolunteers_DepartmentId",
				table: "DepartmentVolunteers",
				column: "DepartmentId");

			migrationBuilder.CreateIndex(
				name: "IX_DepartmentVolunteers_VolunteerId",
				table: "DepartmentVolunteers",
				column: "VolunteerId");

			migrationBuilder.CreateIndex(
				name: "IX_Punches_DepartmentId",
				table: "Punches",
				column: "DepartmentId");

			migrationBuilder.CreateIndex(
				name: "IX_Punches_VolunteerId",
				table: "Punches",
				column: "VolunteerId");

			migrationBuilder.CreateIndex(
				name: "IX_Reconciliations_In",
				table: "Reconciliations",
				column: "In");

			migrationBuilder.CreateIndex(
				name: "IX_Reconciliations_Out",
				table: "Reconciliations",
				column: "Out");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "DepartmentVolunteers");

			migrationBuilder.DropTable(
				name: "Organizations");

			migrationBuilder.DropTable(
				name: "Reconciliations");

			migrationBuilder.DropTable(
				name: "Punches");

			migrationBuilder.DropTable(
				name: "Departments");

			migrationBuilder.DropTable(
				name: "Volunteers");
		}
	}
}