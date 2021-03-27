using Microsoft.EntityFrameworkCore.Migrations;

namespace DLL.Migrations
{
    public partial class fix_table_Facility : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Facilities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EstablishedDay",
                table: "Facilities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fax",
                table: "Facilities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumberPhone",
                table: "Facilities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxCode",
                table: "Facilities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "EstablishedDay",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "Fax",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "NumberPhone",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "TaxCode",
                table: "Facilities");
        }
    }
}
