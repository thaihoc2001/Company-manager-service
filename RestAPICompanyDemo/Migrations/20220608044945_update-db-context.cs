using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestAPICompanyDemo.Migrations
{
    public partial class updatedbcontext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_Image_ImageId",
                table: "employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Image",
                table: "Image");

            migrationBuilder.RenameTable(
                name: "Image",
                newName: "Images");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Imageid");

            migrationBuilder.AddForeignKey(
                name: "FK_employees_Images_ImageId",
                table: "employees",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Imageid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_Images_ImageId",
                table: "employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "Image");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Image",
                table: "Image",
                column: "Imageid");

            migrationBuilder.AddForeignKey(
                name: "FK_employees_Image_ImageId",
                table: "employees",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Imageid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
