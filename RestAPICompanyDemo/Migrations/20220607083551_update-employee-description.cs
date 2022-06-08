using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestAPICompanyDemo.Migrations
{
    public partial class updateemployeedescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "employees",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "employees");
        }
    }
}
