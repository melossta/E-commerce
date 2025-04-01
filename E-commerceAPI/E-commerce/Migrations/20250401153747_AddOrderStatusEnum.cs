using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerce.Migrations
{
    public partial class AddOrderStatusEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Step 1: Update existing values in 'Status' to integer equivalents before changing the column type
            migrationBuilder.Sql("UPDATE Orders SET Status = 1 WHERE Status = 'Pending'");
            migrationBuilder.Sql("UPDATE Orders SET Status = 2 WHERE Status = 'Processing'");
            migrationBuilder.Sql("UPDATE Orders SET Status = 3 WHERE Status = 'Shipped'");
            migrationBuilder.Sql("UPDATE Orders SET Status = 4 WHERE Status = 'Delivered'");
            migrationBuilder.Sql("UPDATE Orders SET Status = 5 WHERE Status = 'Canceled'");

            migrationBuilder.Sql("UPDATE Payments SET Status = 1 WHERE Status = 'Pending'");
            migrationBuilder.Sql("UPDATE Payments SET Status = 2 WHERE Status = 'Processing'");
            migrationBuilder.Sql("UPDATE Payments SET Status = 3 WHERE Status = 'Shipped'");
            migrationBuilder.Sql("UPDATE Payments SET Status = 4 WHERE Status = 'Delivered'");
            migrationBuilder.Sql("UPDATE Payments SET Status = 5 WHERE Status = 'Canceled'");

            // Step 2: Alter column type to 'int'
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Payments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Step 1: Revert back the values to string equivalents (if needed)
            migrationBuilder.Sql("UPDATE Orders SET Status = 'Pending' WHERE Status = 1");
            migrationBuilder.Sql("UPDATE Orders SET Status = 'Processing' WHERE Status = 2");
            migrationBuilder.Sql("UPDATE Orders SET Status = 'Shipped' WHERE Status = 3");
            migrationBuilder.Sql("UPDATE Orders SET Status = 'Delivered' WHERE Status = 4");
            migrationBuilder.Sql("UPDATE Orders SET Status = 'Canceled' WHERE Status = 5");

            migrationBuilder.Sql("UPDATE Payments SET Status = 'Pending' WHERE Status = 1");
            migrationBuilder.Sql("UPDATE Payments SET Status = 'Processing' WHERE Status = 2");
            migrationBuilder.Sql("UPDATE Payments SET Status = 'Shipped' WHERE Status = 3");
            migrationBuilder.Sql("UPDATE Payments SET Status = 'Delivered' WHERE Status = 4");
            migrationBuilder.Sql("UPDATE Payments SET Status = 'Canceled' WHERE Status = 5");

            // Step 2: Alter column type back to 'string'
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
