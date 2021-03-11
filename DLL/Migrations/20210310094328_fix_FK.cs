using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DLL.Migrations
{
    public partial class fix_FK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "DoiTuongs");

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "Cards",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<bool>(nullable: true),
                    TenKhachHang = table.Column<string>(nullable: true),
                    DiaChi = table.Column<string>(nullable: true),
                    NganhNghe = table.Column<string>(nullable: true),
                    SanLuongHangThang = table.Column<decimal>(nullable: false),
                    PhuongAnNhapHang = table.Column<string>(nullable: true),
                    NhaCungCapHienTai = table.Column<string>(nullable: true),
                    GiaTrietKhau = table.Column<string>(nullable: true),
                    MongMuonKhachHang = table.Column<string>(nullable: true),
                    CacVanDeCuaNhaCCCu = table.Column<string>(nullable: true),
                    NguoiLienHe = table.Column<string>(nullable: true),
                    ChucVu = table.Column<string>(nullable: true),
                    SoDienThoai = table.Column<string>(nullable: true),
                    LuuY = table.Column<string>(nullable: true),
                    DanhGiaChung = table.Column<string>(nullable: true),
                    MaKhachHang = table.Column<string>(nullable: true),
                    MaSoThue = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    GiamDoc = table.Column<string>(nullable: true),
                    TiemNang = table.Column<bool>(nullable: true),
                    LoaiKhachHang = table.Column<string>(nullable: true),
                    TrangThaiKhachHang = table.Column<string>(nullable: true),
                    TenVung = table.Column<string>(nullable: true),
                    MaVung = table.Column<string>(nullable: true),
                    TenNganhNghe = table.Column<string>(nullable: true),
                    MaNganhNghe = table.Column<string>(nullable: true),
                    VungId = table.Column<string>(nullable: true),
                    NganhNgheId = table.Column<string>(nullable: true),
                    NguoiDaiDienPhapLuat = table.Column<string>(nullable: true),
                    SoDienThoaiNguoiDaiDien = table.Column<string>(nullable: true),
                    KeToan = table.Column<string>(nullable: true),
                    SoDienThoaiKeToan = table.Column<string>(nullable: true),
                    CongNo = table.Column<string>(nullable: true),
                    CheckCIC = table.Column<string>(nullable: true),
                    BaoLanhThanhToan = table.Column<string>(nullable: true),
                    DiaChiGiaoHang = table.Column<string>(nullable: true),
                    VanPhongGiaoDich = table.Column<string>(nullable: true),
                    EmailNhanHoaDon = table.Column<string>(nullable: true),
                    HanMuc = table.Column<string>(nullable: true),
                    ThoiHanNo = table.Column<string>(nullable: true),
                    DeXuatNhanVien = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Cards");

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    BillCode = table.Column<string>(nullable: true),
                    CardId = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Money = table.Column<decimal>(nullable: false),
                    Status = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DoiTuongs",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    BaoLanhThanhToan = table.Column<string>(nullable: true),
                    CacVanDeCuaNhaCCCu = table.Column<string>(nullable: true),
                    CheckCIC = table.Column<string>(nullable: true),
                    ChucVu = table.Column<string>(nullable: true),
                    CongNo = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    DanhGiaChung = table.Column<string>(nullable: true),
                    DeXuatNhanVien = table.Column<string>(nullable: true),
                    DiaChi = table.Column<string>(nullable: true),
                    DiaChiGiaoHang = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EmailNhanHoaDon = table.Column<string>(nullable: true),
                    GiaTrietKhau = table.Column<string>(nullable: true),
                    GiamDoc = table.Column<string>(nullable: true),
                    HanMuc = table.Column<string>(nullable: true),
                    KeToan = table.Column<string>(nullable: true),
                    LoaiKhachHang = table.Column<string>(nullable: true),
                    LuuY = table.Column<string>(nullable: true),
                    MaKhachHang = table.Column<string>(nullable: true),
                    MaNganhNghe = table.Column<string>(nullable: true),
                    MaSoThue = table.Column<string>(nullable: true),
                    MaVung = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    MongMuonKhachHang = table.Column<string>(nullable: true),
                    NganhNghe = table.Column<string>(nullable: true),
                    NganhNgheId = table.Column<string>(nullable: true),
                    NguoiDaiDienPhapLuat = table.Column<string>(nullable: true),
                    NguoiLienHe = table.Column<string>(nullable: true),
                    NhaCungCapHienTai = table.Column<string>(nullable: true),
                    PhuongAnNhapHang = table.Column<string>(nullable: true),
                    SanLuongHangThang = table.Column<decimal>(nullable: false),
                    SoDienThoai = table.Column<string>(nullable: true),
                    SoDienThoaiKeToan = table.Column<string>(nullable: true),
                    SoDienThoaiNguoiDaiDien = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: true),
                    TenKhachHang = table.Column<string>(nullable: true),
                    TenNganhNghe = table.Column<string>(nullable: true),
                    TenVung = table.Column<string>(nullable: true),
                    ThoiHanNo = table.Column<string>(nullable: true),
                    TiemNang = table.Column<bool>(nullable: true),
                    TrangThaiKhachHang = table.Column<string>(nullable: true),
                    VanPhongGiaoDich = table.Column<string>(nullable: true),
                    VungId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoiTuongs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_CardId",
                table: "Bills",
                column: "CardId");
        }
    }
}
