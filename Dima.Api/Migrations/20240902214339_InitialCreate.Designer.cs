﻿// <auto-generated />
using System;
using Dima.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Dima.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240902214339_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Dima.Core.Models.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(245)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("description");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("title");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("varchar(160)")
                        .HasColumnName("userId");

                    b.HasKey("Id");

                    b.ToTable("category", (string)null);
                });

            modelBuilder.Entity("Dima.Core.Models.Transaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("DECIMAL")
                        .HasColumnName("amount");

                    b.Property<long>("CategoryId")
                        .HasColumnType("bigint")
                        .HasColumnName("categoryId");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("DATETIME")
                        .HasColumnName("createAt");

                    b.Property<DateTime?>("PaidOrReceivedAt")
                        .HasColumnType("DATETIME")
                        .HasColumnName("paidOrReceivedAt");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("title");

                    b.Property<sbyte>("Type")
                        .HasColumnType("TINYINT")
                        .HasColumnName("type");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("userId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("transaction", (string)null);
                });

            modelBuilder.Entity("Dima.Core.Models.Transaction", b =>
                {
                    b.HasOne("Dima.Core.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
