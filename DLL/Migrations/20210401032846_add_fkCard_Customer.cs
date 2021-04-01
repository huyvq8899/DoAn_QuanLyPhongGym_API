using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DLL.Migrations
{
    public partial class add_fkCard_Customer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "Cards");

            migrationBuilder.CreateTable(
                name: "KhachHangLogs",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<bool>(nullable: true),
                    CustomerCode = table.Column<string>(nullable: true),
                    TenTruongThayDoi = table.Column<string>(nullable: true),
                    DienGiai = table.Column<string>(nullable: true),
                    DuLieuCu = table.Column<string>(nullable: true),
                    DuLieuMoi = table.Column<string>(nullable: true),
                    CustomerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHangLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KhachHangLogs_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KhachHangLogs_CustomerId",
                table: "KhachHangLogs",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KhachHangLogs");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Cards",
                nullable: true);
        }
    }
}
