using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Systematix.WebAPI.Migrations
{
    public partial class updatetable_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "LedgerBalance",
                table: "tbl_ClientLedger",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LedgerBalance",
                table: "tbl_ClientLedger",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
