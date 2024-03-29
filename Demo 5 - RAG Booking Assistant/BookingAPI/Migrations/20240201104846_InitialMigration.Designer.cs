﻿// <auto-generated />
using BookingAPI.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookingAPI.Migrations
{
    [DbContext(typeof(BookingDbContext))]
    [Migration("20240201104846_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookingAPI.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CourseCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseCode");

                    b.HasIndex("CustomerId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("BookingAPI.Models.Course", b =>
                {
                    b.Property<string>("CourseCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("OrganizationDates")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CourseCode");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            CourseCode = "UNASP",
                            OrganizationDates = "[\"2024-02-20T00:00:00\",\"2024-03-21T00:00:00\",\"2024-04-15T00:00:00\"]"
                        },
                        new
                        {
                            CourseCode = "UCSPR",
                            OrganizationDates = "[\"2024-02-21T00:00:00\",\"2024-03-22T00:00:00\",\"2024-04-17T00:00:00\"]"
                        },
                        new
                        {
                            CourseCode = "UNOOP",
                            OrganizationDates = "[\"2024-02-22T00:00:00\",\"2024-03-23T00:00:00\",\"2024-04-18T00:00:00\"]"
                        },
                        new
                        {
                            CourseCode = "UTSQL",
                            OrganizationDates = "[\"2024-02-23T00:00:00\",\"2024-03-24T00:00:00\",\"2024-04-19T00:00:00\"]"
                        },
                        new
                        {
                            CourseCode = "USJWEB",
                            OrganizationDates = "[\"2024-02-24T00:00:00\",\"2024-03-25T00:00:00\",\"2024-04-20T00:00:00\"]"
                        });
                });

            modelBuilder.Entity("BookingAPI.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("BookingAPI.Models.Booking", b =>
                {
                    b.HasOne("BookingAPI.Models.Course", "Course")
                        .WithMany("Bookings")
                        .HasForeignKey("CourseCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookingAPI.Models.Customer", "Customer")
                        .WithMany("Bookings")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("BookingAPI.Models.Course", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("BookingAPI.Models.Customer", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
