﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using P7CreateRestApi.Data;

#nullable disable

namespace P7CreateRestApi.Migrations
{
    [DbContext(typeof(LocalDbContext))]
    [Migration("20231115162934_7777")]
    partial class _7777
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("P7CreateRestApi.Domain.BidList", b =>
                {
                    b.Property<int>("BidListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BidListId"), 1L, 1);

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Ask")
                        .HasColumnType("float");

                    b.Property<double?>("AskQuantity")
                        .HasColumnType("float");

                    b.Property<string>("Benchmark")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("BidAmount")
                        .HasColumnType("float");

                    b.Property<DateTime?>("BidListDate")
                        .HasColumnType("datetime2");

                    b.Property<double?>("BidQuantity")
                        .HasColumnType("float");

                    b.Property<string>("BidSecurity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BidStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BidType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Book")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Commentary")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DealName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DealType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RevisionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RevisionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Side")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SourceListId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Trader")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BidListId");

                    b.ToTable("BidLists");
                });

            modelBuilder.Entity("P7CreateRestApi.Domain.CurvePoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("AsOfDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<byte?>("CurveId")
                        .HasColumnType("tinyint");

                    b.Property<double?>("CurvePointValue")
                        .HasColumnType("float");

                    b.Property<double?>("Term")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("CurvePointss");
                });

            modelBuilder.Entity("P7CreateRestApi.Domain.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FitchRating")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MoodysRating")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte?>("OrderNumber")
                        .HasColumnType("tinyint");

                    b.Property<string>("SandPRating")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("P7CreateRestApi.Domain.RuleName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Json")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SqlPart")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SqlStr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Template")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RuleNames");
                });

            modelBuilder.Entity("P7CreateRestApi.Domain.Trade", b =>
                {
                    b.Property<int>("TradeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TradeId"), 1L, 1);

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AccountType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Benchmark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Book")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("BuyPrice")
                        .HasColumnType("float");

                    b.Property<double?>("BuyQuantity")
                        .HasColumnType("float");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreationName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DealName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DealType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RevisionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RevisionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("SellPrice")
                        .HasColumnType("float");

                    b.Property<double?>("SellQuantity")
                        .HasColumnType("float");

                    b.Property<string>("Side")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SourceListId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("TradeDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TradeSecurity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TradeStatus")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Trader")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("TradeId");

                    b.ToTable("Trades");
                });

            modelBuilder.Entity("P7CreateRestApi.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Fullname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}
