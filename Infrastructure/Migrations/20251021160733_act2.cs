using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class act2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PaymentTypes_PaymentTypePaymentId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PaymentTypePaymentId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentTypePaymentId",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PaymentTypes_PaymentId",
                table: "Orders",
                column: "PaymentId",
                principalTable: "PaymentTypes",
                principalColumn: "PaymentId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PaymentTypes_PaymentId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "PaymentTypePaymentId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentTypePaymentId",
                table: "Orders",
                column: "PaymentTypePaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PaymentTypes_PaymentTypePaymentId",
                table: "Orders",
                column: "PaymentTypePaymentId",
                principalTable: "PaymentTypes",
                principalColumn: "PaymentId");
        }
    }
}
