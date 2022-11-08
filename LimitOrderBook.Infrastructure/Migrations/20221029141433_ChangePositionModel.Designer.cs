﻿// <auto-generated />
using System;
using LimitOrderBook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LimitOrderBook.Infrastructure.Migrations
{
    [DbContext(typeof(LimitOrderBookDbContext))]
    [Migration("20221029141433_ChangePositionModel")]
    partial class ChangePositionModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("LimitOrderBook.Infrastructure.Persistence.Models.OrderModel", b =>
                {
                    b.Property<int>("orderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Order_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("orderId"), 1L, 1);

                    b.Property<bool>("IsBuyOrder")
                        .HasColumnType("BIT")
                        .HasColumnName("Is_Buy_Order");

                    b.Property<int>("Issuer_Id")
                        .HasColumnType("int");

                    b.Property<int>("Underlying_Id")
                        .HasColumnType("int");

                    b.Property<int>("price")
                        .HasColumnType("int")
                        .HasColumnName("Price");

                    b.Property<long>("quantity")
                        .HasColumnType("bigint")
                        .HasColumnName("Quantity");

                    b.HasKey("orderId");

                    b.HasIndex("Issuer_Id");

                    b.HasIndex("Underlying_Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("LimitOrderBook.Infrastructure.Persistence.Models.PortfolioModel", b =>
                {
                    b.Property<int>("portfolioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Portfolio_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("portfolioId"), 1L, 1);

                    b.HasKey("portfolioId");

                    b.ToTable("Portfolios");
                });

            modelBuilder.Entity("LimitOrderBook.Infrastructure.Persistence.Models.PositionModel", b =>
                {
                    b.Property<int>("positionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Position_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("positionId"), 1L, 1);

                    b.Property<int?>("Portfolio_Id")
                        .HasColumnType("int");

                    b.Property<int>("Underlying_Id")
                        .HasColumnType("int");

                    b.Property<long>("quantity")
                        .HasColumnType("bigint")
                        .HasColumnName("Quantity");

                    b.HasKey("positionId");

                    b.HasIndex("Portfolio_Id");

                    b.HasIndex("Underlying_Id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("LimitOrderBook.Infrastructure.Persistence.Models.SaleModel", b =>
                {
                    b.Property<int>("saleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Sale_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("saleId"), 1L, 1);

                    b.Property<int>("Buyer_Id")
                        .HasColumnType("int");

                    b.Property<int>("Seller_Id")
                        .HasColumnType("int");

                    b.Property<int>("Underlying_Id")
                        .HasColumnType("int");

                    b.HasKey("saleId");

                    b.HasIndex("Buyer_Id");

                    b.HasIndex("Seller_Id");

                    b.HasIndex("Underlying_Id");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("LimitOrderBook.Infrastructure.Persistence.Models.StockModel", b =>
                {
                    b.Property<int>("stockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Stock_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("stockId"), 1L, 1);

                    b.Property<string>("abbreviation")
                        .IsRequired()
                        .HasColumnType("Varchar(8)")
                        .HasColumnName("Abbreviation");

                    b.HasKey("stockId");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("LimitOrderBook.Infrastructure.Persistence.Models.UserModel", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("User_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("userId"), 1L, 1);

                    b.Property<int>("Portfolio_Id")
                        .HasColumnType("int");

                    b.Property<decimal>("balance")
                        .HasColumnType("Decimal(6,2)")
                        .HasColumnName("Balance");

                    b.Property<byte[]>("password")
                        .IsRequired()
                        .HasColumnType("Binary(64)")
                        .HasColumnName("Password");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("User_Name");

                    b.HasKey("userId");

                    b.HasIndex("Portfolio_Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LimitOrderBook.Infrastructure.Persistence.Models.OrderModel", b =>
                {
                    b.HasOne("LimitOrderBook.Infrastructure.Persistence.Models.UserModel", "issuer")
                        .WithMany()
                        .HasForeignKey("Issuer_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LimitOrderBook.Infrastructure.Persistence.Models.StockModel", "underlying")
                        .WithMany()
                        .HasForeignKey("Underlying_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("issuer");

                    b.Navigation("underlying");
                });

            modelBuilder.Entity("LimitOrderBook.Infrastructure.Persistence.Models.PositionModel", b =>
                {
                    b.HasOne("LimitOrderBook.Infrastructure.Persistence.Models.PortfolioModel", null)
                        .WithMany("positions")
                        .HasForeignKey("Portfolio_Id");

                    b.HasOne("LimitOrderBook.Infrastructure.Persistence.Models.StockModel", "underlying")
                        .WithMany()
                        .HasForeignKey("Underlying_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("underlying");
                });

            modelBuilder.Entity("LimitOrderBook.Infrastructure.Persistence.Models.SaleModel", b =>
                {
                    b.HasOne("LimitOrderBook.Infrastructure.Persistence.Models.UserModel", "buyer")
                        .WithMany()
                        .HasForeignKey("Buyer_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LimitOrderBook.Infrastructure.Persistence.Models.UserModel", "seller")
                        .WithMany()
                        .HasForeignKey("Seller_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LimitOrderBook.Infrastructure.Persistence.Models.StockModel", "underlying")
                        .WithMany()
                        .HasForeignKey("Underlying_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("buyer");

                    b.Navigation("seller");

                    b.Navigation("underlying");
                });

            modelBuilder.Entity("LimitOrderBook.Infrastructure.Persistence.Models.UserModel", b =>
                {
                    b.HasOne("LimitOrderBook.Infrastructure.Persistence.Models.PortfolioModel", "portfolio")
                        .WithMany()
                        .HasForeignKey("Portfolio_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("portfolio");
                });

            modelBuilder.Entity("LimitOrderBook.Infrastructure.Persistence.Models.PortfolioModel", b =>
                {
                    b.Navigation("positions");
                });
#pragma warning restore 612, 618
        }
    }
}
