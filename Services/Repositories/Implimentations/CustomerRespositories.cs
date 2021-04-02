using AutoMapper;
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
            model.CustomerName = model.CustomerName;
            model.CustomerCode = model.CustomerCode.ToTrim();
            model.Address = model.Address;
            model.DoB = model.DoB;
            model.Job = model.Job;
            model.NumberPhone = model.NumberPhone.ToTrim();
            model.Note = model.Note;
            model.Height = model.Height;
            model.Weight = model.Weight;
            model.HealthStatus = model.HealthStatus;
            model.Email = model.Email.ToTrim();
            model.Status = true;
            model.CreatedBy = model.CreatedBy;
            model.CreatedDate = model.CreatedDate;
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
            var tmp = new KhachHangLogViewModel();
            var ur = await db.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == model.Id);
           /* if (ur.Id == model.Id)
            {
                tmp.CreatedBy = model.ModifiedBy;
                if (ur.CustomerName != model.CustomerName)
                {
                    if (model.CustomerName == "" && ur.CustomerName == null)
                    {

                    }
                    else
                    {
                        tmp.TenTruongThayDoi = "CustomerName";
                        tmp.DienGiai = "Tên khách hàng";
                        tmp.DuLieuCu = ur.CustomerName;
                        tmp.DuLieuMoi = model.CustomerName;
                        tmp.CustomerId = model.Id;
                        tmp.CreatedDate = DateTime.Now;
                        await GetLichSuKH(tmp);
                    }
                }
                if (ur.Address != model.Address)
                {
                    if (model.Address == "" && ur.Address == null)
                    {

                    }
                    else
                    {
                        tmp.TenTruongThayDoi = "Address";
                        tmp.DienGiai = "Địa chỉ";
                        tmp.DuLieuCu = ur.Address;
                        tmp.DuLieuMoi = model.Address;
                        tmp.CustomerId = model.Id;
                        tmp.CreatedDate = DateTime.Now;
                        await GetLichSuKH(tmp);
                    }
                }
                if (ur.DoB != model.DoB)
                {
                    if (model.DoB.ToString() == "" && ur.DoB.ToString() == null)
                    {

                    }
                    else
                    {
                        tmp.TenTruongThayDoi = "DoB";
                        tmp.DienGiai = "Ngày sinh";
                        tmp.DuLieuCu = Convert.ToString(ur.DoB);
                        tmp.DuLieuMoi = Convert.ToString(ur.DoB);
                        tmp.CustomerId = model.Id;
                        tmp.CreatedDate = DateTime.Now;
                        await GetLichSuKH(tmp);
                    }
                }
                if (ur.Job != model.Job)
                {
                    if (model.Job.ToString() == "" && ur.Job.ToString() == null)
                    {

                    }
                    else
                    {

                        tmp.TenTruongThayDoi = "Job";
                        tmp.DienGiai = "Công việc hiện tại";
                        tmp.DuLieuCu = Convert.ToString(ur.Job);
                        tmp.DuLieuMoi = Convert.ToString(model.Job);
                        tmp.CustomerId = model.Id;
                        tmp.CreatedDate = DateTime.Now;
                        await GetLichSuKH(tmp);
                    }
                }
                if (ur.NumberPhone != model.NumberPhone)
                {
                    if (model.NumberPhone == "" && ur.NumberPhone == null)
                    {

                    }
                    else
                    {
                        tmp.TenTruongThayDoi = "NumberPhone";
                        tmp.DienGiai = "Số điện thoại";
                        tmp.DuLieuCu = Convert.ToString(ur.NumberPhone);
                        tmp.DuLieuMoi = model.NumberPhone;
                        tmp.CustomerId = model.Id;
                        tmp.CreatedDate = DateTime.Now;
                        await GetLichSuKH(tmp);
                    }
                }
                if (ur.Note != model.Note)
                {
                    if (model.Note == "" && ur.Note == null)
                    {

                    }
                    else
                    {
                        tmp.TenTruongThayDoi = "Note";
                        tmp.DienGiai = "Chú ý";
                        tmp.DuLieuCu = ur.Note;
                        tmp.DuLieuMoi = model.Note;
                        tmp.CustomerId = model.Id;
                        tmp.CreatedDate = DateTime.Now;
                        await GetLichSuKH(tmp);
                    }
                }
                if (ur.Email != model.Email)
                {
                    if (model.Email == "" && ur.Email == null)
                    {

                    }
                    else
                    {
                        tmp.TenTruongThayDoi = "Email";
                        tmp.DienGiai = "Email";
                        tmp.DuLieuCu = ur.Email;
                        tmp.DuLieuMoi = model.Email;
                        tmp.CustomerId = model.Id;
                        tmp.CreatedDate = DateTime.Now;
                        await GetLichSuKH(tmp);
                    }
                }
                if (ur.Height != model.Height)
                {
                    if (model.Height.ToString() == "" && ur.Height.ToString() == null)
                    {

                    }
                    else
                    {
                        tmp.TenTruongThayDoi = "Height";
                        tmp.DienGiai = "Chiều cao";
                        tmp.DuLieuCu = ur.Height.ToString();
                        tmp.DuLieuMoi = model.Height.ToString();
                        tmp.CustomerId = model.Id;
                        tmp.CreatedDate = DateTime.Now;
                        await GetLichSuKH(tmp);
                    }
                }
                if (ur.Weight != model.Weight)
                {
                    if (model.Weight.ToString() == "" && ur.Weight.ToString() == null)
                    {

                    }
                    else
                    {
                        tmp.TenTruongThayDoi = "Weight";
                        tmp.DienGiai = "Cân nặng";
                        tmp.DuLieuCu = ur.Weight.ToString();
                        tmp.DuLieuMoi = model.Weight.ToString();
                        tmp.CustomerId = model.Id;
                        tmp.CreatedDate = DateTime.Now;
                        await GetLichSuKH(tmp);
                    }
                }
                if (ur.HealthStatus != model.HealthStatus)
                {
                    if (model.HealthStatus == "" && ur.HealthStatus == null)
                    {

                    }
                    else
                    {
                        tmp.TenTruongThayDoi = "HealthStatus";
                        tmp.DienGiai = "Tình trạng sức khỏe";
                        tmp.DuLieuCu = ur.HealthStatus;
                        tmp.DuLieuMoi = model.HealthStatus;
                        tmp.CustomerId = model.Id;
                        tmp.CreatedDate = DateTime.Now;
                        await GetLichSuKH(tmp);
                    }
                }
                if (ur.CustomerCode != model.CustomerCode)
                {
                    if (model.CustomerCode == "" && ur.CustomerCode == null)
                    {

                    }
                    else
                    {
                        tmp.TenTruongThayDoi = "CustomerCode";
                        tmp.DienGiai = "Mã khách hàng";
                        tmp.DuLieuCu = ur.CustomerCode;
                        tmp.DuLieuMoi = model.CustomerCode;
                        tmp.CustomerId = model.Id;
                        tmp.CreatedDate = DateTime.Now;
                        await GetLichSuKH(tmp);
                    }
                }
            }*/
            try
            {
                if (TN == 1)
                {
                    ur.CustomerCode = model.CustomerCode.Trim();
                    ur.CustomerName = model.CustomerName;
                    ur.Address = model.Address;
                    ur.DoB = model.DoB;
                    ur.JobId = model.JobId;
                    ur.NumberPhone = model.NumberPhone;
                    ur.Note = model.Note;
                    ur.Email = model.Email;
                    ur.Height = model.Height;
                    ur.Weight = model.Weight;
                    ur.HealthStatus = model.HealthStatus;
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
                db.Customers.Update(ur);
                var rs = await db.SaveChangesAsync();
                return rs; // 1 thanh cong, 0 that bai

            }
            catch (Exception e)
            {

                throw;
            }

        }
          public async Task<int> GetLichSuKH(KhachHangLogViewModel model)
          {
              model.Id = Guid.NewGuid().ToString();
              model.TenTruongThayDoi = model.TenTruongThayDoi;
              model.DuLieuCu = model.DuLieuCu;
              model.DuLieuMoi = model.DuLieuMoi;
              model.CustomerId = model.CustomerId;
              model.DienGiai = model.DienGiai;
              model.CreatedDate = DateTime.Now;
              var entity = mp.Map<KhachHangLog>(model);
              await db.KhachHangLogs.AddAsync(entity);
              var rs = await db.SaveChangesAsync();
              // thanh cong 1, o loi
              return rs;
          }
          public async Task<List<KhachHangLogViewModel>> GetAllLiSuKH()
          {
              var query = from dt in db.KhachHangLogs
                          join kh in db.Customers on dt.CustomerId equals kh.Id
                          orderby dt.CreatedDate descending
                          select new KhachHangLogViewModel
                          {
                              Id = dt.Id,
                              CustomerCode = kh.CustomerCode,
                              TenTruongThayDoi=dt.TenTruongThayDoi,
                              DienGiai=dt.DienGiai,
                              DuLieuMoi=dt.DuLieuMoi,
                              DuLieuCu=dt.DuLieuCu,
                              CreatedDate=dt.CreatedDate,
                              CustomerId=dt.CustomerId
                          };
              return await query.ToListAsync();
          }
        public async Task<List<CustomerViewModel>> GetAllKH()
        {
            var query = from dt in db.Customers
                        join jb in db.Jobs on dt.JobId equals jb.Id

                        orderby dt.ModifiedDate descending
                        select new CustomerViewModel
                        {
                            Id = dt.Id,
                            CustomerName = dt.CustomerName,
                            CustomerCode = dt.CustomerCode.ToTrim(),
                            Address = dt.Address,
                            DoB = dt.DoB,
                            Job = jb.JobName,
                            NumberPhone = dt.NumberPhone.ToTrim(),
                            Note = dt.Note,
                            Height = dt.Height,
                            Weight = dt.Weight,
                            HealthStatus = dt.HealthStatus,
                            Email = dt.Email.ToTrim(),
                            Status = true,
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
                                join us in db.Users on dt.CreatedBy equals us.UserId
                                join jb in db.Jobs on dt.JobId equals jb.Id
                                orderby dt.CreatedDate descending
                                select new CustomerViewModel
                                {
                                    Id = dt.Id,
                                    CustomerName = dt.CustomerName,
                                    CustomerCode = dt.CustomerCode.ToTrim(),
                                    Address = dt.Address,
                                    DoB = dt.DoB,
                                    JobId = dt.JobId,
                                    JobName = jb.JobName,
                                    NumberPhone = dt.NumberPhone.ToTrim(),
                                    Note = dt.Note,
                                    Height = dt.Height,
                                    Weight = dt.Weight,
                                    HealthStatus = dt.HealthStatus,
                                    Email = dt.Email.ToTrim(),
                                    Status = true,
                                    CreatedDate = dt.CreatedDate,
                                    CreatedBy = dt.CreatedBy,
                                    NguoiThem = us.FullName
                                };
                    }
                    else
                    {
                        query = from dt in db.Customers
                                where dt.CreatedBy == selectedId
                                join us in db.Users on dt.CreatedBy equals us.UserId
                                join jb in db.Jobs on dt.JobId equals jb.Id
                                orderby dt.CreatedDate descending
                                select new CustomerViewModel
                                {
                                    Id = dt.Id,
                                    CustomerName = dt.CustomerName,
                                    CustomerCode = dt.CustomerCode.ToTrim(),
                                    Address = dt.Address,
                                    DoB = dt.DoB,
                                    JobId = dt.JobId,
                                    JobName = jb.JobName,
                                    NumberPhone = dt.NumberPhone.ToTrim(),
                                    Note = dt.Note,
                                    Height = dt.Height,
                                    Weight = dt.Weight,
                                    HealthStatus = dt.HealthStatus,
                                    Email = dt.Email.ToTrim(),
                                    Status = true,
                                    CreatedDate = dt.CreatedDate,
                                    CreatedBy = dt.CreatedBy,
                                    NguoiThem = us.FullName
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
                                    join jb in db.Jobs on dt.JobId equals jb.Id
                                    where us.NguoiQuanLy == Id || us.UserId == Id
                                    orderby dt.CreatedDate descending
                                    select new CustomerViewModel
                                    {
                                        Id = dt.Id,
                                        CustomerName = dt.CustomerName,
                                        CustomerCode = dt.CustomerCode.ToTrim(),
                                        Address = dt.Address,
                                        DoB = dt.DoB,
                                        JobId = dt.JobId,
                                        JobName = jb.JobName,
                                        NumberPhone = dt.NumberPhone.ToTrim(),
                                        Note = dt.Note,
                                        Height = dt.Height,
                                        Weight = dt.Weight,
                                        HealthStatus = dt.HealthStatus,
                                        Email = dt.Email.ToTrim(),
                                        Status = true,
                                        CreatedDate = dt.CreatedDate,
                                        CreatedBy = dt.CreatedBy,
                                        NguoiThem = us.FullName
                                    };
                        }
                        else
                        {
                            query = from dt in db.Customers
                                    join us in db.Users on dt.CreatedBy equals us.UserId
                                    join jb in db.Jobs on dt.JobId equals jb.Id
                                    where dt.CreatedBy == Id
                                    orderby dt.CreatedDate descending
                                    select new CustomerViewModel
                                    {
                                        Id = dt.Id,
                                        CustomerName = dt.CustomerName,
                                        CustomerCode = dt.CustomerCode.ToTrim(),
                                        Address = dt.Address,
                                        DoB = dt.DoB,
                                        JobId = dt.JobId,
                                        JobName =  jb.JobName,
                                        NumberPhone = dt.NumberPhone.ToTrim(),
                                        Note = dt.Note,
                                        Height = dt.Height,
                                        Weight = dt.Weight,
                                        HealthStatus = dt.HealthStatus,
                                        Email = dt.Email.ToTrim(),
                                        Status = true,
                                        CreatedDate = dt.CreatedDate,
                                        CreatedBy = dt.CreatedBy,
                                        NguoiThem = us.FullName
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
                                        join jb in db.Jobs on dt.JobId equals jb.Id
                                        where us.NguoiQuanLy == Id || us.UserId == Id
                                        orderby dt.CreatedDate descending
                                        select new CustomerViewModel
                                        {
                                            Id = dt.Id,
                                            CustomerName = dt.CustomerName,
                                            CustomerCode = dt.CustomerCode.ToTrim(),
                                            Address = dt.Address,
                                            DoB = dt.DoB,
                                            JobId = dt.JobId,
                                            JobName = jb.JobName,
                                            NumberPhone = dt.NumberPhone.ToTrim(),
                                            Note = dt.Note,
                                            Height = dt.Height,
                                            Weight = dt.Weight,
                                            HealthStatus = dt.HealthStatus,
                                            Email = dt.Email.ToTrim(),
                                            Status = true,
                                            CreatedDate = dt.CreatedDate,
                                            CreatedBy = dt.CreatedBy,
                                            NguoiThem = us.FullName
                                        };
                            }
                            else
                            {
                                query = from dt in db.Customers
                                        join us in db.Users on dt.CreatedBy equals us.UserId
                                        join jb in db.Jobs on dt.JobId equals jb.Id
                                        where dt.CreatedBy == Id
                                        orderby dt.CreatedDate descending
                                        select new CustomerViewModel
                                        {
                                            Id = dt.Id,
                                            CustomerName = dt.CustomerName,
                                            CustomerCode = dt.CustomerCode.ToTrim(),
                                            Address = dt.Address,
                                            DoB = dt.DoB,
                                            JobId = dt.JobId,
                                            JobName = jb.JobName,
                                            NumberPhone = dt.NumberPhone.ToTrim(),
                                            Note = dt.Note,
                                            Height = dt.Height,
                                            Weight = dt.Weight,
                                            HealthStatus = dt.HealthStatus,
                                            Email = dt.Email.ToTrim(),
                                            Status = true,
                                            CreatedDate = dt.CreatedDate,
                                            CreatedBy = dt.CreatedBy,
                                            NguoiThem = us.FullName
                                        };
                            }
                        }
                        else
                        {
                            query = from dt in db.Customers
                                    join us in db.Users on dt.CreatedBy equals us.UserId
                                    join jb in db.Jobs on dt.JobId equals jb.Id
                                    where dt.CreatedBy == Id
                                    orderby dt.CreatedDate descending
                                    select new CustomerViewModel
                                    {
                                        Id = dt.Id,
                                        CustomerName = dt.CustomerName,
                                        CustomerCode = dt.CustomerCode.ToTrim(),
                                        Address = dt.Address,
                                        DoB = dt.DoB,
                                        JobId = dt.JobId,
                                        JobName = jb.JobName,
                                        NumberPhone = dt.NumberPhone.ToTrim(),
                                        Note = dt.Note,
                                        Height = dt.Height,
                                        Weight = dt.Weight,
                                        HealthStatus = dt.HealthStatus,
                                        Email = dt.Email.ToTrim(),
                                        Status = true,
                                        CreatedDate = dt.CreatedDate,
                                        CreatedBy = dt.CreatedBy,
                                        NguoiThem = us.FullName,
                                        DoBName = dt.DoB.HasValue ? dt.DoB.Value.ToString("dd/MM/yyyy") : "",
                                        CreateDateName = dt.CreatedDate.HasValue ? dt.CreatedDate.Value.ToString("dd/MM/yyyy") : "",
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
                    if (pagingParams.SortKey == "CustomerCode" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.CustomerCode);
                    }
                    if (pagingParams.SortKey == "CustomerCode" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.CustomerCode);
                    }
                    if (pagingParams.SortKey == "CustomerName" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.CustomerName);
                    }
                    if (pagingParams.SortKey == "CustomerName" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.CustomerName);
                    }
                    if (pagingParams.SortKey == "Address" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.Address);
                    }
                    if (pagingParams.SortKey == "Address" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.Address);
                    }
                    if (pagingParams.SortKey == "DoB" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.DoB);
                    }
                    if (pagingParams.SortKey == "DoB" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.DoB);
                    }
                    if (pagingParams.SortKey == "JobName" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.JobName);
                    }
                    if (pagingParams.SortKey == "JobName" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.JobName);
                    }
                    if (pagingParams.SortKey == "NumberPhone" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.NumberPhone);
                    }
                    if (pagingParams.SortKey == "NumberPhone" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.NumberPhone);
                    }
                    if (pagingParams.SortKey == "Note" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.Note);
                    }
                    if (pagingParams.SortKey == "Note" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.Note);
                    }
                    if (pagingParams.SortKey == "Height" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.Height);
                    }
                    if (pagingParams.SortKey == "Height" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.Height);
                    }
                    if (pagingParams.SortKey == "Weight" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.Weight);
                    }
                    if (pagingParams.SortKey == "Weight" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.Weight);
                    }
                    if (pagingParams.SortKey == "HealthStatus" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.HealthStatus);
                    }
                    if (pagingParams.SortKey == "HealthStatus" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.HealthStatus);
                    }
                    if (pagingParams.SortKey == "Email" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.Email);
                    }
                    if (pagingParams.SortKey == "Email" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.Email);
                    }
                    if (pagingParams.SortKey == "Status" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.Status);
                    }
                    if (pagingParams.SortKey == "Status" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.Status);
                    }
                    if (pagingParams.SortKey == "CreatedDate" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.CreatedDate);
                    }
                    if (pagingParams.SortKey == "CreatedDate" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.CreatedDate);
                    }
                    if (pagingParams.SortKey == "CreatedBy" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.CreatedBy);
                    }
                    if (pagingParams.SortKey == "CreatedBy" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.CreatedBy);
                    }
                    if (pagingParams.SortKey == "NguuoiThem" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.CreatedBy);
                    }
                    if (pagingParams.SortKey == "NguuoiThem" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.CreatedBy);
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
                        join jb in db.Jobs on dt.JobId equals jb.Id
                        where dt.Id == Id
                        select new CustomerViewModel
                        {
                            Id = dt.Id,
                            CustomerName = dt.CustomerName,
                            CustomerCode = dt.CustomerCode.ToTrim(),
                            Address = dt.Address,
                            DoB = dt.DoB,
                            JobId = dt.JobId,
                            JobName = jb.JobName,
                            NumberPhone = dt.NumberPhone.ToTrim(),
                            Note = dt.Note,
                            Height = dt.Height,
                            Weight = dt.Weight,
                            HealthStatus = dt.HealthStatus,
                            Email = dt.Email.ToTrim(),
                            Status = true,
                            CreatedDate = dt.CreatedDate,
                            CreatedBy = dt.CreatedBy
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
                                join jb in db.Jobs on dt.JobId equals jb.Id
                                where dt.CreatedBy == Id
                                select new CustomerViewModel
                                {
                                    Id = dt.Id,
                                    CustomerName = dt.CustomerName,
                                    CustomerCode = dt.CustomerCode.ToTrim(),
                                    Address = dt.Address,
                                    DoB = dt.DoB,
                                    JobId = dt.JobId,
                                    JobName = jb.JobName,
                                    NumberPhone = dt.NumberPhone.ToTrim(),
                                    Note = dt.Note,
                                    Height = dt.Height,
                                    Weight = dt.Weight,
                                    HealthStatus = dt.HealthStatus,
                                    Email = dt.Email.ToTrim(),
                                    Status = true,
                                    CreatedDate = dt.CreatedDate,
                                    CreatedBy = dt.CreatedBy
                                };
                    return await query.ToListAsync();
                }
                else
                {
                    var query = from dt in db.Customers
                                join jb in db.Jobs on dt.JobId equals jb.Id
                                select new CustomerViewModel
                                {
                                    Id = dt.Id,
                                    CustomerName = dt.CustomerName,
                                    CustomerCode = dt.CustomerCode.ToTrim(),
                                    Address = dt.Address,
                                    DoB = dt.DoB,
                                    JobId = dt.JobId,
                                    JobName = jb.JobName,
                                    NumberPhone = dt.NumberPhone.ToTrim(),
                                    Note = dt.Note,
                                    Height = dt.Height,
                                    Weight = dt.Weight,
                                    HealthStatus = dt.HealthStatus,
                                    Email = dt.Email.ToTrim(),
                                    Status = true,
                                    CreatedDate = dt.CreatedDate,
                                    CreatedBy = dt.CreatedBy
                                };
                    return await query.ToListAsync();
                }


            }
            else
                return null;
        }
        public async Task<bool> CheckTrungMa(string MaKH)
        {
            var rs = await db.Customers.FirstOrDefaultAsync(x => x.CustomerCode.ToString().ToUpper().ToTrim() == (MaKH.ToString().ToUpper().ToTrim()));
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

            var query = from dt in db.Customers
                        join us in db.Users on dt.CreatedBy equals us.UserId
                        join jb in db.Jobs on dt.JobId equals jb.Id
                        orderby dt.CreatedDate descending
                        select new CustomerViewModel
                        {
                            CustomerName = dt.CustomerName,
                            CustomerCode = dt.CustomerCode.ToTrim(),
                            Address = dt.Address,
                            DoB = dt.DoB,
                            JobId = dt.JobId,
                            JobName = jb.JobName,
                            NumberPhone = dt.NumberPhone.ToTrim(),
                            Note = dt.Note,
                            Height = dt.Height,
                            Weight = dt.Weight,
                            HealthStatus = dt.HealthStatus,
                            Email = dt.Email.ToTrim(),
                            Status = true,
                            CreatedDate = dt.CreatedDate,
                            CreatedBy = dt.CreatedBy,
                            DoBName = dt.DoB.HasValue ? dt.DoB.Value.ToString("dd/MM/yyyy") : "",
                            CreateDateName = dt.CreatedDate.HasValue ? dt.CreatedDate.Value.ToString("dd/MM/yyyy") : "",
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
                                        (x.CustomerName ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.CustomerName ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.CustomerCode ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.CustomerCode ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.Address ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.Address ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.Job ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.Job ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.NumberPhone ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.NumberPhone ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.Email ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.Email ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.Note ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.Note ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.Height.Value.ToString()).ToUpper().ToUnSign().Contains(keyword.ToUnSign())||
                                        (x.Height.Value.ToString()).ToUpper().Contains(keyword) ||
                                        (x.Weight.Value.ToString()).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.Weight.Value.ToString()).ToUpper().Contains(keyword) ||
                                        (x.HealthStatus ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.HealthStatus ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.Email ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.Email ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.CreatedBy ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.CreatedBy ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.DoBName ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.DoBName ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.CreateDateName ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.CreateDateName ?? string.Empty).ToUpper().Contains(keyword)
                                        //(x.CreatedDate) == DateTime.Parse(keyword)
                                        );

            }

            if (!string.IsNullOrEmpty(pagingParams.KeywordCol))
            {
                var keyword = pagingParams.KeywordCol.ToUpper().ToTrim();


                if (pagingParams.ColName == "customerName")
                {
                    query = query.Where(x => (x.CustomerName ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.CustomerName ?? string.Empty).ToUpper().Contains(keyword));
                }
                if (pagingParams.ColName == "customerCode")
                {
                    query = query.Where(x => (x.CustomerCode ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.CustomerCode ?? string.Empty).ToUpper().Contains(keyword));
                }
                if (pagingParams.ColName == "address")
                {
                    query = query.Where(x => (x.Address ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.Address ?? string.Empty).ToUpper().Contains(keyword));
                }
                if (pagingParams.ColName == "job")
                {
                    query = query.Where(x => (x.Job ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.Job ?? string.Empty).ToUpper().Contains(keyword));
                }
                if (pagingParams.ColName == "numberPhone")
                {
                    query = query.Where(x => (x.NumberPhone ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.NumberPhone ?? string.Empty).ToUpper().Contains(keyword));
                }
                if (pagingParams.ColName == "email")
                {
                    query = query.Where(x => (x.Email ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.Email ?? string.Empty).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "note")
                {
                    query = query.Where(x => (x.Note ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.Note ?? string.Empty).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "height")
                {
                    query = query.Where(x => (x.Height.Value.ToString()).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.Height.Value.ToString()).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "weight")
                {
                    query = query.Where(x => (x.Weight.Value.ToString()).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.Weight.Value.ToString()).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "healthStatus")
                {
                    query = query.Where(x => (x.HealthStatus ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.HealthStatus ?? string.Empty).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "createdBy")
                {
                    query = query.Where(x => (x.CreatedBy ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.CreatedBy ?? string.Empty).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "doBName")
                {
                    query = query.Where(x => (x.DoBName ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.DoBName ?? string.Empty).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "createDateName")
                {
                    query = query.Where(x => (x.CreateDateName ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.CreateDateName ?? string.Empty).ToUpper().Contains(keyword));

                }

            }

            if (!string.IsNullOrEmpty(pagingParams.SortKey))
            {
                if (pagingParams.SortKey == "customerName" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.CustomerName);
                }
                if (pagingParams.SortKey == "customerName" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.CustomerName);
                }
                if (pagingParams.SortKey == "customerCode" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.CustomerCode);
                }
                if (pagingParams.SortKey == "customerCode" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.CustomerCode);
                }

                if (pagingParams.SortKey == "address" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.Address);
                }
                if (pagingParams.SortKey == "address" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.Address);
                }

                if (pagingParams.SortKey == "numberPhone" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.NumberPhone);
                }
                if (pagingParams.SortKey == "numberPhone" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.NumberPhone);
                }

                if (pagingParams.SortKey == "email" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.Email);
                }
                if (pagingParams.SortKey == "email" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.Email);
                }
                if (pagingParams.SortKey == "note" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.Note);
                }
                if (pagingParams.SortKey == "note" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.Note);
                }
                if (pagingParams.SortKey == "height" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.Height);
                }
                if (pagingParams.SortKey == "height" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.Height);
                }
                if (pagingParams.SortKey == "weight" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.Weight);
                }
                if (pagingParams.SortKey == "weight" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.Weight);
                }
                if (pagingParams.SortKey == "healthStatus" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.HealthStatus);
                }
                if (pagingParams.SortKey == "healthStatus" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.HealthStatus);
                }

                if (pagingParams.SortKey == "doBName" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.DoBName);
                }
                if (pagingParams.SortKey == "doBName" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.DoBName);
                }

                if (pagingParams.SortKey == "createDateName" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.CreateDateName);
                }
                if (pagingParams.SortKey == "createDateName" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.CreateDateName);
                }
                if (pagingParams.SortKey == "createdBy" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.CreatedBy);
                }
                if (pagingParams.SortKey == "createdBy" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.CreatedBy);
                }
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

                        worksheet.Cells[idx, 2].Value = it.CustomerCode;
                        worksheet.Cells[idx, 3].Value = it.CustomerName;
                        worksheet.Cells[idx, 4].Value = it.DoBName;
                        worksheet.Cells[idx, 5].Value = it.Address;
                        worksheet.Cells[idx, 6].Value = it.NumberPhone;
                        worksheet.Cells[idx, 7].Value = it.Email;
                        worksheet.Cells[idx, 8].Value = it.Job;
                        worksheet.Cells[idx, 9].Value = it.Note;
                        worksheet.Cells[idx, 10].Value = it.Height;
                        worksheet.Cells[idx, 11].Value = it.Weight;
                        worksheet.Cells[idx, 12].Value = it.HealthStatus;
                        worksheet.Cells[idx, 13].Value = it.CreatedBy;
                        worksheet.Cells[idx, 14].Value = it.CreateDateName;
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

        async Task<List<CustomerViewModel>> ICustomerRespositories.GetAllKH()
        {
            var entity = await db.Customers.ToListAsync();
            return mp.Map<List<CustomerViewModel>>(entity);
        }
    }
}