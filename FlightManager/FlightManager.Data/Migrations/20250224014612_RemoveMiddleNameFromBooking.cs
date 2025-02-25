using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMiddleNameFromBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "HW5Eo6YcZVS8Izly0MlTQICkkj+W8dnE6oCd4bQgvVQrYwHDjJFCfVspDuz9yze+");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "Ug5bfj885K4Q9ESl7zDBo5jhCKObxbbN3/iFE+sZFZFy6dXvei3MWyabzlQZko7u");
        }
    }
}
