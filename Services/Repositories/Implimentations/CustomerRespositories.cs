using AutoMapper;
using AutoMapper.QueryableExtensions;
using DLL;
using DLL.Entity;
using ManagementServices.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using Services.Helper;
using Services.Repositories.Interfaces;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Repositories.Implimentations
{
    public class CustomerRespositories : ICustomerRespositories
    {
        Datacontext db;
        IMapper mp;
        private readonly IHostingEnvironment _hostingEnvironment;
        IHttpContextAccessor _IHttpContextAccessor;
        IConfiguration _IConfiguration;
        public CustomerRespositories(Datacontext datacontext, IMapper mapper, IHostingEnvironment IHostingEnvironment, IHttpContextAccessor IHttpContextAccessor, IConfiguration IConfiguration)
        {
            this.db = datacontext;
            this.mp = mapper;
            _hostingEnvironment = IHostingEnvironment;
            _IHttpContextAccessor = IHttpContextAccessor;
            _IConfiguration = IConfiguration;
        }

        public async Task<int> Insert(int TN, CustomerViewModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            model.TenKhachHang = model.TenKhachHang.ToTrim();
            model.MaKhachHang = model.MaKhachHang.ToTrim();
            model.DiaChi = model.DiaChi.ToTrim();
            model.MaSoThue = model.MaSoThue.ToTrim();
            model.NguoiLienHe = model.NguoiLienHe.ToTrim();
            model.SoDienThoai = model.SoDienThoai.ToTrim();
            model.TiemNang = true;
            model.Status = true;
            if (TN == 1) model.TiemNang = true;
            else model.TiemNang = false;
            var entity = mp.Map<Customer>(model);
            await db.Customers.AddAsync(entity);
            var rs = await db.SaveChangesAsync();
            // thanh cong 1, o loi
            return rs;
        }

        public async Task<int> Delete(Guid Id)
        {
            try
            {
                var entity = await db.Customers.FirstOrDefaultAsync(x => x.Id == Id.ToString());
                db.Customers.Remove(entity);
                var rs = await db.SaveChangesAsync();
                return rs;
            }
            catch (DbUpdateException)
            {
                return -1;
            }
        }

        public async Task<int> Update(int TN, CustomerViewModel model)
        {
            try
            {
                if (TN == 1)
                {
                   /* ur.TenKhachHang = model.TenKhachHang.Trim();
                    ur.DiaChi = model.DiaChi.Trim();
                    ur.MaSoThue = model.MaSoThue;
                    ur.SanLuongHangThang = model.SanLuongHangThang;
                    ur.PhuongAnNhapHang = model.PhuongAnNhapHang;
                    ur.NhaCungCapHienTai = model.NhaCungCapHienTai;
                    ur.GiaTrietKhau = model.GiaTrietKhau;
                    ur.MongMuonKhachHang = model.MongMuonKhachHang;
                    ur.CacVanDeCuaNhaCCCu = model.CacVanDeCuaNhaCCCu;
                    ur.NguoiLienHe = model.NguoiLienHe.Trim();
                    ur.ChucVu = model.ChucVu;
                    ur.LuuY = model.LuuY;
                    ur.DanhGiaChung = model.DanhGiaChung;
                    ur.SoDienThoai = model.SoDienThoai.Trim();
                    ur.LoaiKhachHang = model.LoaiKhachHang;
                    ur.MaKhachHang = model.MaKhachHang.Trim();
                    ur.TrangThaiKhachHang = model.TrangThaiKhachHang.Trim();
                    ur.TenVung = model.TenVung ?? string.Empty;
                    ur.VungId = model.VungId ?? string.Empty;
                    ur.MaVung = model.MaVung ?? string.Empty;
                    ur.MaNganhNghe = model.MaNganhNghe ?? string.Empty;
                    ur.NganhNgheId = model.NganhNgheId ?? string.Empty;
                    ur.TenNganhNghe = model.TenNganhNghe ?? string.Empty;
                    ur.NguoiDaiDienPhapLuat = model.NguoiDaiDienPhapLuat;
                    ur.SoDienThoaiNguoiDaiDien = model.SoDienThoaiNguoiDaiDien;
                    ur.KeToan = model.KeToan;
                    ur.SoDienThoaiKeToan = model.SoDienThoaiKeToan;
                    ur.CheckCIC = model.CheckCIC;
                    ur.CongNo = model.CongNo;
                    ur.BaoLanhThanhToan = model.BaoLanhThanhToan;
                    ur.GiamDoc = model.GiamDoc;
                    ur.DiaChiGiaoHang = model.DiaChiGiaoHang;
                    ur.VanPhongGiaoDich = model.VanPhongGiaoDich;
                    ur.DeXuatNhanVien = model.DeXuatNhanVien;
                    ur.HanMuc = model.HanMuc;
                    ur.ThoiHanNo = model.ThoiHanNo;
*/
                }
                //else
                //{
                //    ur.TenKhachHang = model.TenKhachHang.Trim();
                //    ur.DiaChi = model.DiaChi.Trim();

                //    ur.NguoiLienHe = model.NguoiLienHe;
                //    ur.MaKhachHang = model.MaKhachHang.Trim();
                //    ur.MaSoThue = model.MaSoThue; 
                //    ur.Email = model.Email;
                //    ur.GiamDoc = model.GiamDoc;
                //    ur.TrangThaiKhachHang = model.TrangThaiKhachHang.Trim();
                //    ur.LoaiKhachHang = model.LoaiKhachHang;
                //    ur.TenVung = model.TenVung ?? string.Empty;
                //    ur.VungId = model.VungId ?? string.Empty;
                //    ur.MaVung = model.MaVung ?? string.Empty;
                //    ur.MaNganhNghe = model.MaNganhNghe ?? string.Empty;
                //    ur.NganhNgheId = model.NganhNgheId ?? string.Empty;
                //    ur.TenNganhNghe = model.TenNganhNghe ?? string.Empty;

                //}
               // db.Customers.Update(ur);
                var rs = await db.SaveChangesAsync();
                return rs; // 1 thanh cong, 0 that bai

            }
            catch (Exception e)
            {

                throw;
            }

        }
      /*  public async Task<int> GetLichSuKH(KhachHangLogViewModels model)
        {
            model.Id = Guid.NewGuid().ToString();
            model.TenTruongThayDoi = model.TenTruongThayDoi;
            model.DuLieuCu = model.DuLieuCu;
            model.DuLieuMoi = model.DuLieuMoi;
            model.KhachHangId = model.KhachHangId;
            model.DienGiai = model.DienGiai;
            model.CreatedDate = DateTime.Now;
            var entity = mp.Map<KhachHangLog>(model);
            await db.KhachHangLogs.AddAsync(entity);
            var rs = await db.SaveChangesAsync();
            // thanh cong 1, o loi
            return rs;
        }
        public async Task<List<KhachHangLogViewModels>> GetAllLiSuKH()
        {
            var query = from dt in db.KhachHangLogs
                        join kh in db.Customers on dt.KhachHangId equals kh.Id
                        orderby dt.CreatedDate descending
                        select new KhachHangLogViewModels
                        {
                            Id = dt.Id,
                            MaKhachHang = kh.MaKhachHang,
                            TenTruongThayDoi=dt.TenTruongThayDoi,
                            DienGiai=dt.DienGiai,
                            DuLieuMoi=dt.DuLieuMoi,
                            DuLieuCu=dt.DuLieuCu,
                            CreatedDate=dt.CreatedDate,
                            KhachHangId=dt.KhachHangId
                        };
            return await query.ToListAsync();
        }*/
        public async Task<List<CustomerViewModel>> GetAllKH()
        {
            var query = from dt in db.Customers

                        orderby dt.ModifiedDate descending
                        select new CustomerViewModel
                        {
                            Id = dt.Id,
                            TenKhachHang = dt.TenKhachHang,
                            DiaChi = dt.DiaChi,

                            NguoiLienHe = dt.NguoiLienHe,
                            MaKhachHang = dt.MaKhachHang,
                            MaSoThue = dt.MaSoThue,
                            SoDienThoai = dt.SoDienThoai,
                            Email = dt.Email,
                            GiamDoc = dt.GiamDoc,
                            TrangThaiKhachHang = dt.TrangThaiKhachHang,
                            LoaiKhachHang = dt.LoaiKhachHang ?? "Không rõ",
                        };
            return await query.ToListAsync();
        }
        public async Task<PagedList<CustomerViewModel>> GetAllPagingAsync(PagingParams pagingParams, string Id, string selectedId)
        {
            IQueryable<CustomerViewModel> query = from dt in db.Customers
                                                  select new CustomerViewModel();
            var tg = new User();
            tg = await db.Users.FindAsync(Id);
            if (tg != null)
            {
                if (tg.RoleId == "BLD" || tg.RoleId == "ADMIN")
                {
                    if (selectedId == null || selectedId == "")
                    {
                        query = from dt in db.Customers
                                where dt.TiemNang == true
                                join us in db.Users on dt.CreatedBy equals us.UserId
                                orderby dt.CreatedDate descending
                                select new CustomerViewModel
                                {
                                    Id = dt.Id,
                                    TenKhachHang = dt.TenKhachHang,
                                    DiaChi = dt.DiaChi,
                                    NguoiLienHe = dt.NguoiLienHe,
                                    MaKhachHang = dt.MaKhachHang,
                                    MaSoThue = dt.MaSoThue,
                                    SoDienThoai = dt.SoDienThoai,
                                    Email = dt.Email,
                                    GiamDoc = dt.GiamDoc,
                                    SanLuongHangThang = dt.SanLuongHangThang,
                                    PhuongAnNhapHang = dt.PhuongAnNhapHang,
                                    NhaCungCapHienTai = dt.NhaCungCapHienTai,
                                    GiaTrietKhau = dt.GiaTrietKhau,
                                    MongMuonKhachHang = dt.MongMuonKhachHang ?? string.Empty,
                                    CacVanDeCuaNhaCCCu = dt.CacVanDeCuaNhaCCCu,
                                    ChucVu = dt.ChucVu,
                                    LuuY = dt.LuuY ?? string.Empty,
                                    DanhGiaChung = dt.DanhGiaChung ?? string.Empty,
                                    LoaiKhachHang = dt.LoaiKhachHang,
                                    TrangThaiKhachHang = dt.TrangThaiKhachHang,
                                    MaNganhNghe = dt.MaNganhNghe,
                                    MaVung = dt.MaVung,
                                    TenVung = dt.TenVung,
                                    TenNganhNghe = dt.TenNganhNghe,
                                    VungId = dt.VungId,
                                    NganhNgheId = dt.NganhNgheId,
                                    NguoiThem = us.FullName,
                                    NguoiDaiDienPhapLuat = dt.NguoiDaiDienPhapLuat,
                                    SoDienThoaiNguoiDaiDien = dt.SoDienThoaiNguoiDaiDien,
                                    KeToan = dt.KeToan,
                                    SoDienThoaiKeToan = dt.SoDienThoaiKeToan,
                                    CheckCIC = dt.CheckCIC,
                                    CongNo = dt.CongNo,
                                    BaoLanhThanhToan = dt.BaoLanhThanhToan,
                                    DiaChiGiaoHang = dt.DiaChiGiaoHang,
                                    VanPhongGiaoDich = dt.VanPhongGiaoDich,
                                    DeXuatNhanVien = dt.DeXuatNhanVien,
                                    HanMuc = dt.HanMuc,
                                    ThoiHanNo = dt.ThoiHanNo,
                                    CreatedDate = dt.CreatedDate,
                                    CreatedBy = dt.CreatedBy,

                                };
                    }
                    else
                    {
                        query = from dt in db.Customers
                                where dt.TiemNang == true
                                where dt.CreatedBy == selectedId
                                join us in db.Users on dt.CreatedBy equals us.UserId
                                orderby dt.CreatedDate descending
                                select new CustomerViewModel
                                {
                                    Id = dt.Id,
                                    TenKhachHang = dt.TenKhachHang,
                                    DiaChi = dt.DiaChi,
                                    NguoiLienHe = dt.NguoiLienHe,
                                    MaKhachHang = dt.MaKhachHang,
                                    MaSoThue = dt.MaSoThue,
                                    SoDienThoai = dt.SoDienThoai,
                                    Email = dt.Email,
                                    GiamDoc = dt.GiamDoc,
                                    SanLuongHangThang = dt.SanLuongHangThang,
                                    PhuongAnNhapHang = dt.PhuongAnNhapHang,
                                    NhaCungCapHienTai = dt.NhaCungCapHienTai,
                                    GiaTrietKhau = dt.GiaTrietKhau,
                                    MongMuonKhachHang = dt.MongMuonKhachHang ?? string.Empty,
                                    CacVanDeCuaNhaCCCu = dt.CacVanDeCuaNhaCCCu,
                                    ChucVu = dt.ChucVu,
                                    LuuY = dt.LuuY ?? string.Empty,
                                    DanhGiaChung = dt.DanhGiaChung ?? string.Empty,
                                    LoaiKhachHang = dt.LoaiKhachHang,
                                    TrangThaiKhachHang = dt.TrangThaiKhachHang,
                                    MaNganhNghe = dt.MaNganhNghe,
                                    MaVung = dt.MaVung,
                                    TenVung = dt.TenVung,
                                    TenNganhNghe = dt.TenNganhNghe,
                                    VungId = dt.VungId,
                                    NganhNgheId = dt.NganhNgheId,
                                    NguoiThem = us.FullName,
                                    NguoiDaiDienPhapLuat = dt.NguoiDaiDienPhapLuat,
                                    SoDienThoaiNguoiDaiDien = dt.SoDienThoaiNguoiDaiDien,
                                    KeToan = dt.KeToan,
                                    SoDienThoaiKeToan = dt.SoDienThoaiKeToan,
                                    CheckCIC = dt.CheckCIC,
                                    CongNo = dt.CongNo,
                                    BaoLanhThanhToan = dt.BaoLanhThanhToan,
                                    DiaChiGiaoHang = dt.DiaChiGiaoHang,
                                    VanPhongGiaoDich = dt.VanPhongGiaoDich,
                                    DeXuatNhanVien = dt.DeXuatNhanVien,
                                    HanMuc = dt.HanMuc,
                                    ThoiHanNo = dt.ThoiHanNo,
                                    CreatedDate = dt.CreatedDate,
                                    CreatedBy = dt.CreatedBy,
                                };
                    }

                }
                else
                {
                    if (tg.NguoiQuanLy == null || tg.NguoiQuanLy == "" || tg.NguoiQuanLy == string.Empty)
                    {

                        var temp = await db.Users.Where(x => x.NguoiQuanLy == tg.UserId).ToListAsync();
                        if (temp != null)
                        {

                            query = from dt in db.Customers
                                    join us in db.Users on dt.CreatedBy equals us.UserId
                                    where us.NguoiQuanLy == Id || us.UserId == Id
                                    where dt.TiemNang == true
                                    orderby dt.CreatedDate descending
                                    select new CustomerViewModel
                                    {
                                        Id = dt.Id,
                                        TenKhachHang = dt.TenKhachHang,
                                        DiaChi = dt.DiaChi,
                                        NguoiLienHe = dt.NguoiLienHe,
                                        MaKhachHang = dt.MaKhachHang,
                                        MaSoThue = dt.MaSoThue,
                                        SoDienThoai = dt.SoDienThoai,
                                        Email = dt.Email,
                                        GiamDoc = dt.GiamDoc,
                                        SanLuongHangThang = dt.SanLuongHangThang,
                                        PhuongAnNhapHang = dt.PhuongAnNhapHang,
                                        NhaCungCapHienTai = dt.NhaCungCapHienTai,
                                        GiaTrietKhau = dt.GiaTrietKhau,
                                        MongMuonKhachHang = dt.MongMuonKhachHang ?? string.Empty,
                                        CacVanDeCuaNhaCCCu = dt.CacVanDeCuaNhaCCCu,
                                        ChucVu = dt.ChucVu,
                                        LuuY = dt.LuuY ?? string.Empty,
                                        DanhGiaChung = dt.DanhGiaChung ?? string.Empty,
                                        LoaiKhachHang = dt.LoaiKhachHang,
                                        TrangThaiKhachHang = dt.TrangThaiKhachHang,
                                        MaNganhNghe = dt.MaNganhNghe,
                                        MaVung = dt.MaVung,
                                        TenVung = dt.TenVung,
                                        TenNganhNghe = dt.TenNganhNghe,
                                        VungId = dt.VungId,
                                        NganhNgheId = dt.NganhNgheId,
                                        NguoiThem = us.FullName,
                                        NguoiDaiDienPhapLuat = dt.NguoiDaiDienPhapLuat,
                                        SoDienThoaiNguoiDaiDien = dt.SoDienThoaiNguoiDaiDien,
                                        KeToan = dt.KeToan,
                                        SoDienThoaiKeToan = dt.SoDienThoaiKeToan,
                                        CheckCIC = dt.CheckCIC,
                                        CongNo = dt.CongNo,
                                        BaoLanhThanhToan = dt.BaoLanhThanhToan,
                                        DiaChiGiaoHang = dt.DiaChiGiaoHang,
                                        VanPhongGiaoDich = dt.VanPhongGiaoDich,
                                        DeXuatNhanVien = dt.DeXuatNhanVien,
                                        HanMuc = dt.HanMuc,
                                        ThoiHanNo = dt.ThoiHanNo,
                                        CreatedDate = dt.CreatedDate,
                                        CreatedBy = dt.CreatedBy,
                                    };
                        }
                        else
                        {
                            query = from dt in db.Customers
                                    join us in db.Users on dt.CreatedBy equals us.UserId
                                    where dt.CreatedBy == Id
                                    where dt.TiemNang == true
                                    orderby dt.CreatedDate descending
                                    select new CustomerViewModel
                                    {
                                        Id = dt.Id,
                                        TenKhachHang = dt.TenKhachHang,
                                        DiaChi = dt.DiaChi,
                                        NguoiLienHe = dt.NguoiLienHe,
                                        MaKhachHang = dt.MaKhachHang,
                                        MaSoThue = dt.MaSoThue,
                                        SoDienThoai = dt.SoDienThoai,
                                        Email = dt.Email,
                                        GiamDoc = dt.GiamDoc,
                                        SanLuongHangThang = dt.SanLuongHangThang,
                                        PhuongAnNhapHang = dt.PhuongAnNhapHang,
                                        NhaCungCapHienTai = dt.NhaCungCapHienTai,
                                        GiaTrietKhau = dt.GiaTrietKhau,
                                        MongMuonKhachHang = dt.MongMuonKhachHang ?? string.Empty,
                                        CacVanDeCuaNhaCCCu = dt.CacVanDeCuaNhaCCCu,
                                        ChucVu = dt.ChucVu,
                                        LuuY = dt.LuuY ?? string.Empty,
                                        DanhGiaChung = dt.DanhGiaChung ?? string.Empty,
                                        LoaiKhachHang = dt.LoaiKhachHang,
                                        TrangThaiKhachHang = dt.TrangThaiKhachHang,
                                        MaNganhNghe = dt.MaNganhNghe,
                                        MaVung = dt.MaVung,
                                        TenVung = dt.TenVung,
                                        TenNganhNghe = dt.TenNganhNghe,
                                        VungId = dt.VungId,
                                        NganhNgheId = dt.NganhNgheId,
                                        NguoiThem = us.FullName,
                                        NguoiDaiDienPhapLuat = dt.NguoiDaiDienPhapLuat,
                                        SoDienThoaiNguoiDaiDien = dt.SoDienThoaiNguoiDaiDien,
                                        KeToan = dt.KeToan,
                                        SoDienThoaiKeToan = dt.SoDienThoaiKeToan,
                                        CheckCIC = dt.CheckCIC,
                                        CongNo = dt.CongNo,
                                        BaoLanhThanhToan = dt.BaoLanhThanhToan,
                                        DiaChiGiaoHang = dt.DiaChiGiaoHang,
                                        VanPhongGiaoDich = dt.VanPhongGiaoDich,
                                        DeXuatNhanVien = dt.DeXuatNhanVien,
                                        HanMuc = dt.HanMuc,
                                        ThoiHanNo = dt.ThoiHanNo,
                                        CreatedDate = dt.CreatedDate,
                                        CreatedBy = dt.CreatedBy,
                                    };
                        }
                    }
                    else
                    {
                        var test = new User();
                        test = await db.Users.FindAsync(tg.NguoiQuanLy);
                        if (test.RoleId == "BLD" || test.RoleId == "ADMIN")
                        {
                            var temp = await db.Users.Where(x => x.NguoiQuanLy == tg.UserId).ToListAsync();
                            if (temp != null)
                            {
                                query = from dt in db.Customers
                                        join us in db.Users on dt.CreatedBy equals us.UserId
                                        where us.NguoiQuanLy == Id || us.UserId == Id
                                        where dt.TiemNang == true
                                        orderby dt.CreatedDate descending
                                        select new CustomerViewModel
                                        {
                                            Id = dt.Id,
                                            TenKhachHang = dt.TenKhachHang,
                                            DiaChi = dt.DiaChi,
                                            NguoiLienHe = dt.NguoiLienHe,
                                            MaKhachHang = dt.MaKhachHang,
                                            MaSoThue = dt.MaSoThue,
                                            SoDienThoai = dt.SoDienThoai,
                                            Email = dt.Email,
                                            GiamDoc = dt.GiamDoc,
                                            SanLuongHangThang = dt.SanLuongHangThang,
                                            PhuongAnNhapHang = dt.PhuongAnNhapHang,
                                            NhaCungCapHienTai = dt.NhaCungCapHienTai,
                                            GiaTrietKhau = dt.GiaTrietKhau,
                                            MongMuonKhachHang = dt.MongMuonKhachHang ?? string.Empty,
                                            CacVanDeCuaNhaCCCu = dt.CacVanDeCuaNhaCCCu,
                                            ChucVu = dt.ChucVu,
                                            LuuY = dt.LuuY ?? string.Empty,
                                            DanhGiaChung = dt.DanhGiaChung ?? string.Empty,
                                            LoaiKhachHang = dt.LoaiKhachHang,
                                            TrangThaiKhachHang = dt.TrangThaiKhachHang,
                                            MaNganhNghe = dt.MaNganhNghe,
                                            MaVung = dt.MaVung,
                                            TenVung = dt.TenVung,
                                            TenNganhNghe = dt.TenNganhNghe,
                                            VungId = dt.VungId,
                                            NganhNgheId = dt.NganhNgheId,
                                            NguoiThem = us.FullName,
                                            NguoiDaiDienPhapLuat = dt.NguoiDaiDienPhapLuat,
                                            SoDienThoaiNguoiDaiDien = dt.SoDienThoaiNguoiDaiDien,
                                            KeToan = dt.KeToan,
                                            SoDienThoaiKeToan = dt.SoDienThoaiKeToan,
                                            CheckCIC = dt.CheckCIC,
                                            CongNo = dt.CongNo,
                                            BaoLanhThanhToan = dt.BaoLanhThanhToan,
                                            DiaChiGiaoHang = dt.DiaChiGiaoHang,
                                            VanPhongGiaoDich = dt.VanPhongGiaoDich,
                                            DeXuatNhanVien = dt.DeXuatNhanVien,
                                            HanMuc = dt.HanMuc,
                                            ThoiHanNo = dt.ThoiHanNo,
                                            CreatedDate = dt.CreatedDate,
                                            CreatedBy = dt.CreatedBy,
                                        };
                            }
                            else
                            {
                                query = from dt in db.Customers
                                        join us in db.Users on dt.CreatedBy equals us.UserId
                                        where dt.CreatedBy == Id
                                        where dt.TiemNang == true
                                        orderby dt.CreatedDate descending
                                        select new CustomerViewModel
                                        {
                                            Id = dt.Id,
                                            TenKhachHang = dt.TenKhachHang,
                                            DiaChi = dt.DiaChi,
                                            NguoiLienHe = dt.NguoiLienHe,
                                            MaKhachHang = dt.MaKhachHang,
                                            MaSoThue = dt.MaSoThue,
                                            SoDienThoai = dt.SoDienThoai,
                                            Email = dt.Email,
                                            GiamDoc = dt.GiamDoc,
                                            SanLuongHangThang = dt.SanLuongHangThang,
                                            PhuongAnNhapHang = dt.PhuongAnNhapHang,
                                            NhaCungCapHienTai = dt.NhaCungCapHienTai,
                                            GiaTrietKhau = dt.GiaTrietKhau,
                                            MongMuonKhachHang = dt.MongMuonKhachHang ?? string.Empty,
                                            CacVanDeCuaNhaCCCu = dt.CacVanDeCuaNhaCCCu,
                                            ChucVu = dt.ChucVu,
                                            LuuY = dt.LuuY ?? string.Empty,
                                            DanhGiaChung = dt.DanhGiaChung ?? string.Empty,
                                            LoaiKhachHang = dt.LoaiKhachHang,
                                            TrangThaiKhachHang = dt.TrangThaiKhachHang,
                                            MaNganhNghe = dt.MaNganhNghe,
                                            MaVung = dt.MaVung,
                                            TenVung = dt.TenVung,
                                            TenNganhNghe = dt.TenNganhNghe,
                                            VungId = dt.VungId,
                                            NganhNgheId = dt.NganhNgheId,
                                            NguoiThem = us.FullName,
                                            NguoiDaiDienPhapLuat = dt.NguoiDaiDienPhapLuat,
                                            SoDienThoaiNguoiDaiDien = dt.SoDienThoaiNguoiDaiDien,
                                            KeToan = dt.KeToan,
                                            SoDienThoaiKeToan = dt.SoDienThoaiKeToan,
                                            CheckCIC = dt.CheckCIC,
                                            CongNo = dt.CongNo,
                                            BaoLanhThanhToan = dt.BaoLanhThanhToan,
                                            DiaChiGiaoHang = dt.DiaChiGiaoHang,
                                            VanPhongGiaoDich = dt.VanPhongGiaoDich,
                                            DeXuatNhanVien = dt.DeXuatNhanVien,
                                            HanMuc = dt.HanMuc,
                                            ThoiHanNo = dt.ThoiHanNo,
                                            CreatedDate = dt.CreatedDate,
                                            CreatedBy = dt.CreatedBy,
                                        };
                            }
                        }
                        else
                        {
                            query = from dt in db.Customers
                                    join us in db.Users on dt.CreatedBy equals us.UserId
                                    where dt.CreatedBy == Id
                                    where dt.TiemNang == true
                                    orderby dt.CreatedDate descending
                                    select new CustomerViewModel
                                    {
                                        Id = dt.Id,
                                        TenKhachHang = dt.TenKhachHang,
                                        DiaChi = dt.DiaChi,
                                        NguoiLienHe = dt.NguoiLienHe,
                                        MaKhachHang = dt.MaKhachHang,
                                        MaSoThue = dt.MaSoThue,
                                        SoDienThoai = dt.SoDienThoai,
                                        Email = dt.Email,
                                        GiamDoc = dt.GiamDoc,
                                        SanLuongHangThang = dt.SanLuongHangThang,
                                        PhuongAnNhapHang = dt.PhuongAnNhapHang,
                                        NhaCungCapHienTai = dt.NhaCungCapHienTai,
                                        GiaTrietKhau = dt.GiaTrietKhau,
                                        MongMuonKhachHang = dt.MongMuonKhachHang ?? string.Empty,
                                        CacVanDeCuaNhaCCCu = dt.CacVanDeCuaNhaCCCu,
                                        ChucVu = dt.ChucVu,
                                        LuuY = dt.LuuY ?? string.Empty,
                                        DanhGiaChung = dt.DanhGiaChung ?? string.Empty,
                                        LoaiKhachHang = dt.LoaiKhachHang,
                                        TrangThaiKhachHang = dt.TrangThaiKhachHang,
                                        MaNganhNghe = dt.MaNganhNghe,
                                        MaVung = dt.MaVung,
                                        TenVung = dt.TenVung,
                                        TenNganhNghe = dt.TenNganhNghe,
                                        VungId = dt.VungId,
                                        NganhNgheId = dt.NganhNgheId,
                                        NguoiThem = us.FullName,
                                        NguoiDaiDienPhapLuat = dt.NguoiDaiDienPhapLuat,
                                        SoDienThoaiNguoiDaiDien = dt.SoDienThoaiNguoiDaiDien,
                                        KeToan = dt.KeToan,
                                        SoDienThoaiKeToan = dt.SoDienThoaiKeToan,
                                        CheckCIC = dt.CheckCIC,
                                        CongNo = dt.CongNo,
                                        BaoLanhThanhToan = dt.BaoLanhThanhToan,
                                        DiaChiGiaoHang = dt.DiaChiGiaoHang,
                                        VanPhongGiaoDich = dt.VanPhongGiaoDich,
                                        DeXuatNhanVien = dt.DeXuatNhanVien,
                                        HanMuc = dt.HanMuc,
                                        ThoiHanNo = dt.ThoiHanNo,
                                        CreatedDate = dt.CreatedDate,
                                        CreatedBy = dt.CreatedBy,
                                    };
                        }

                    }
                }
                if (!string.IsNullOrEmpty(pagingParams.fromDate) && !string.IsNullOrEmpty(pagingParams.toDate))
                {
                    DateTime fromDate = DateTime.Parse(pagingParams.fromDate);
                    DateTime toDate = DateTime.Parse(pagingParams.toDate);
                    query = query.Where(x => (x.CreatedDate) >= fromDate.Date && (x.CreatedDate) <= toDate.Date.AddDays(1));
                }
                if (!string.IsNullOrEmpty(pagingParams.SortKey))
                {
                    if (pagingParams.SortKey == "maKhachHang" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.MaKhachHang);
                    }
                    if (pagingParams.SortKey == "maKhachHang" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.MaKhachHang);
                    }
                    if (pagingParams.SortKey == "diaChiGiaoHang" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.DiaChiGiaoHang);
                    }
                    if (pagingParams.SortKey == "diaChiGiaoHang" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.DiaChiGiaoHang);
                    }
                    if (pagingParams.SortKey == "vanPhongGiaoDich" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.VanPhongGiaoDich);
                    }
                    if (pagingParams.SortKey == "vanPhongGiaoDich" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.VanPhongGiaoDich);
                    }
                    if (pagingParams.SortKey == "thoiHanNo" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.ThoiHanNo);
                    }
                    if (pagingParams.SortKey == "thoiHanNo" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.ThoiHanNo);
                    }
                    if (pagingParams.SortKey == "hanMuc" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.HanMuc);
                    }
                    if (pagingParams.SortKey == "hanMuc" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.HanMuc);
                    }
                    if (pagingParams.SortKey == "deXuatNhanVien" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.DeXuatNhanVien);
                    }
                    if (pagingParams.SortKey == "deXuatNhanVien" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.DeXuatNhanVien);
                    }
                    if (pagingParams.SortKey == "createdDate" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.CreatedDate);
                    }
                    if (pagingParams.SortKey == "createdDate" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.CreatedDate);
                    }
                    if (pagingParams.SortKey == "tenKhachHang" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.TenKhachHang);
                    }
                    if (pagingParams.SortKey == "tenKhachHang" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.TenKhachHang);
                    }

                    if (pagingParams.SortKey == "maSoThue" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.MaSoThue);
                    }
                    if (pagingParams.SortKey == "maSoThue" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.MaSoThue);
                    }

                    if (pagingParams.SortKey == "diaChi" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.DiaChi);
                    }
                    if (pagingParams.SortKey == "diaChi" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.DiaChi);
                    }

                    if (pagingParams.SortKey == "tenNganhNghe" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.TenNganhNghe);
                    }
                    if (pagingParams.SortKey == "tenNganhNghe" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.TenNganhNghe);
                    }

                    if (pagingParams.SortKey == "tenVung" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.TenVung);
                    }
                    if (pagingParams.SortKey == "tenVung" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.TenVung);
                    }

                    if (pagingParams.SortKey == "trangThaiKhachHang" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.TrangThaiKhachHang);
                    }
                    if (pagingParams.SortKey == "trangThaiKhachHang" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.TrangThaiKhachHang);
                    }

                    if (pagingParams.SortKey == "nguoiLienHe" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.NguoiLienHe);
                    }
                    if (pagingParams.SortKey == "nguoiLienHe" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.NguoiLienHe);
                    }
                    if (pagingParams.SortKey == "cacVanDeCuaNhaCCCu" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.CacVanDeCuaNhaCCCu);
                    }
                    if (pagingParams.SortKey == "cacVanDeCuaNhaCCCu" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.CacVanDeCuaNhaCCCu);
                    }
                    if (pagingParams.SortKey == "mongMuonKhachHang" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.MongMuonKhachHang);
                    }
                    if (pagingParams.SortKey == "mongMuonKhachHang" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.MongMuonKhachHang);
                    }
                    if (pagingParams.SortKey == "giaTrietKhau" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.GiaTrietKhau);
                    }
                    if (pagingParams.SortKey == "giaTrietKhau" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.GiaTrietKhau);
                    }
                    if (pagingParams.SortKey == "nhaCungCapHienTai" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.NhaCungCapHienTai);
                    }
                    if (pagingParams.SortKey == "nhaCungCapHienTai" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.NhaCungCapHienTai);
                    }
                    if (pagingParams.SortKey == "phuongAnNhapHang" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.PhuongAnNhapHang);
                    }
                    if (pagingParams.SortKey == "phuongAnNhapHang" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.PhuongAnNhapHang);
                    }
                    if (pagingParams.SortKey == "danhGiaChung" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.DanhGiaChung);
                    }
                    if (pagingParams.SortKey == "danhGiaChung" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.DanhGiaChung);
                    }
                    if (pagingParams.SortKey == "luuY" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.LuuY);
                    }
                    if (pagingParams.SortKey == "luuY" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.LuuY);
                    }
                    if (pagingParams.SortKey == "nguoiThem" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.NguoiThem);
                    }
                    if (pagingParams.SortKey == "nguoiThem" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.NguoiThem);
                    }
                    if (pagingParams.SortKey == "chucVu" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.ChucVu);
                    }
                    if (pagingParams.SortKey == "chucVu" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.ChucVu);
                    }
                    if (pagingParams.SortKey == "sanLuongHangThang" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.SanLuongHangThang);
                    }
                    if (pagingParams.SortKey == "sanLuongHangThang" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.SanLuongHangThang);
                    }
                    if (pagingParams.SortKey == "loaiKhachHang" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.LoaiKhachHang);
                    }
                    if (pagingParams.SortKey == "loaiKhachHang" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.LoaiKhachHang);
                    }
                    if (pagingParams.SortKey == "soDienThoai" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.SoDienThoai);
                    }
                    if (pagingParams.SortKey == "soDienThoai" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.SoDienThoai);
                    }
                    if (pagingParams.SortKey == "nguoiDaiDienPhapLuat" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.NguoiDaiDienPhapLuat);
                    }
                    if (pagingParams.SortKey == "nguoiDaiDienPhapLuat" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.NguoiDaiDienPhapLuat);
                    }
                    if (pagingParams.SortKey == "soDienThoaiNguoiDaiDien" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.SoDienThoaiNguoiDaiDien);
                    }
                    if (pagingParams.SortKey == "soDienThoaiNguoiDaiDien" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.SoDienThoaiNguoiDaiDien);
                    }
                    if (pagingParams.SortKey == "keToan" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.KeToan);
                    }
                    if (pagingParams.SortKey == "keToan" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.KeToan);
                    }
                    if (pagingParams.SortKey == "soDienThoaiKeToan" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.SoDienThoaiKeToan);
                    }
                    if (pagingParams.SortKey == "soDienThoaiKeToan" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.SoDienThoaiKeToan);
                    }
                    if (pagingParams.SortKey == "checkCIC" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.CheckCIC);
                    }
                    if (pagingParams.SortKey == "checkCIC" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.CheckCIC);
                    }
                    if (pagingParams.SortKey == "congNo" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.CongNo);
                    }
                    if (pagingParams.SortKey == "congNo" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.CongNo);
                    }
                    if (pagingParams.SortKey == "baoLanhThanhToan" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.BaoLanhThanhToan);
                    }
                    if (pagingParams.SortKey == "baoLanhThanhToan" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.BaoLanhThanhToan);
                    }






                }
                if (pagingParams.PageSize == -1)
                {
                    pagingParams.PageSize = await query.CountAsync();
                }

                return await PagedList<CustomerViewModel>
                   .CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
            }
            else
                return null;

        }
        public async Task<CustomerViewModel> GetById(string Id)
        {
            var query = from dt in db.Customers
                        where dt.Id == Id
                        select new CustomerViewModel
                        {
                            Id = dt.Id,
                            TenKhachHang = dt.TenKhachHang,
                            DiaChi = dt.DiaChi,

                            NguoiLienHe = dt.NguoiLienHe,
                            MaKhachHang = dt.MaKhachHang,
                            MaSoThue = dt.MaSoThue,
                            SoDienThoai = dt.SoDienThoai,
                            Email = dt.Email,
                            GiamDoc = dt.GiamDoc,
                            SanLuongHangThang = dt.SanLuongHangThang,
                            PhuongAnNhapHang = dt.PhuongAnNhapHang,
                            NhaCungCapHienTai = dt.NhaCungCapHienTai,
                            GiaTrietKhau = dt.GiaTrietKhau,
                            MongMuonKhachHang = dt.MongMuonKhachHang ?? string.Empty,
                            CacVanDeCuaNhaCCCu = dt.CacVanDeCuaNhaCCCu,
                            ChucVu = dt.ChucVu,
                            LuuY = dt.LuuY ?? string.Empty,
                            DanhGiaChung = dt.DanhGiaChung ?? string.Empty,
                            LoaiKhachHang = dt.LoaiKhachHang,
                            TrangThaiKhachHang = dt.TrangThaiKhachHang,
                            MaNganhNghe = dt.MaNganhNghe,
                            MaVung = dt.MaVung,
                            TenVung = dt.TenVung,
                            TenNganhNghe = dt.TenNganhNghe,
                            VungId = dt.VungId,
                            NganhNgheId = dt.NganhNgheId
                        };
            return await query.FirstOrDefaultAsync();
        }


        public async Task<List<CustomerViewModel>> GetKHByUser(string Id)
        {
            var tg = new User();
            tg = await db.Users.FindAsync(Id);
            if (tg != null)
            {
                if (tg.RoleId == "NVKD")
                {
                    var query = from dt in db.Customers
                                where dt.CreatedBy == Id
                                where dt.TiemNang == true
                                select new CustomerViewModel
                                {
                                    Id = dt.Id,
                                    TenKhachHang = dt.TenKhachHang,
                                    DiaChi = dt.DiaChi,
                                    NguoiLienHe = dt.NguoiLienHe,
                                    MaKhachHang = dt.MaKhachHang,
                                    MaSoThue = dt.MaSoThue,
                                    SoDienThoai = dt.SoDienThoai,
                                    Email = dt.Email,
                                    GiamDoc = dt.GiamDoc,
                                    SanLuongHangThang = dt.SanLuongHangThang,
                                    PhuongAnNhapHang = dt.PhuongAnNhapHang,
                                    NhaCungCapHienTai = dt.NhaCungCapHienTai,
                                    GiaTrietKhau = dt.GiaTrietKhau,
                                    MongMuonKhachHang = dt.MongMuonKhachHang ?? string.Empty,
                                    CacVanDeCuaNhaCCCu = dt.CacVanDeCuaNhaCCCu,
                                    ChucVu = dt.ChucVu,
                                    LuuY = dt.LuuY ?? string.Empty,
                                    DanhGiaChung = dt.DanhGiaChung ?? string.Empty,
                                    LoaiKhachHang = dt.LoaiKhachHang,
                                    TrangThaiKhachHang = dt.TrangThaiKhachHang,
                                    MaNganhNghe = dt.MaNganhNghe,
                                    MaVung = dt.MaVung,
                                    TenVung = dt.TenVung,
                                    TenNganhNghe = dt.TenNganhNghe,
                                    VungId = dt.VungId,
                                    NganhNgheId = dt.NganhNgheId,
                                };
                    return await query.ToListAsync();
                }
                else
                {
                    var query = from dt in db.Customers
                                where dt.TiemNang == true
                                select new CustomerViewModel
                                {
                                    Id = dt.Id,
                                    TenKhachHang = dt.TenKhachHang,
                                    DiaChi = dt.DiaChi,
                                    NguoiLienHe = dt.NguoiLienHe,
                                    MaKhachHang = dt.MaKhachHang,
                                    MaSoThue = dt.MaSoThue,
                                    SoDienThoai = dt.SoDienThoai,
                                    Email = dt.Email,
                                    GiamDoc = dt.GiamDoc,
                                    SanLuongHangThang = dt.SanLuongHangThang,
                                    PhuongAnNhapHang = dt.PhuongAnNhapHang,
                                    NhaCungCapHienTai = dt.NhaCungCapHienTai,
                                    GiaTrietKhau = dt.GiaTrietKhau,
                                    MongMuonKhachHang = dt.MongMuonKhachHang ?? string.Empty,
                                    CacVanDeCuaNhaCCCu = dt.CacVanDeCuaNhaCCCu,
                                    ChucVu = dt.ChucVu,
                                    LuuY = dt.LuuY ?? string.Empty,
                                    DanhGiaChung = dt.DanhGiaChung ?? string.Empty,
                                    LoaiKhachHang = dt.LoaiKhachHang,
                                    TrangThaiKhachHang = dt.TrangThaiKhachHang,
                                    MaNganhNghe = dt.MaNganhNghe,
                                    MaVung = dt.MaVung,
                                    TenVung = dt.TenVung,
                                    TenNganhNghe = dt.TenNganhNghe,
                                    VungId = dt.VungId,
                                    NganhNgheId = dt.NganhNgheId,
                                };
                    return await query.ToListAsync();
                }


            }
            else
                return null;
        }
        public async Task<bool> CheckTrungMa(string MaKH)
        {
            var rs = await db.Customers.FirstOrDefaultAsync(x => x.MaKhachHang.ToString().ToUpper().ToTrim() == (MaKH.ToString().ToUpper().ToTrim()));
            if (rs != null) return true;
            else return false;
        }

        public async Task<string> CreateMa()
        {
            var code = db.GetLastSTT("Vungs");
            int codeInt = code + 1;
            string codeStr = codeInt.ToAutoIncrementOrderCode();

            return codeStr;
        }
        public async Task<string> ExportExcelAsync(PagingParams pagingParams, string selectedId)
        {
            User currentUser = await db.Users
                .AsNoTracking()
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.UserId == pagingParams.userId);

            var query = from main in db.Customers
                        join us in db.Users on main.CreatedBy equals us.UserId
                        orderby main.CreatedDate descending
                        select new CustomerViewModel
                        {
                            TenKhachHang = main.TenKhachHang,
                            LoaiKhachHang = main.LoaiKhachHang,
                            GiaTrietKhau = main.GiaTrietKhau,
                            Id = main.Id,
                            NganhNgheId = main.NganhNgheId,
                            MaNganhNghe = main.MaNganhNghe,
                            TenNganhNghe = main.TenNganhNghe,
                            TenVung = main.TenVung,
                            VungId = main.VungId,
                            ChucVu = main.ChucVu,
                            TrangThaiKhachHang = main.TrangThaiKhachHang,
                            NhaCungCapHienTai = main.NhaCungCapHienTai,
                            CacVanDeCuaNhaCCCu = main.CacVanDeCuaNhaCCCu,
                            CreatedBy = main.CreatedBy,
                            CreatedDate = main.CreatedDate,
                            MaSoThue = main.MaSoThue,
                            MongMuonKhachHang = main.MongMuonKhachHang,
                            LuuY = main.LuuY,
                            SoDienThoai = main.SoDienThoai,
                            SanLuongHangThang = main.SanLuongHangThang,
                            NguoiLienHe = main.NguoiLienHe,
                            DanhGiaChung = main.DanhGiaChung,
                            DiaChi = main.DiaChi,
                            Email = main.Email,
                            MaKhachHang = main.MaKhachHang,
                            MaVung = main.MaVung,
                            PhuongAnNhapHang = main.PhuongAnNhapHang,
                            Status = main.Status,
                            NguoiThem = us.FullName,
                            NguoiDaiDienPhapLuat = main.NguoiDaiDienPhapLuat,
                            SoDienThoaiNguoiDaiDien = main.SoDienThoaiNguoiDaiDien,
                            KeToan = main.KeToan,
                            SoDienThoaiKeToan = main.SoDienThoaiKeToan,
                            CheckCIC = main.CheckCIC,
                            CongNo = main.CongNo,
                            BaoLanhThanhToan = main.BaoLanhThanhToan,
                            DeXuatNhanVien = main.DeXuatNhanVien,
                            DiaChiGiaoHang = main.DiaChiGiaoHang,
                            HanMuc = main.HanMuc,
                            ThoiHanNo = main.ThoiHanNo,
                            VanPhongGiaoDich = main.VanPhongGiaoDich,
                            

                        };

            //if (!string.IsNullOrEmpty(pagingParams.fromDate) && !string.IsNullOrEmpty(pagingParams.toDate))
            //{
            //    DateTime fromDate = DateTime.Parse(pagingParams.fromDate);
            //    DateTime toDate = DateTime.Parse(pagingParams.toDate);
            //    query = query.Where(x => DateTime.Parse(x.NgayLap) >= fromDate && DateTime.Parse(x.NgayLap) <= toDate);
            //}
            if (!string.IsNullOrEmpty(pagingParams.fromDate) && !string.IsNullOrEmpty(pagingParams.toDate))
            {
                DateTime fromDate = DateTime.Parse(pagingParams.fromDate);
                DateTime toDate = DateTime.Parse(pagingParams.toDate);
                query = query.Where(x => (x.CreatedDate >= fromDate && x.CreatedDate <= toDate.AddDays(1)));
            }
            if (!string.IsNullOrEmpty(pagingParams.userId))
            {
                if (currentUser.Role.RoleId != "ADMIN" && currentUser.Role.RoleId != "BLD")
                {
                    query = query.Where(x => x.CreatedBy == currentUser.UserId);
                }
                else
                {
                    if (selectedId != null && selectedId != "")
                    {
                        query = query.Where(x => x.CreatedBy == selectedId);
                    }
                }
            }

            //if (!string.IsNullOrEmpty(pagingParams.nhanVienId))
            //{
            //    query = query.Where(x => x.CreatedBy == pagingParams.nhanVienId);
            //}

            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                var keyword = pagingParams.Keyword.ToUpper().ToTrim();

                query = query.Where(x =>
                                        (x.NguoiThem ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.NguoiThem ?? string.Empty).ToUpper().Contains(keyword) ||

                                        (x.MaKhachHang ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.MaKhachHang ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.TenKhachHang ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.TenKhachHang ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.MaSoThue ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.MaSoThue ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.TenVung ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.TenVung ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.DiaChi ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.DiaChi ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.DiaChiGiaoHang ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.DiaChiGiaoHang ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.VanPhongGiaoDich ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.VanPhongGiaoDich ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.Email ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.Email ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.LoaiKhachHang ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.LoaiKhachHang ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.TenNganhNghe ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.TenNganhNghe ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.SoDienThoai ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.SoDienThoai ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.TrangThaiKhachHang ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.TrangThaiKhachHang ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.NguoiLienHe ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.NguoiLienHe ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.ChucVu ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.ChucVu ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.NguoiDaiDienPhapLuat ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.NguoiDaiDienPhapLuat ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.SoDienThoaiNguoiDaiDien ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.SoDienThoaiNguoiDaiDien ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.KeToan ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.KeToan ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.SoDienThoaiKeToan ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.SoDienThoaiKeToan ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.CongNo ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.CongNo ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.CheckCIC ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.CheckCIC ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.HanMuc ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.HanMuc ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.ThoiHanNo ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.ThoiHanNo ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.BaoLanhThanhToan ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.BaoLanhThanhToan ?? string.Empty).ToUpper().Contains(keyword) ||

                                        (x.PhuongAnNhapHang ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.PhuongAnNhapHang ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.NhaCungCapHienTai ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.NhaCungCapHienTai ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.GiaTrietKhau ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.GiaTrietKhau ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.MongMuonKhachHang ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.MongMuonKhachHang ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.CacVanDeCuaNhaCCCu ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.CacVanDeCuaNhaCCCu ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.DanhGiaChung ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.DanhGiaChung ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.DeXuatNhanVien ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.DeXuatNhanVien ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.LuuY ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.LuuY ?? string.Empty).ToUpper().Contains(keyword)
                                        //(x.CreatedDate) == DateTime.Parse(keyword)
                                        );

            }

            if (!string.IsNullOrEmpty(pagingParams.KeywordCol))
            {
                var keyword = pagingParams.KeywordCol.ToUpper().ToTrim();


                if (pagingParams.ColName == "nguoiThem")
                {
                    query = query.Where(x => (x.NguoiThem ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.NguoiThem ?? string.Empty).ToUpper().Contains(keyword));
                }
                if (pagingParams.ColName == "maKhachHang")
                {
                    query = query.Where(x => (x.MaKhachHang ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.MaKhachHang ?? string.Empty).ToUpper().Contains(keyword));
                }
                if (pagingParams.ColName == "tenKhachHang")
                {
                    query = query.Where(x => (x.TenKhachHang ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.TenKhachHang ?? string.Empty).ToUpper().Contains(keyword));
                }
                if (pagingParams.ColName == "maSoThue")
                {
                    query = query.Where(x => (x.MaSoThue ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.MaSoThue ?? string.Empty).ToUpper().Contains(keyword));
                }
                if (pagingParams.ColName == "tenVung")
                {
                    query = query.Where(x => (x.TenVung ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.TenVung ?? string.Empty).ToUpper().Contains(keyword));
                }
                if (pagingParams.ColName == "diaChi")
                {
                    query = query.Where(x => (x.DiaChi ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.DiaChi ?? string.Empty).ToUpper().Contains(keyword));
                }
                if (pagingParams.ColName == "diaChiGiaoHang")
                {
                    query = query.Where(x => (x.DiaChiGiaoHang ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.DiaChiGiaoHang ?? string.Empty).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "vanPhongGiaoDich")
                {
                    query = query.Where(x => (x.VanPhongGiaoDich ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.VanPhongGiaoDich ?? string.Empty).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "email")
                {
                    query = query.Where(x => (x.Email ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.Email ?? string.Empty).ToUpper().Contains(keyword));
                }
                if (pagingParams.ColName == "loaiKhachHang")
                {
                    query = query.Where(x => (x.LoaiKhachHang ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.LoaiKhachHang ?? string.Empty).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "tenNganhNghe")
                {
                    query = query.Where(x => (x.TenNganhNghe ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.TenNganhNghe ?? string.Empty).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "soDienThoai")
                {
                    query = query.Where(x => (x.SoDienThoai ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.SoDienThoai ?? string.Empty).ToUpper().Contains(keyword));
                }
                if (pagingParams.ColName == "trangThaiKhachHang")
                {
                    query = query.Where(x => (x.TrangThaiKhachHang ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.TrangThaiKhachHang ?? string.Empty).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "nguoiLienHe")
                {
                    query = query.Where(x => (x.NguoiLienHe ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.NguoiLienHe ?? string.Empty).ToUpper().Contains(keyword));
                }
                if (pagingParams.ColName == "chucVu")
                {
                    query = query.Where(x => (x.ChucVu ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.ChucVu ?? string.Empty).ToUpper().Contains(keyword));
                }
                if (pagingParams.ColName == "nguoiDaiDienPhapLuat")
                {
                    query = query.Where(x => (x.NguoiDaiDienPhapLuat ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.NguoiDaiDienPhapLuat ?? string.Empty).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "soDienThoaiNguoiDaiDien")
                {
                    query = query.Where(x => (x.SoDienThoaiNguoiDaiDien ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.SoDienThoaiNguoiDaiDien ?? string.Empty).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "keToan")
                {
                    query = query.Where(x => (x.KeToan ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.KeToan ?? string.Empty).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "soDienThoaiKeToan")
                {
                    query = query.Where(x => (x.SoDienThoaiKeToan ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.SoDienThoaiKeToan ?? string.Empty).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "congNo")
                {
                    query = query.Where(x => (x.CongNo ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.CongNo ?? string.Empty).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "checkCIC")
                {
                    query = query.Where(x => (x.CheckCIC ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.CheckCIC ?? string.Empty).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "hanMuc")
                {
                    query = query.Where(x => (x.HanMuc ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.HanMuc ?? string.Empty).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "thoiHanNo")
                {
                    query = query.Where(x => (x.ThoiHanNo ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.ThoiHanNo ?? string.Empty).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "baoLanhThanhToan")
                {
                    query = query.Where(x => (x.BaoLanhThanhToan ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.BaoLanhThanhToan ?? string.Empty).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "phuongAnNhapHang")
                {
                    query = query.Where(x => (x.PhuongAnNhapHang ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.PhuongAnNhapHang ?? string.Empty).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "nhaCungCapHienTai")
                {
                    query = query.Where(x => (x.NhaCungCapHienTai ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.NhaCungCapHienTai ?? string.Empty).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "giaTrietKhau")
                {
                    query = query.Where(x => (x.GiaTrietKhau ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.GiaTrietKhau ?? string.Empty).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "mongMuonKhachHang")
                {
                    query = query.Where(x => (x.MongMuonKhachHang ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.MongMuonKhachHang ?? string.Empty).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "cacVanDeCuaNhaCCCu")
                {
                    query = query.Where(x => (x.CacVanDeCuaNhaCCCu ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.CacVanDeCuaNhaCCCu ?? string.Empty).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "danhGiachung")
                {
                    query = query.Where(x => (x.DanhGiaChung ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.DanhGiaChung ?? string.Empty).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "deXuatNhanVien")
                {
                    query = query.Where(x => (x.DeXuatNhanVien ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.DeXuatNhanVien ?? string.Empty).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "luuY")
                {
                    query = query.Where(x => (x.LuuY ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.LuuY ?? string.Empty).ToUpper().Contains(keyword));

                }
                //if (pagingParams.ColName == "createDate")
                //{
                //    query = query.Where(x => (x.CreatedDate) == DateTime.Parse(keyword));

                //}
                if (pagingParams.ColName == "sanLuongHangThang")
                {
                    query = query.Where(x => (x.SanLuongHangThang == Convert.ToDecimal(keyword)));

                }

            }

            if (!string.IsNullOrEmpty(pagingParams.SortKey))
            {
                if (pagingParams.SortKey == "nguoiThem" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.NguoiThem);
                }
                if (pagingParams.SortKey == "nguoiThem" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.NguoiThem);
                }
                if (pagingParams.SortKey == "maKhachHang" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.MaKhachHang);
                }
                if (pagingParams.SortKey == "maKhachHang" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.MaKhachHang);
                }

                if (pagingParams.SortKey == "tenKhachHang" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.TenKhachHang);
                }
                if (pagingParams.SortKey == "tenKhachHang" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.TenKhachHang);
                }

                if (pagingParams.SortKey == "maSoThue" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.MaSoThue);
                }
                if (pagingParams.SortKey == "maSoThue" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.MaSoThue);
                }

                if (pagingParams.SortKey == "tenVung" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.TenVung);
                }
                if (pagingParams.SortKey == "tenVung" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.TenVung);
                }

                if (pagingParams.SortKey == "diaChi" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.DiaChi);
                }
                if (pagingParams.SortKey == "diaChi" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.DiaChi);
                }

                if (pagingParams.SortKey == "diaChiGiaoHang" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.DiaChiGiaoHang);
                }
                if (pagingParams.SortKey == "diaChiGiaoHang" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.DiaChiGiaoHang);
                }

                if (pagingParams.SortKey == "vanPhongGiaoDich" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.VanPhongGiaoDich);
                }
                if (pagingParams.SortKey == "vanPhongGiaoDich" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.VanPhongGiaoDich);
                }

                if (pagingParams.SortKey == "email" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.Email);
                }
                if (pagingParams.SortKey == "email" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.Email);
                }

                if (pagingParams.SortKey == "loaiKhachHang" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.LoaiKhachHang);
                }
                if (pagingParams.SortKey == "loaiKhachHang" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.LoaiKhachHang);
                }

                if (pagingParams.SortKey == "tenNganhNghe" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.TenNganhNghe);
                }
                if (pagingParams.SortKey == "tenNganhNghe" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.TenNganhNghe);
                }

                if (pagingParams.SortKey == "soDienThoai" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.SoDienThoai);
                }
                if (pagingParams.SortKey == "soDienThoai" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.SoDienThoai);
                }

                if (pagingParams.SortKey == "trangThaiKhachHang" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.TrangThaiKhachHang);
                }
                if (pagingParams.SortKey == "trangThaiKhachHang" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.TrangThaiKhachHang);
                }

                if (pagingParams.SortKey == "nguoiLienHe" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.NguoiLienHe);
                }
                if (pagingParams.SortKey == "nguoiLienHe" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.NguoiLienHe);
                }

                if (pagingParams.SortKey == "chucVu" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.ChucVu);
                }
                if (pagingParams.SortKey == "chucVu" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.ChucVu);
                }

                if (pagingParams.SortKey == "nguoiDaiDienPhapLuat" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.NguoiDaiDienPhapLuat);
                }
                if (pagingParams.SortKey == "nguoiDaiDienPhapLuat" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.NguoiDaiDienPhapLuat);
                }

                if (pagingParams.SortKey == "soDienThoaiNguoiDaiDien" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.SoDienThoaiNguoiDaiDien);
                }
                if (pagingParams.SortKey == "soDienThoaiNguoiDaiDien" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.SoDienThoaiNguoiDaiDien);
                }

                if (pagingParams.SortKey == "keToan" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.KeToan);
                }
                if (pagingParams.SortKey == "keToan" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.KeToan);
                }

                if (pagingParams.SortKey == "soDienThoaiKeToan" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.SoDienThoaiKeToan);
                }
                if (pagingParams.SortKey == "soDienThoaiKeToan" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.SoDienThoaiKeToan);
                }

                if (pagingParams.SortKey == "checkCIC" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.CheckCIC);
                }
                if (pagingParams.SortKey == "checkCIC" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.CheckCIC);
                }

                if (pagingParams.SortKey == "congNo" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.CongNo);
                }
                if (pagingParams.SortKey == "congNo" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.CongNo);
                }

                if (pagingParams.SortKey == "hanMuc" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.HanMuc);
                }
                if (pagingParams.SortKey == "hanMuc" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.HanMuc);
                }

                if (pagingParams.SortKey == "thoiHanNo" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.ThoiHanNo);
                }
                if (pagingParams.SortKey == "thoiHanNo" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.ThoiHanNo);
                }

                if (pagingParams.SortKey == "baoLanhThanhToan" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.BaoLanhThanhToan);
                }
                if (pagingParams.SortKey == "baoLanhThanhToan" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.BaoLanhThanhToan);
                }

                if (pagingParams.SortKey == "phuongAnNhapHang" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.PhuongAnNhapHang);
                }
                if (pagingParams.SortKey == "phuongAnNhapHang" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.PhuongAnNhapHang);
                }

                if (pagingParams.SortKey == "nhaCungCapHienTai" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.NhaCungCapHienTai);
                }
                if (pagingParams.SortKey == "nhaCungCapHienTai" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.NhaCungCapHienTai);
                }

                if (pagingParams.SortKey == "giaTrietKhau" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.GiaTrietKhau);
                }
                if (pagingParams.SortKey == "giaTrietKhau" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.GiaTrietKhau);
                }

                if (pagingParams.SortKey == "mongMuonKhachHang" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.MongMuonKhachHang);
                }
                if (pagingParams.SortKey == "mongMuonKhachHang" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.MongMuonKhachHang);
                }

                if (pagingParams.SortKey == "cacVanDeCuaNhaCCCu" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.CacVanDeCuaNhaCCCu);
                }
                if (pagingParams.SortKey == "cacVanDeCuaNhaCCCu" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.CacVanDeCuaNhaCCCu);
                }

                if (pagingParams.SortKey == "danhGiaChung" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.DanhGiaChung);
                }
                if (pagingParams.SortKey == "danhGiaChung" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.DanhGiaChung);
                }

                if (pagingParams.SortKey == "deXuatNhanVien" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.DeXuatNhanVien);
                }
                if (pagingParams.SortKey == "deXuatNhanVien" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.DeXuatNhanVien);
                }

                if (pagingParams.SortKey == "luuY" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.LuuY);
                }
                if (pagingParams.SortKey == "luuY" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.LuuY);
                }

                if (pagingParams.SortKey == "sanLuongHangThang" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.SanLuongHangThang);
                }
                if (pagingParams.SortKey == "sanLuongHangThang" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.SanLuongHangThang);
                }

                //if (pagingParams.SortKey == "createDate" && pagingParams.SortValue == "ascend")
                //{
                //    query = query.OrderBy(x => x.CreatedDate);
                //}
                //if (pagingParams.SortKey == "createDate" && pagingParams.SortValue == "descend")
                //{
                //    query = query.OrderByDescending(x => x.CreatedDate);
                //}

            }

            string _path_sample = Path.Combine(_hostingEnvironment.ContentRootPath, $"MyAssets/uploaded/sample/Bao_cao_khach_hang.xlsx");
            string desFileName = Guid.NewGuid() + ".xlsx";
            string desFilePath = Path.Combine(_hostingEnvironment.ContentRootPath, $"MyAssets/uploaded/excels/{desFileName}");
            string uploadFolder = Path.Combine(_hostingEnvironment.ContentRootPath, "MyAssets/uploaded/excels");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            // Excel
            FileInfo file = new FileInfo(_path_sample);
            //string dateReport = string.Format("Báo cáo khách hàng (Từ ngày {0} đến ngày {1})",
            //    string.IsNullOrEmpty(pagingParams.fromDate) ? string.Empty : DateTime.Parse(pagingParams.fromDate).ToString("dd/MM/yyyy"),
            //    string.IsNullOrEmpty(pagingParams.toDate) ? string.Empty : DateTime.Parse(pagingParams.toDate).ToString("dd/MM/yyyy"));

            using (ExcelPackage package = new ExcelPackage(file))
            {
                try
                {
                    List<CustomerViewModel> list = query.ToList();
                    // Open sheet1
                    int totalRows = list.Count;

                    // Begin row
                    int begin_row = 3;

                    // Open sheet1
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    // Add Row
                    worksheet.InsertRow(begin_row + 1, totalRows - 1, begin_row);

                    // Fill data
                    int idx = begin_row;
                    for (int i = 0; i < list.Count; i++)
                    {
                        var it = list[i];
                        worksheet.Cells[idx, 1].Value = i + 1;

                        worksheet.Cells[idx, 2].Value = it.NguoiThem;
                        worksheet.Cells[idx, 3].Value = it.CreatedDate.Value.ToString("dd/MM/yyyy");
                        worksheet.Cells[idx, 4].Value = it.MaKhachHang;
                        worksheet.Cells[idx, 5].Value = it.TenKhachHang;
                        worksheet.Cells[idx, 6].Value = it.MaSoThue;
                        worksheet.Cells[idx, 7].Value = it.TenVung;
                        worksheet.Cells[idx, 8].Value = it.DiaChi;
                        worksheet.Cells[idx, 9].Value = it.DiaChiGiaoHang;
                        worksheet.Cells[idx, 10].Value = it.VanPhongGiaoDich;
                        worksheet.Cells[idx, 11].Value = it.Email;
                        worksheet.Cells[idx, 12].Value = it.LoaiKhachHang;
                        worksheet.Cells[idx, 13].Value = it.TenNganhNghe;
                        worksheet.Cells[idx, 14].Value = it.SoDienThoai;
                        worksheet.Cells[idx, 15].Value = it.TrangThaiKhachHang;
                        worksheet.Cells[idx, 16].Value = it.NguoiLienHe;
                        worksheet.Cells[idx, 17].Value = it.ChucVu;
                        worksheet.Cells[idx, 18].Value = it.NguoiDaiDienPhapLuat;
                        worksheet.Cells[idx, 19].Value = it.SoDienThoaiNguoiDaiDien;
                        worksheet.Cells[idx, 20].Value = it.KeToan;
                        worksheet.Cells[idx, 21].Value = it.SoDienThoaiKeToan;
                        worksheet.Cells[idx, 22].Value = it.CongNo;
                        worksheet.Cells[idx, 23].Value = it.CheckCIC;
                        worksheet.Cells[idx, 24].Value = it.HanMuc;
                        worksheet.Cells[idx, 25].Value = it.ThoiHanNo;
                        worksheet.Cells[idx, 26].Value = it.BaoLanhThanhToan;
                        worksheet.Cells[idx, 27].Value = it.SanLuongHangThang;
                        worksheet.Cells[idx, 28].Value = it.PhuongAnNhapHang;
                        worksheet.Cells[idx, 29].Value = it.NhaCungCapHienTai;
                        worksheet.Cells[idx, 30].Value = it.GiaTrietKhau;
                        worksheet.Cells[idx, 31].Value = it.MongMuonKhachHang;
                        worksheet.Cells[idx, 32].Value = it.CacVanDeCuaNhaCCCu;
                        worksheet.Cells[idx, 33].Value = it.DanhGiaChung;
                        worksheet.Cells[idx, 34].Value = it.DeXuatNhanVien;
                        worksheet.Cells[idx, 35].Value = it.LuuY;

                        idx += 1;
                    }
                    //worksheet.Cells[1, 1].Value = dateReport.ToUpper();
                    worksheet.Row(1).Style.Font.Italic = true;
                    // Total
                    worksheet.Row(idx).Style.Font.Bold = true;
                    //worksheet.Cells[2, 4].Value = string.Format("Tổng số khách hàng = {0}", list.Count);

                    package.SaveAs(new FileInfo(desFilePath));
                    Byte[] bytes = File.ReadAllBytes(desFilePath);
                    String base64String = Convert.ToBase64String(bytes);

                    if (File.Exists(desFilePath))
                    {
                        File.Delete(desFilePath);
                    }

                    return base64String;
                }
                catch (Exception e)
                {

                    throw;
                }

            }


        }
    }
}