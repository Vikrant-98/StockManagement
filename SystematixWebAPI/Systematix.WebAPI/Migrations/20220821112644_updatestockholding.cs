using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Systematix.WebAPI.Migrations
{
    public partial class updatestockholding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BranchCode",
                table: "tbl_ClientHoldings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranchCode",
                table: "tbl_ClientHoldings");
        }
    }
}
