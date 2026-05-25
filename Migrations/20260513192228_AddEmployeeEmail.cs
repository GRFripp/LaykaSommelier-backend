using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaykaSommelier.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "employee_email",
                table: "employees",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "employee_email",
                table: "employees");
        }
    }
}
