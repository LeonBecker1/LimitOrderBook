using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LimitOrderBook.Infrastructure.Migrations
{
    public partial class AddPortfolios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Stocks_Underlying_Id",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_Issuer_Id",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "OrderModel");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_Underlying_Id",
                table: "OrderModel",
                newName: "IX_OrderModel_Underlying_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_Issuer_Id",
                table: "OrderModel",
                newName: "IX_OrderModel_Issuer_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderModel",
                table: "OrderModel",
                column: "Order_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderModel_Stocks_Underlying_Id",
                table: "OrderModel",
                column: "Underlying_Id",
                principalTable: "Stocks",
                principalColumn: "Stock_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderModel_Users_Issuer_Id",
                table: "OrderModel",
                column: "Issuer_Id",
                principalTable: "Users",
                principalColumn: "User_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderModel_Stocks_Underlying_Id",
                table: "OrderModel");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderModel_Users_Issuer_Id",
                table: "OrderModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderModel",
                table: "OrderModel");

            migrationBuilder.RenameTable(
                name: "OrderModel",
                newName: "Orders");

            migrationBuilder.RenameIndex(
                name: "IX_OrderModel_Underlying_Id",
                table: "Orders",
                newName: "IX_Orders_Underlying_Id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderModel_Issuer_Id",
                table: "Orders",
                newName: "IX_Orders_Issuer_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Order_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Stocks_Underlying_Id",
                table: "Orders",
                column: "Underlying_Id",
                principalTable: "Stocks",
                principalColumn: "Stock_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_Issuer_Id",
                table: "Orders",
                column: "Issuer_Id",
                principalTable: "Users",
                principalColumn: "User_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
