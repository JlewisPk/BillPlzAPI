using Microsoft.EntityFrameworkCore.Migrations;

namespace BillPlzAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemObject",
                columns: table => new
                {
                    itemId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    itemName = table.Column<string>(nullable: true),
                    itemPrice = table.Column<int>(nullable: false),
                    itemCount = table.Column<int>(nullable: false),
                    itemURL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemObject", x => x.itemId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemObject");
        }
    }
}
