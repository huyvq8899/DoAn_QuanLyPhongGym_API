using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DLL.Migrations
{
    public partial class add_ThongBao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ThongBaos",
                columns: table => new
                {
                    ThongBaoId = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<bool>(nullable: true),
                    NguoiGuiId = table.Column<string>(nullable: true),
                    NguoiNhanId = table.Column<string>(nullable: true),
                    TieuDe = table.Column<string>(nullable: true),
                    NoiDung = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    LoaiThongBao = table.Column<int>(nullable: false),
                    ObjectId = table.Column<string>(nullable: true),
                    IsRead = table.Column<bool>(nullable: true),
                    IsOpend = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongBaos", x => x.ThongBaoId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThongBaos");
        }
    }
}
