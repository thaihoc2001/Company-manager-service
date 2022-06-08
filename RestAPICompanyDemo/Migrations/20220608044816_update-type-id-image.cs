using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestAPICompanyDemo.Migrations
{
    public partial class updatetypeidimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_employees_Imageid",
                table: "Image");

            migrationBuilder.AlterColumn<string>(
                name: "Imageid",
                table: "Image",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_employees_ImageId",
                table: "employees",
                column: "ImageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_employees_Image_ImageId",
                table: "employees",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Imageid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_Image_ImageId",
                table: "employees");

            migrationBuilder.DropIndex(
                name: "IX_employees_ImageId",
                table: "employees");

            migrationBuilder.AlterColumn<int>(
                name: "Imageid",
                table: "Image",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_employees_Imageid",
                table: "Image",
                column: "Imageid",
                principalTable: "employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
