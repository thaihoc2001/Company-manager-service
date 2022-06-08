using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestAPICompanyDemo.Migrations
{
    public partial class addimagemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageAvatar",
                table: "employees",
                newName: "ImageId");

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Imageid = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Imageid);
                    table.ForeignKey(
                        name: "FK_Image_employees_Imageid",
                        column: x => x.Imageid,
                        principalTable: "employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "employees",
                newName: "ImageAvatar");
        }
    }
}
