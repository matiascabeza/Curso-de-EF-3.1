﻿// <auto-generated />
using System;
using DemoEFCoreWinforms.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCore_Mod3.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200323190020_student_remove")]
    partial class student_remove
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DemoEFCoreWinforms.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateBirth")
                        .HasColumnType("datetime2");

                    b.Property<bool>("ItsErased")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            Id = 25,
                            DateBirth = new DateTime(1999, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ItsErased = false,
                            Name = "JuanCarlos"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
