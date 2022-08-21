using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Systematix.WebAPI.Migrations
{
    public partial class updatetable_ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FundAvailable",
                table: "tbl_ClientDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FundAvailable",
                table: "tbl_ClientDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
