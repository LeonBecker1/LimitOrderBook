using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LimitOrderBook.Infrastructure.Migrations
{
    public partial class ChangePositionModel1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Portfolios_Position_Id1",
                table: "Positions");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Portfolios_Portfolio_Id",
                table: "Positions",
                column: "Portfolio_Id",
                principalTable: "Portfolios",
                principalColumn: "Portfolio_Id",
                onDelete: ReferentialAction.Cascade
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Portfolios_Portfolio_Id",
                table: "Positions");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Portfolios_Portfolio_Id",
                table: "Positions",
                column: "Portfolio_Id",
                principalTable: "Portfolios",
                principalColumn: "Portfolio_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
