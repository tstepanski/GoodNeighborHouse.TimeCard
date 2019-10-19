﻿// <auto-generated />

using System;
using GoodNeighborHouse.TimeCard.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GoodNeighborHouse.TimeCard.Data.Migrations
{
	[DbContext(typeof(GNHContext))]
	partial class GNHContextModelSnapshot : ModelSnapshot
	{
		protected override void BuildModel(ModelBuilder modelBuilder)
		{
#pragma warning disable 612, 618
			modelBuilder
				.HasAnnotation("ProductVersion", "3.0.0")
				.HasAnnotation("Relational:MaxIdentifierLength", 128)
				.HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

			modelBuilder.Entity("GoodNeighborHouse.TimeCard.Data.Department", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("UNIQUEIDENTIFIER")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            ID = new Guid("00000000-0000-0000-0000-000000000001"),
                            Name = "Dental"
                        },
                        new
                        {
                            ID = new Guid("00000000-0000-0000-0000-000000000002"),
                            Name = "Human Services"
                        },
                        new
                        {
                            ID = new Guid("00000000-0000-0000-0000-000000000003"),
                            Name = "Medical"
                        });
                });

            modelBuilder.Entity("GoodNeighborHouse.TimeCard.Data.Organization", b =>
			{
				b.Property<Guid>("ID")
					.ValueGeneratedOnAdd()
					.HasColumnName("Id")
					.HasColumnType("UNIQUEIDENTIFIER")
					.HasDefaultValueSql("NEWSEQUENTIALID()");

				b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("GoodNeighborHouse.TimeCard.Data.Punch", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("UNIQUEIDENTIFIER")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("CreatedAt")
                        .HasColumnType("DATETIME");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnName("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnName("DepartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsClockIn")
                        .HasColumnName("IsClockIn")
                        .HasColumnType("BIT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("IsDeleted")
                        .HasColumnType("BIT");

                    b.Property<Guid>("LastUpdatedBy")
                        .HasColumnName("LastUpdatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("PunchTime")
                        .HasColumnName("PunchTime")
                        .HasColumnType("DATETIME");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnName("UpdatedAt")
                        .HasColumnType("DATETIME");

                    b.Property<Guid>("VolunteerId")
                        .HasColumnName("VolunteerId")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.HasKey("Id");

                    b.HasIndex("VolunteerId");

                    b.ToTable("Punches");
                });

            modelBuilder.Entity("GoodNeighborHouse.TimeCard.Data.Volunteer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("UNIQUEIDENTIFIER")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<DateTime>("DateOfBirth")
					.HasColumnName("DOB")
					.HasColumnType("datetime");

				b.Property<string>("FirstName")
					.IsRequired()
					.HasColumnName("FirstName")
					.HasColumnType("nvarchar(100)")
					.HasMaxLength(100);

				b.Property<string>("LastName")
					.IsRequired()
					.HasColumnName("LastName")
					.HasColumnType("nvarchar(100)")
					.HasMaxLength(100);

				b.Property<string>("Pin")
					.IsRequired()
					.HasColumnName("PIN")
					.HasColumnType("nvarchar(6)")
					.HasMaxLength(6);

				b.HasKey("Id");

				b.ToTable("Volunteers");
			});

            modelBuilder.Entity("GoodNeighborHouse.TimeCard.Data.Punch", b =>
                {
                    b.HasOne("GoodNeighborHouse.TimeCard.Data.Volunteer", "Volunteer")
                        .WithMany()
                        .HasForeignKey("VolunteerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
		}
	}
}