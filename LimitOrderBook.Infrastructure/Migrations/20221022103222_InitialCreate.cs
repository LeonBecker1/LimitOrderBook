using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LimitOrderBook.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Portfolios",
                columns: table => new
                {
                    Portfolio_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolios", x => x.Portfolio_Id);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Stock_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abbreviation = table.Column<string>(type: "Varchar(8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Stock_Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    User_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<byte[]>(type: "Binary(64)", nullable: false),
                    Balance = table.Column<decimal>(type: "Decimal(6,2)", nullable: false),
                    Portfolio_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.User_Id);
                    table.ForeignKey(
                        name: "FK_Users_Portfolios_Portfolio_Id",
                        column: x => x.Portfolio_Id,
                        principalTable: "Portfolios",
                        principalColumn: "Portfolio_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Position_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Underlying_Id = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<long>(type: "bigint", nullable: false),
                    Position_Id1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Position_Id);
                    table.ForeignKey(
                        name: "FK_Positions_Portfolios_Position_Id1",
                        column: x => x.Position_Id1,
                        principalTable: "Portfolios",
                        principalColumn: "Portfolio_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Positions_Stocks_Underlying_Id",
                        column: x => x.Underlying_Id,
                        principalTable: "Stocks",
                        principalColumn: "Stock_Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Order_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<long>(type: "bigint", nullable: false),
                    Underlying_Id = table.Column<int>(type: "int", nullable: false),
                    Issuer_Id = table.Column<int>(type: "int", nullable: false),
                    Is_Buy_Order = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Order_Id);
                    table.ForeignKey(
                        name: "FK_Orders_Stocks_Underlying_Id",
                        column: x => x.Underlying_Id,
                        principalTable: "Stocks",
                        principalColumn: "Stock_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_Issuer_Id",
                        column: x => x.Issuer_Id,
                        principalTable: "Users",
                        principalColumn: "User_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Sale_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Underlying_Id = table.Column<int>(type: "int", nullable: false),
                    Buyer_Id = table.Column<int>(type: "int", nullable: false),
                    Seller_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Sale_Id);
                    table.ForeignKey(
                        name: "FK_Sales_Stocks_Underlying_Id",
                        column: x => x.Underlying_Id,
                        principalTable: "Stocks",
                        principalColumn: "Stock_Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Sales_Users_Buyer_Id",
                        column: x => x.Buyer_Id,
                        principalTable: "Users",
                        principalColumn: "User_Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Sales_Users_Seller_Id",
                        column: x => x.Seller_Id,
                        principalTable: "Users",
                        principalColumn: "User_Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Issuer_Id",
                table: "Orders",
                column: "Issuer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Underlying_Id",
                table: "Orders",
                column: "Underlying_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_Position_Id1",
                table: "Positions",
                column: "Position_Id1");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_Underlying_Id",
                table: "Positions",
                column: "Underlying_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_Buyer_Id",
                table: "Sales",
                column: "Buyer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_Seller_Id",
                table: "Sales",
                column: "Seller_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_Underlying_Id",
                table: "Sales",
                column: "Underlying_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Portfolio_Id",
                table: "Users",
                column: "Portfolio_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Portfolios");
        }
    }
}
