using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestAPICompanyDemo.Migrations
{
    public partial class updatetabledepartmentanduser1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_Departments_DepartmentId",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "FK_users_employees_EmployeeId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Departments");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "users",
                newName: "CurentEmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_users_EmployeeId",
                table: "users",
                newName: "IX_users_CurentEmployeeId");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "employees",
                newName: "CurentDepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_employees_DepartmentId",
                table: "employees",
                newName: "IX_employees_CurentDepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_employees_Departments_CurentDepartmentId",
                table: "employees",
                column: "CurentDepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_employees_CurentEmployeeId",
                table: "users",
                column: "CurentEmployeeId",
                principalTable: "employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_Departments_CurentDepartmentId",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "FK_users_employees_CurentEmployeeId",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "CurentEmployeeId",
                table: "users",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_users_CurentEmployeeId",
                table: "users",
                newName: "IX_users_EmployeeId");

            migrationBuilder.RenameColumn(
                name: "CurentDepartmentId",
                table: "employees",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_employees_CurentDepartmentId",
                table: "employees",
                newName: "IX_employees_DepartmentId");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Departments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_employees_Departments_DepartmentId",
                table: "employees",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_employees_EmployeeId",
                table: "users",
                column: "EmployeeId",
                principalTable: "employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
