using Microsoft.EntityFrameworkCore.Migrations;

namespace Readooks.DataAccessLayer.Migrations
{
    public partial class AddedNumberOfReadPagesColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfReadPages",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfReadPages",
                table: "Books");
        }
    }
}
