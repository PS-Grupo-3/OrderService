using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class canceledQuit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderStatuses",
                keyColumn: "OrderStatusId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PaymentStatuses",
                keyColumn: "PaymentStatusId",
                keyValue: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "OrderStatusId", "StatusName" },
                values: new object[] { 3, "Canceled" });

            migrationBuilder.InsertData(
                table: "PaymentStatuses",
                columns: new[] { "PaymentStatusId", "PaymentStatusName" },
                values: new object[] { 3, "Canceled" });
        }
    }
}
