﻿// <auto-generated />
using System;
using GoodNeighborHouse.TimeCard.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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

            modelBuilder.Entity("GoodNeighborHouse.TimeCard.Data.Entities.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("UNIQUEIDENTIFIER")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b4240b56-6ff2-e911-9ae8-d0c637a95ae1"),
                            Name = "Dental"
                        },
                        new
                        {
                            Id = new Guid("b5240b56-6ff2-e911-9ae8-d0c637a95ae1"),
                            Name = "Human Services"
                        },
                        new
                        {
                            Id = new Guid("b6240b56-6ff2-e911-9ae8-d0c637a95ae1"),
                            Name = "Medical"
                        });
                });

            modelBuilder.Entity("GoodNeighborHouse.TimeCard.Data.Entities.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("UNIQUEIDENTIFIER")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("GoodNeighborHouse.TimeCard.Data.Entities.Punch", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("UNIQUEIDENTIFIER")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("CreatedAt")
                        .HasColumnType("DATETIME");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnName("CreatedBy")
                        .HasColumnType("varchar(500)")
                        .HasMaxLength(500);

                    b.Property<Guid>("DepartmentId")
                        .HasColumnName("DepartmentId")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<bool>("IsClockIn")
                        .HasColumnName("IsClockIn")
                        .HasColumnType("BIT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("IsDeleted")
                        .HasColumnType("BIT");

                    b.Property<string>("LastUpdatedBy")
                        .IsRequired()
                        .HasColumnName("LastUpdatedBy")
                        .HasColumnType("varchar(500)")
                        .HasMaxLength(500);

                    b.Property<DateTime>("PunchTime")
                        .HasColumnName("PunchTime")
                        .HasColumnType("DATETIME");

                    b.Property<int>("Quantity")
                        .HasColumnName("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnName("UpdatedAt")
                        .HasColumnType("DATETIME");

                    b.Property<Guid>("VolunteerId")
                        .HasColumnName("VolunteerId")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("VolunteerId");

                    b.ToTable("Punches");
                });

            modelBuilder.Entity("GoodNeighborHouse.TimeCard.Data.Entities.Reconciliation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("UNIQUEIDENTIFIER")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<int>("ApprovedBy")
                        .HasColumnName("ApprovedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("ApprovedOn")
                        .HasColumnName("ApprovedOn")
                        .HasColumnType("DATETIME");

                    b.Property<long>("Difference")
                        .HasColumnName("Difference")
                        .HasColumnType("BIGINT");

                    b.Property<Guid>("PunchInId")
                        .HasColumnName("In")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<Guid>("PunchOutId")
                        .HasColumnName("Out")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.HasKey("Id");

                    b.HasIndex("PunchInId");

                    b.HasIndex("PunchOutId");

                    b.ToTable("Reconciliations");
                });

            modelBuilder.Entity("GoodNeighborHouse.TimeCard.Data.Entities.Volunteer", b =>
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

                    b.Property<bool>("IsGroup")
                        .HasColumnName("IsGroup")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPaid")
                        .HasColumnName("IsPaid")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnName("LastName")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnName("Username")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(6);

                    b.HasKey("Id");

                    b.ToTable("Volunteers");
                });

            modelBuilder.Entity("GoodNeighborHouse.TimeCard.Data.Entities.Punch", b =>
                {
                    b.HasOne("GoodNeighborHouse.TimeCard.Data.Entities.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GoodNeighborHouse.TimeCard.Data.Entities.Volunteer", "Volunteer")
                        .WithMany()
                        .HasForeignKey("VolunteerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GoodNeighborHouse.TimeCard.Data.Entities.Reconciliation", b =>
                {
                    b.HasOne("GoodNeighborHouse.TimeCard.Data.Entities.Punch", "PunchIn")
                        .WithMany()
                        .HasForeignKey("PunchInId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GoodNeighborHouse.TimeCard.Data.Entities.Punch", "PunchOut")
                        .WithMany()
                        .HasForeignKey("PunchOutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
