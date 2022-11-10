using Microsoft.EntityFrameworkCore.Migrations;

namespace BookBob_Bootstrap.Migrations
{
    public partial class cartitem1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CartItemId",
                table: "CartItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CartItemId",
                table: "CartItems");
        }
    }
}
