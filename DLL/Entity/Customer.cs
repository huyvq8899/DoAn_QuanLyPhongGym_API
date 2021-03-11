using DLL.LogEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Entity
{
    public class Customer : AuditableEntity
    {
        public string Id { get; set; }
        public string TenKhachHang { get; set; }
        public string DiaChi { get; set; }
        public string NganhNghe { get; set; }
        public decimal SanLuongHangThang { get; set; }
        public string PhuongAnNhapHang { get; set; }
        public string NhaCungCapHienTai { get; set; }
        public string GiaTrietKhau { get; set; }
        public string MongMuonKhachHang { get; set; }
        public string CacVanDeCuaNhaCCCu { get; set; }
        public string NguoiLienHe { get; set; }
        public string ChucVu { get; set; }
        public string SoDienThoai { get; set; }
        public string LuuY { get; set; }
        public string DanhGiaChung { get; set; }
        public string MaKhachHang { get; set; }
        public string MaSoThue { get; set; }
        public string Email { get; set; }
        public string GiamDoc { get; set; }
        public bool? TiemNang { get; set; }
        public string LoaiKhachHang { get; set; }
        public string TrangThaiKhachHang { get; set; }
        public string TenVung { get; set; }
        public string MaVung { get; set; }
        public string TenNganhNghe { get; set; }
        public string MaNganhNghe { get; set; }
        public string VungId { get; set; }
        public string NganhNgheId { get; set; }
        public string NguoiDaiDienPhapLuat { get; set; }
        public string SoDienThoaiNguoiDaiDien { get; set; }
        public string KeToan { get; set; }
        public string SoDienThoaiKeToan { get; set; }
        public string CongNo { get; set; }
        public string CheckCIC { get; set; }
        public string BaoLanhThanhToan { get; set; }

        public string DiaChiGiaoHang { get; set; }
        public string VanPhongGiaoDich { get; set; }
        public string EmailNhanHoaDon { get; set; }
        public string HanMuc { get; set;}
        public string ThoiHanNo { get; set; }
        public string DeXuatNhanVien { get; set; }
    }
}
