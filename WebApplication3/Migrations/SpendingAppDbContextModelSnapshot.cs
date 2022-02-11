﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication3.Model;

#nullable disable

namespace WebApplication3.Migrations
{
    [DbContext(typeof(SpendingAppDbContext))]
    partial class SpendingAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebApplication3.Model.AccountVerificationToken", b =>
                {
                    b.Property<string>("token")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("expirationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("useremail")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("token");

                    b.HasIndex("useremail");

                    b.ToTable("AccountVerificationToken");
                });

            modelBuilder.Entity("WebApplication3.Model.PwRecoveryToken", b =>
                {
                    b.Property<string>("token")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("expirationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("useremail")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("token");

                    b.HasIndex("useremail");

                    b.ToTable("PwRecoveryToken");
                });

            modelBuilder.Entity("WebApplication3.Model.Spending", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<int>("price")
                        .HasColumnType("int");

                    b.Property<string>("product")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("productCategory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("subuserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("subuserId");

                    b.ToTable("Spending");
                });

            modelBuilder.Entity("WebApplication3.Model.SubUser", b =>
                {
                    b.Property<int>("subuserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("subuserId"), 1L, 1);

                    b.Property<string>("Useremail")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("subUserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("subuserId");

                    b.HasIndex("Useremail");

                    b.ToTable("SubUser");
                });

            modelBuilder.Entity("WebApplication3.Model.User", b =>
                {
                    b.Property<string>("email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("verified")
                        .HasColumnType("bit");

                    b.HasKey("email");

                    b.ToTable("User");
                });

            modelBuilder.Entity("WebApplication3.Model.AccountVerificationToken", b =>
                {
                    b.HasOne("WebApplication3.Model.User", "user")
                        .WithMany()
                        .HasForeignKey("useremail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("WebApplication3.Model.PwRecoveryToken", b =>
                {
                    b.HasOne("WebApplication3.Model.User", "user")
                        .WithMany()
                        .HasForeignKey("useremail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("WebApplication3.Model.Spending", b =>
                {
                    b.HasOne("WebApplication3.Model.SubUser", "subUser")
                        .WithMany("Spendings")
                        .HasForeignKey("subuserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("subUser");
                });

            modelBuilder.Entity("WebApplication3.Model.SubUser", b =>
                {
                    b.HasOne("WebApplication3.Model.User", "User")
                        .WithMany("SubUsers")
                        .HasForeignKey("Useremail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApplication3.Model.SubUser", b =>
                {
                    b.Navigation("Spendings");
                });

            modelBuilder.Entity("WebApplication3.Model.User", b =>
                {
                    b.Navigation("SubUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
