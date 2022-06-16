using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class EditSomeColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductDescription",
                table: "Products",
                newName: "ProductQuantity");

            migrationBuilder.RenameColumn(
                name: "ProductDepartment",
                table: "Products",
                newName: "ProductName");

            migrationBuilder.AddColumn<string>(
                name: "ProductPrice",
                table: "Products",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductPrice",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductQuantity",
                table: "Products",
                newName: "ProductDescription");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Products",
                newName: "ProductDepartment");
        }
    }
}
