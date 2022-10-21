﻿// <auto-generated />
using System;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221001144242_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Core.Domain.Entities.Canteen", b =>
                {
                    b.Property<int>("CanteenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CanteenId"), 1L, 1);

                    b.Property<int>("City")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("OfferingHotMeals")
                        .IsRequired()
                        .HasColumnType("bit");

                    b.HasKey("CanteenId");

                    b.ToTable("Canteens");
                });

            modelBuilder.Entity("Core.Domain.Entities.CanteenEmployee", b =>
                {
                    b.Property<int>("CanteenEmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CanteenEmployeeId"), 1L, 1);

                    b.Property<string>("EmployeeNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CanteenEmployeeId");

                    b.ToTable("CanteenEmployees");
                });

            modelBuilder.Entity("Core.Domain.Entities.Packet", b =>
                {
                    b.Property<int>("PacketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PacketId"), 1L, 1);

                    b.Property<int>("CanteenId")
                        .HasColumnType("int");

                    b.Property<int>("City")
                        .HasColumnType("int");

                    b.Property<bool?>("IsEightteenPlusPacket")
                        .IsRequired()
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LatestPickUpTime")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<int>("MealType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("PickUpDateTime")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<double?>("Price")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<int?>("ReservedByStudentId")
                        .HasColumnType("int");

                    b.HasKey("PacketId");

                    b.HasIndex("CanteenId");

                    b.HasIndex("ReservedByStudentId");

                    b.ToTable("Packets");
                });

            modelBuilder.Entity("Core.Domain.Entities.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"), 1L, 1);

                    b.Property<bool?>("IsAlcoholic")
                        .IsRequired()
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PacketId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Picture")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("ProductId");

                    b.HasIndex("PacketId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Core.Domain.Entities.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"), 1L, 1);

                    b.Property<int>("AmountOfReports")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateOfBirth")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudyCity")
                        .HasColumnType("int");

                    b.HasKey("StudentId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Core.Domain.Entities.Packet", b =>
                {
                    b.HasOne("Core.Domain.Entities.Canteen", "Canteen")
                        .WithMany()
                        .HasForeignKey("CanteenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.Entities.Student", "ReservedBy")
                        .WithMany()
                        .HasForeignKey("ReservedByStudentId");

                    b.Navigation("Canteen");

                    b.Navigation("ReservedBy");
                });

            modelBuilder.Entity("Core.Domain.Entities.Product", b =>
                {
                    b.HasOne("Core.Domain.Entities.Packet", null)
                        .WithMany("Products")
                        .HasForeignKey("PacketId");
                });

            modelBuilder.Entity("Core.Domain.Entities.Packet", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
