﻿// <auto-generated />
using System;
using FundRaiser_Team1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FundRaiser_Team1.Migrations
{
    [DbContext(typeof(FundRaiserDbContext))]
    [Migration("20211129184852_try")]
    partial class @try
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FundRaiser_Team1.Models.Package", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PackageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PackagePrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Package");
                });

            modelBuilder.Entity("FundRaiser_Team1.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PackageId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId1")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PackageId");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("FundRaiser_Team1.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("FundRaiser_Team1.Models.Project", b =>
                {
                    b.HasOne("FundRaiser_Team1.Models.Package", null)
                        .WithMany("Projects")
                        .HasForeignKey("PackageId");

                    b.HasOne("FundRaiser_Team1.Models.User", null)
                        .WithMany("CreatedProjects")
                        .HasForeignKey("UserId");

                    b.HasOne("FundRaiser_Team1.Models.User", null)
                        .WithMany("FundedProjects")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("FundRaiser_Team1.Models.Package", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("FundRaiser_Team1.Models.User", b =>
                {
                    b.Navigation("CreatedProjects");

                    b.Navigation("FundedProjects");
                });
#pragma warning restore 612, 618
        }
    }
}