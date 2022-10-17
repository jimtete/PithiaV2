﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PithiaV2.Data;

#nullable disable

namespace PithiaV2.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221017103225_database_v_0_1_0")]
    partial class database_v_0_1_0
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PithiaV2.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CourseCharacteristic")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("LabHours")
                        .HasColumnType("integer");

                    b.Property<int>("Specialization")
                        .HasColumnType("integer");

                    b.Property<int>("TheoryHours")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("PithiaV2.Models.StudentXCourse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CourseId")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("UserId");

                    b.ToTable("StudentXCourses");
                });

            modelBuilder.Entity("PithiaV2.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BirthYear")
                        .HasColumnType("integer");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SchoolCharacteristic")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)");

                    b.Property<int>("age")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PithiaV2.Models.StudentXCourse", b =>
                {
                    b.HasOne("PithiaV2.Models.Course", "Course")
                        .WithMany("StudentXCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PithiaV2.Models.User", "User")
                        .WithMany("StudentXCourses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PithiaV2.Models.Course", b =>
                {
                    b.Navigation("StudentXCourses");
                });

            modelBuilder.Entity("PithiaV2.Models.User", b =>
                {
                    b.Navigation("StudentXCourses");
                });
#pragma warning restore 612, 618
        }
    }
}
