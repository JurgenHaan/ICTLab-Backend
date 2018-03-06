﻿// <auto-generated />
using ICTLab_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace ICTLabbackend.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20180219104313_Firstmigration2")]
    partial class Firstmigration2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("ICTLab_backend.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<string>("UserType");

                    b.Property<string>("fullName");

                    b.HasKey("ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ICTLab_backend.Models.UserSession", b =>
                {
                    b.Property<int>("UserID");

                    b.Property<DateTime>("ExpireDate");

                    b.Property<string>("SessionKey");

                    b.HasKey("UserID");

                    b.ToTable("UserSession");
                });

            modelBuilder.Entity("ICTLab_backend.Models.UserSession", b =>
                {
                    b.HasOne("ICTLab_backend.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
