using AutoMapper;
using DLL;
using DLL.Entity;
using ManagementServices.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services.Repositories.Interfaces;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using OfficeOpenXml;

namespace Services.Repositories.Implimentations
{
    public class KhachHangLogRespositories : IKhachHangLogRespositories
    {
        Datacontext db;
        IMapper mp;
        private readonly IHostingEnvironment _hostingEnvironment;
        IHttpContextAccessor _IHttpContextAccessor;
        IConfiguration _IConfiguration;
        public KhachHangLogRespositories(Datacontext datacontext, IMapper mapper, IHostingEnvironment IHostingEnvironment, IHttpContextAccessor IHttpContextAccessor, IConfiguration IConfiguration)
        {
            this.db = datacontext;
            this.mp = mapper;
            _hostingEnvironment = IHostingEnvironment;
            _IHttpContextAccessor = IHttpContextAccessor;
            _IConfiguration = IConfiguration;
        }

        public async Task<int> Insert(KhachHangLogViewModels model)
        {
            model.Id = Guid.NewGuid().ToString();
            var entity = mp.Map<KhachHangLog>(model);
            await db.KhachHangLogs.AddAsync(entity);
            var rs = await db.SaveChangesAsync();
            // thanh cong 1, o loi
            return rs;
        }
        public async Task<PagedList<KhachHangLogViewModels>> GetAllPagingAsync(PagingParams pagingParams, string selectedId)
        {
            IQueryable<KhachHangLogViewModels> query = from dt in db.KhachHangLogs
                                                       select new KhachHangLogViewModels();
            if (selectedId == null || selectedId == "")
            {
                query = from dt in db.KhachHangLogs
                        join kh in db.Customers on dt.CustomerId equals kh.Id
                        join ur in db.Users on dt.CreatedBy equals ur.UserId
                        orderby dt.CreatedDate descending
                        select new KhachHangLogViewModels
                        {
                            Id = dt.Id,
                            CustomerCode = kh.CustomerCode,
                            CreatedBy = ur.FullName,
                            CustomerName = kh.CustomerName,
                            TenTruongThayDoi = dt.TenTruongThayDoi,
                            DienGiai = dt.DienGiai,
                            DuLieuMoi = dt.DuLieuMoi,
                            DuLieuCu = dt.DuLieuCu,
                            CreatedDate = dt.CreatedDate,
                            CustomerId = dt.CustomerId
                        };
            }
            else
            {
                query = from dt in db.KhachHangLogs
                        where dt.CreatedBy == selectedId
                        join kh in db.Customers on dt.CustomerId equals kh.Id
                        join ur in db.Users on dt.CreatedBy equals ur.UserId
                        orderby dt.CreatedDate descending
                        select new KhachHangLogViewModels
                        {
                            Id = dt.Id,
                            CustomerCode = kh.CustomerCode,
                            CreatedBy = ur.FullName,
                            CustomerName = kh.CustomerName,
                            TenTruongThayDoi = dt.TenTruongThayDoi,
                            DienGiai = dt.DienGiai,
                            DuLieuMoi = dt.DuLieuMoi,
                            DuLieuCu = dt.DuLieuCu,
                            CreatedDate = dt.CreatedDate,
                            CustomerId = dt.CustomerId
                        };
            }

            if (!string.IsNullOrEmpty(pagingParams.fromDate) && !string.IsNullOrEmpty(pagingParams.toDate))
            {
                DateTime fromDate = DateTime.Parse(pagingParams.fromDate);
                DateTime toDate = DateTime.Parse(pagingParams.toDate);
                query = query.Where(x => (x.CreatedDate) >= fromDate.Date && (x.CreatedDate) < toDate.Date.AddDays(1));
            }
            if (!string.IsNullOrEmpty(pagingParams.userId))
            {
                User currentUser = await db.Users
                .AsNoTracking()
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.UserId == pagingParams.userId);
            }
            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                var keyword = pagingParams.Keyword.ToUpper().ToTrim();

                query = query.Where(x =>
                                        (x.CustomerCode ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.CustomerCode ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.CustomerName ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.CustomerName ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.TaxCode ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.TaxCode ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.TenTruongThayDoi ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.TenTruongThayDoi ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.DienGiai ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.DienGiai ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.DuLieuMoi ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.DuLieuMoi ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.DuLieuCu ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.DuLieuCu ?? string.Empty).ToUpper().Contains(keyword));
            }
            if (!string.IsNullOrEmpty(pagingParams.KeywordCol))
            {
                var keyword = pagingParams.KeywordCol.ToUpper().ToTrim();

                if (pagingParams.ColName == "CustomerCode")
                {
                    query = query.Where(x => (x.CustomerCode ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                    (x.CustomerCode ?? string.Empty).ToUpper().Contains(keyword));
                }
                if (pagingParams.ColName == "CustomerName")
                {
                    query = query.Where(x => (x.CustomerName ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                    (x.CustomerName ?? string.Empty).ToUpper().Contains(keyword));
                }
                if (pagingParams.ColName == "TaxCode")
                {
                    query = query.Where(x => (x.TaxCode ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                    (x.TaxCode ?? string.Empty).ToUpper().Contains(keyword));
                }
                if (pagingParams.ColName == "TenTruongThayDoi")
                {
                    query = query.Where(x => (x.TenTruongThayDoi ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                    (x.TenTruongThayDoi ?? string.Empty).ToUpper().Contains(keyword));
                }
                if (pagingParams.ColName == "DienGiai")
                {
                    query = query.Where(x => (x.DienGiai ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                    (x.DienGiai ?? string.Empty).ToUpper().Contains(keyword));
                }
                if (pagingParams.ColName == "DuLieuMoi")
                {
                    query = query.Where(x => (x.DuLieuMoi ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                    (x.DuLieuMoi ?? string.Empty).ToUpper().Contains(keyword));
                }
                if (pagingParams.ColName == "DuLieuCu")
                {
                    query = query.Where(x => (x.DuLieuCu ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                    (x.DuLieuCu ?? string.Empty).ToUpper().Contains(keyword));
                }
            }
            if (!string.IsNullOrEmpty(pagingParams.SortKey))
            {
                if (pagingParams.SortKey == "CustomerCode" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.CustomerCode);
                }
                if (pagingParams.SortKey == "CustomerName" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.CustomerName);
                }

                if (pagingParams.SortKey == "TaxCode" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.TaxCode);
                }
                if (pagingParams.SortKey == "TenTruongThayDoi" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.TenTruongThayDoi);
                }

                if (pagingParams.SortKey == "DienGiai" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.DienGiai);
                }
                if (pagingParams.SortKey == "DuLieuMoi" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.DuLieuMoi);
                }

                if (pagingParams.SortKey == "DuLieuCu" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.DuLieuCu);
                }
            }
            return await PagedList<KhachHangLogViewModels>.CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }
        public async Task<string> ExportExcelAsync(PagingParams pagingParams, string selectedId)
        {
            User currentUser = await db.Users
                .AsNoTracking()
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.UserId == pagingParams.userId);
            var query = from dt in db.KhachHangLogs
                        join kh in db.Customers on dt.CustomerId equals kh.Id
                        join ur in db.Users on dt.CreatedBy equals ur.UserId
                        orderby dt.CreatedDate descending
                        select new KhachHangLogViewModels
                        {
                            Id = dt.Id,
                            CustomerCode = kh.CustomerCode,
                            CreatedBy = ur.FullName,
                            CustomerName = kh.CustomerName,
                            TenTruongThayDoi = dt.TenTruongThayDoi,
                            DienGiai = dt.DienGiai,
                            DuLieuMoi = dt.DuLieuMoi,
                            DuLieuCu = dt.DuLieuCu,
                            CreatedDate = dt.CreatedDate,
                            CustomerId = dt.CustomerId,
                            NguoiThem = dt.CreatedBy,
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
                query = query.Where(x => (x.CreatedDate >= fromDate && x.CreatedDate < toDate.Date.AddDays(1)));
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
                        query = query.Where(x => x.NguoiThem == selectedId);
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
               (x.CustomerCode ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
               (x.CustomerCode ?? string.Empty).ToUpper().Contains(keyword) ||
               (x.CustomerName ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
               (x.CustomerName ?? string.Empty).ToUpper().Contains(keyword) ||
               (x.TaxCode ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
               (x.TaxCode ?? string.Empty).ToUpper().Contains(keyword) ||
               (x.TenTruongThayDoi ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
               (x.TenTruongThayDoi ?? string.Empty).ToUpper().Contains(keyword) ||
               (x.DienGiai ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
               (x.DienGiai ?? string.Empty).ToUpper().Contains(keyword) ||
               (x.DuLieuMoi ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
               (x.DuLieuMoi ?? string.Empty).ToUpper().Contains(keyword) ||
               (x.DuLieuCu ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
               (x.DuLieuCu ?? string.Empty).ToUpper().Contains(keyword));
            }
            if (!string.IsNullOrEmpty(pagingParams.KeywordCol))
            {
                var keyword = pagingParams.KeywordCol.ToUpper().ToTrim();

                if (pagingParams.ColName == "CustomerCode")
                {
                    query = query.Where(x => (x.CustomerCode ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                    (x.CustomerCode ?? string.Empty).ToUpper().Contains(keyword));
                }
                if (pagingParams.ColName == "CustomerName")
                {
                    query = query.Where(x => (x.CustomerName ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                    (x.CustomerName ?? string.Empty).ToUpper().Contains(keyword));
                }
                if (pagingParams.ColName == "TaxCode")
                {
                    query = query.Where(x => (x.TaxCode ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                    (x.TaxCode ?? string.Empty).ToUpper().Contains(keyword));
                }
                if (pagingParams.ColName == "TenTruongThayDoi")
                {
                    query = query.Where(x => (x.TenTruongThayDoi ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                    (x.TenTruongThayDoi ?? string.Empty).ToUpper().Contains(keyword));
                }
                if (pagingParams.ColName == "DienGiai")
                {
                    query = query.Where(x => (x.DienGiai ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                    (x.DienGiai ?? string.Empty).ToUpper().Contains(keyword));
                }
                if (pagingParams.ColName == "DuLieuMoi")
                {
                    query = query.Where(x => (x.DuLieuMoi ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                    (x.DuLieuMoi ?? string.Empty).ToUpper().Contains(keyword));
                }
                if (pagingParams.ColName == "DuLieuCu")
                {
                    query = query.Where(x => (x.DuLieuCu ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                    (x.DuLieuCu ?? string.Empty).ToUpper().Contains(keyword));
                }
            }
            if (!string.IsNullOrEmpty(pagingParams.SortKey))
            {
                if (pagingParams.SortKey == "CustomerCode" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.CustomerCode);
                }
                if (pagingParams.SortKey == "CustomerName" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.CustomerName);
                }

                if (pagingParams.SortKey == "TaxCode" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.TaxCode);
                }
                if (pagingParams.SortKey == "TenTruongThayDoi" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.TenTruongThayDoi);
                }

                if (pagingParams.SortKey == "DienGiai" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.DienGiai);
                }
                if (pagingParams.SortKey == "DuLieuMoi" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.DuLieuMoi);
                }

                if (pagingParams.SortKey == "DuLieuCu" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.DuLieuCu);
                }
            }

            string _path_sample = Path.Combine(_hostingEnvironment.ContentRootPath, $"MyAssets/uploaded/sample/luu_lich_su_khach_hang.xlsx");
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
                    List<KhachHangLogViewModels> list = query.ToList();
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

                        worksheet.Cells[idx, 2].Value = it.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm");
                        worksheet.Cells[idx, 3].Value = it.CreatedBy;
                        worksheet.Cells[idx, 4].Value = it.CustomerCode;
                        worksheet.Cells[idx, 5].Value = it.CustomerName;
                        worksheet.Cells[idx, 6].Value = it.TenTruongThayDoi;
                        worksheet.Cells[idx, 7].Value = it.DienGiai;
                        worksheet.Cells[idx, 8].Value = it.DuLieuCu;
                        worksheet.Cells[idx, 9].Value = it.DuLieuMoi;


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
                    Console.WriteLine(e);
                    throw;
                }

            }
        }
        public Task<int> Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(KhachHangLogViewModels model)
        {
            throw new NotImplementedException();
        }

        public Task<List<KhachHangLogViewModels>> GetAllLichSuKh()
        {
            throw new NotImplementedException();
        }
    }
}
