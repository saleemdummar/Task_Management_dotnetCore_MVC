using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Task_Management.Migrations
{
    /// <inheritdoc />
    public partial class addedSomeValuesinTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "tasks",
                columns: new[] { "TaskID", "Column", "Description", "Position", "Status", "Title" },
                values: new object[,]
                {
                    { 1, "In Progress", "Finish all outstanding tasks", 1, "In Progress", "Review code" },
                    { 2, "Completed", "Check code quality and address any issues", 2, "Completed", "Test functionality" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "TaskID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "TaskID",
                keyValue: 2);
        }
    }
}
