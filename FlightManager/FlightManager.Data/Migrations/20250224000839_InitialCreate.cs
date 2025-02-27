using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "WSW604a0qVdeLRPdMoXMjpVd0QlxpLQnqDNCuG2hT8qhwovR+AnmeYAAuOtCzOVb");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "PRvO1o8rfEQPKCUzVAxn3/WtRD1qW2kgioyP+A6gNd7UXDAwok0j8huWyu382n7l");
        }
    }
}
