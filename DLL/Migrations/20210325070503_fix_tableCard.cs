using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DLL.Migrations
{
    public partial class fix_tableCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaoLanhThanhToan",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CacVanDeCuaNhaCCCu",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CheckCIC",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ChucVu",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CongNo",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DanhGiaChung",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DeXuatNhanVien",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DiaChi",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DiaChiGiaoHang",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "EmailNhanHoaDon",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "GiaTrietKhau",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "GiamDoc",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "HanMuc",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "KeToan",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "LoaiKhachHang",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "LuuY",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "MaKhachHang",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "MaNganhNghe",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "MaSoThue",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "MaVung",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "MongMuonKhachHang",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "NganhNghe",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "NganhNgheId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "NguoiDaiDienPhapLuat",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "NguoiLienHe",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "NhaCungCapHienTai",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PhuongAnNhapHang",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "SanLuongHangThang",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "SoDienThoai",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "SoDienThoaiKeToan",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "SoDienThoaiNguoiDaiDien",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "TiemNang",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "NumOfDay",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Cards");

            migrationBuilder.RenameColumn(
                name: "VungId",
                table: "Customers",
                newName: "NumberPhone");

            migrationBuilder.RenameColumn(
                name: "VanPhongGiaoDich",
                table: "Customers",
                newName: "Note");

            migrationBuilder.RenameColumn(
                name: "TrangThaiKhachHang",
                table: "Customers",
                newName: "Job");

            migrationBuilder.RenameColumn(
                name: "ThoiHanNo",
                table: "Customers",
                newName: "HealthStatus");

            migrationBuilder.RenameColumn(
                name: "TenVung",
                table: "Customers",
                newName: "CustomerName");

            migrationBuilder.RenameColumn(
                name: "TenNganhNghe",
                table: "Customers",
                newName: "CustomerCode");

            migrationBuilder.RenameColumn(
                name: "TenKhachHang",
                table: "Customers",
                newName: "Address");

            migrationBuilder.AddColumn<DateTime>(
                name: "DoB",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FromDate",
                table: "Customers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Height",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ToDate",
                table: "Customers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FromDate",
                table: "Cards",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ToDate",
                table: "Cards",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoB",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "FromDate",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ToDate",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "FromDate",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "ToDate",
                table: "Cards");

            migrationBuilder.RenameColumn(
                name: "NumberPhone",
                table: "Customers",
                newName: "VungId");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "Customers",
                newName: "VanPhongGiaoDich");

            migrationBuilder.RenameColumn(
                name: "Job",
                table: "Customers",
                newName: "TrangThaiKhachHang");

            migrationBuilder.RenameColumn(
                name: "HealthStatus",
                table: "Customers",
                newName: "ThoiHanNo");

            migrationBuilder.RenameColumn(
                name: "CustomerName",
                table: "Customers",
                newName: "TenVung");

            migrationBuilder.RenameColumn(
                name: "CustomerCode",
                table: "Customers",
                newName: "TenNganhNghe");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Customers",
                newName: "TenKhachHang");

            migrationBuilder.AddColumn<string>(
                name: "BaoLanhThanhToan",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CacVanDeCuaNhaCCCu",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CheckCIC",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChucVu",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CongNo",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DanhGiaChung",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeXuatNhanVien",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiaChi",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiaChiGiaoHang",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailNhanHoaDon",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GiaTrietKhau",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GiamDoc",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HanMuc",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KeToan",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LoaiKhachHang",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LuuY",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaKhachHang",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaNganhNghe",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaSoThue",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaVung",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MongMuonKhachHang",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NganhNghe",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NganhNgheId",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiDaiDienPhapLuat",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiLienHe",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NhaCungCapHienTai",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhuongAnNhapHang",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SanLuongHangThang",
                table: "Customers",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "SoDienThoai",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoDienThoaiKeToan",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoDienThoaiNguoiDaiDien",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TiemNang",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumOfDay",
                table: "Cards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Cards",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
