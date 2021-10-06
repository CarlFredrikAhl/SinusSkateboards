using Microsoft.EntityFrameworkCore.Migrations;

namespace SinusSkateboards.Migrations
{
    public partial class removedOrderIdFromCheckout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Checkouts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Checkouts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
