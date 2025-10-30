using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class transactionAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "transactionId",
                table: "OrderDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_transactionId",
                table: "OrderDetails",
                column: "transactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_transactionId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "transactionId",
                table: "OrderDetails");
        }
    }
}
