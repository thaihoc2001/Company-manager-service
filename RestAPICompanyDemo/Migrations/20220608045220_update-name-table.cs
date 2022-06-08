using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestAPICompanyDemo.Migrations
{
    public partial class updatenametable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_Departments_CurentDepartmentId",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "FK_employees_Images_ImageId",
                table: "employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                table: "Departments");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "images");

            migrationBuilder.RenameTable(
                name: "Departments",
                newName: "departments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_images",
                table: "images",
                column: "Imageid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_departments",
                table: "departments",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_employees_departments_CurentDepartmentId",
                table: "employees",
                column: "CurentDepartmentId",
                principalTable: "departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_employees_images_ImageId",
                table: "employees",
                column: "ImageId",
                principalTable: "images",
                principalColumn: "Imageid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_departments_CurentDepartmentId",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "FK_employees_images_ImageId",
                table: "employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_images",
                table: "images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_departments",
                table: "departments");

            migrationBuilder.RenameTable(
                name: "images",
                newName: "Images");

            migrationBuilder.RenameTable(
                name: "departments",
                newName: "Departments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Imageid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_employees_Departments_CurentDepartmentId",
                table: "employees",
                column: "CurentDepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_employees_Images_ImageId",
                table: "employees",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Imageid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
