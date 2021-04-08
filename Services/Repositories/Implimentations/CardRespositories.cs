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
    public class CardRespositories : ICardRespositories
    {
        Datacontext db;
        IMapper mp;
        private readonly IHostingEnvironment _hostingEnvironment;
        IHttpContextAccessor _IHttpContextAccessor;
        IConfiguration _IConfiguration;
        public CardRespositories(Datacontext datacontext, IMapper mapper, IHostingEnvironment IHostingEnvironment, IHttpContextAccessor IHttpContextAccessor, IConfiguration IConfiguration)
        {
            this.db = datacontext;
            this.mp = mapper;
            _hostingEnvironment = IHostingEnvironment;
            _IHttpContextAccessor = IHttpContextAccessor;
            _IConfiguration = IConfiguration;
        }
        public async Task<int> Delete(Guid Id)
        {
            var entity = await db.Cards.FirstOrDefaultAsync(x => x.Id == Id.ToString());
            db.Cards.Remove(entity);
            var rs = await db.SaveChangesAsync();
            return rs;
        }

        public async Task<List<CardViewModel>> GetAll()
        {
            var query = from cr in db.Cards
                        join ct in db.Customers on cr.CustomerId equals ct.Id
                        join fc in db.Facilities on cr.FacilityId equals fc.Id
                        join cp in db.CardTypes on cr.CardTypeId equals cp.Id
                        join sv in db.Services on cr.ServiceId equals sv.Id
                        select new CardViewModel
                        {
                            Id = cr.Id,
                            CardCode = cr.CardCode,
                            CustomerCode = ct.CustomerCode,
                            CustomerName = ct.CustomerName,
                            Address = ct.Address,
                            NumberPhone = ct.NumberPhone,
                            NameType = cp.NameType,
                            FacilityName = fc.FacilityName,
                            ServiceName = sv.ServiceName,
                            Money = sv.Money,
                            Price = cr.Price,

                        };
            return await query.OrderBy(x => x.CardCode).ToListAsync();
        }

        public Task<CardViewModel> GetProductById(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Insert(int TN, CardViewModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            model.CardCode = model.CardCode;
            model.CardTypeId = model.CardTypeId;
            model.CustomerId = model.CustomerId;
            model.FacilityId = model.FacilityId;
            model.ServiceId = model.ServiceId;
            model.Price = model.Price;
            model.Note = model.Note;     
            model.FromDate = model.FromDate;
            model.ToDate = Convert.ToDateTime(model.FromDate).Date.AddDays(30);
            model.CreatedDate = DateTime.Now;
            model.CreatedBy = model.CreatedBy;
            var entity = mp.Map<Card>(model);
            await db.Cards.AddAsync(entity);
            var rs = await db.SaveChangesAsync();
            // thanh cong 1, o loi
            return rs;
        }

        public async Task<int> Update(int TN, CardViewModel model)
        {
            var eq = await db.Cards.AsNoTracking().FirstOrDefaultAsync(x => x.Id == model.Id);
            eq.CardCode = model.CardCode.ToTrim();
            eq.CardTypeId = model.CardTypeId;
            eq.CustomerId = model.CustomerId;
            eq.FacilityId = model.FacilityId;
            eq.ServiceId = model.ServiceId;
            eq.UserId = model.UserId;
            // eq.Note = model.Note;
            eq.ToDate = model.ToDate;
            eq.FromDate = model.FromDate;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = model.CreatedBy;
            db.Cards.Update(eq);
            var rs = await db.SaveChangesAsync();
            return rs; // 1 thanh cong, 0 that bai
        }
        public async Task<PagedList<CardViewModel>> GetAllPagingAsync(PagingParams pagingParams, string Id, string selectedId)
        {
            IQueryable<CardViewModel> query = from dt in db.Cards
                                              select new CardViewModel();
            var tg = new User();
            tg = await db.Users.FindAsync(Id);
            if (tg != null)
            {
                if (tg.RoleId == "BLD" || tg.RoleId == "ADMIN")
                {
                    if (selectedId == null || selectedId == "")
                    {
                        query = from cr in db.Cards
                                join us in db.Users on cr.CreatedBy equals us.UserId
                                join ct in db.Customers on cr.CustomerId equals ct.Id
                                join fc in db.Facilities on cr.FacilityId equals fc.Id
                                join cp in db.CardTypes on cr.CardTypeId equals cp.Id
                                join sv in db.Services on cr.ServiceId equals sv.Id
                                select new CardViewModel
                                {
                                    Id = cr.Id,
                                    CardCode = cr.CardCode,
                                    CardTypeId=cr.CardTypeId,
                                    CustomerId=cr.CustomerId,
                                    CustomerCode = ct.CustomerCode,
                                    CustomerName = ct.CustomerName,
                                    Address = ct.Address,
                                    NumberPhone = ct.NumberPhone,
                                    NameType = cp.NameType,
                                    FacilityName = fc.FacilityName,
                                    FacilityId=cr.FacilityId,
                                    ServiceName = sv.ServiceName,
                                    ServiceId = cr.ServiceId,
                                    Money = sv.Money,
                                    Price = cr.Price,
                                    ToDate = cr.ToDate,
                                    FromDate = cr.FromDate,
                                    CreatedBy=cr.CreatedBy,
                                    ToDateName = cr.ToDate.HasValue ? cr.ToDate.Value.ToString("dd/MM/yyyy") : "",
                                    FromDateName = cr.FromDate.HasValue ? cr.FromDate.Value.ToString("dd/MM/yyyy") : "",
                                    NguoiThem = us.FullName,
                                    CreateDateName = cr.CreatedDate.HasValue ? cr.CreatedDate.Value.ToString("dd/MM/yyyy") : ""
                                };
                    }
                    else
                    {
                        query = from cr in db.Cards
                                join ct in db.Customers on cr.CustomerId equals ct.Id
                                join us in db.Users on cr.CreatedBy equals us.UserId
                                join fc in db.Facilities on cr.FacilityId equals fc.Id
                                join cp in db.CardTypes on cr.CardTypeId equals cp.Id
                                join sv in db.Services on cr.ServiceId equals sv.Id
                                select new CardViewModel
                                {
                                    Id = cr.Id,
                                    CardCode = cr.CardCode,
                                    CardTypeId = cr.CardTypeId,
                                    CustomerId = cr.CustomerId,
                                    CustomerCode = ct.CustomerCode,
                                    CustomerName = ct.CustomerName,
                                    Address = ct.Address,
                                    NumberPhone = ct.NumberPhone,
                                    NameType = cp.NameType,
                                    FacilityName = fc.FacilityName,
                                    FacilityId = cr.FacilityId,
                                    ServiceName = sv.ServiceName,
                                    ServiceId = cr.ServiceId,
                                    Money = sv.Money,
                                    Price = cr.Price,
                                    ToDate = cr.ToDate,
                                    FromDate = cr.FromDate,
                                    CreatedBy = cr.CreatedBy,
                                    ToDateName = cr.ToDate.HasValue ? cr.ToDate.Value.ToString("dd/MM/yyyy") : "",
                                    FromDateName = cr.FromDate.HasValue ? cr.FromDate.Value.ToString("dd/MM/yyyy") : "",
                                    NguoiThem = us.FullName,
                                    CreateDateName = cr.CreatedDate.HasValue ? cr.CreatedDate.Value.ToString("dd/MM/yyyy") : ""
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

                            query = from cr in db.Cards
                                    join ct in db.Customers on cr.CustomerId equals ct.Id
                                    join us in db.Users on cr.CreatedBy equals us.UserId
                                    join fc in db.Facilities on cr.FacilityId equals fc.Id
                                    join cp in db.CardTypes on cr.CardTypeId equals cp.Id
                                    join sv in db.Services on cr.ServiceId equals sv.Id
                                    select new CardViewModel
                                    {
                                        Id = cr.Id,
                                        CardCode = cr.CardCode,
                                        CardTypeId = cr.CardTypeId,
                                        CustomerId = cr.CustomerId,
                                        CustomerCode = ct.CustomerCode,
                                        CustomerName = ct.CustomerName,
                                        Address = ct.Address,
                                        NumberPhone = ct.NumberPhone,
                                        NameType = cp.NameType,
                                        FacilityName = fc.FacilityName,
                                        FacilityId = cr.FacilityId,
                                        ServiceName = sv.ServiceName,
                                        ServiceId = cr.ServiceId,
                                        Money = sv.Money,
                                        Price = cr.Price,
                                        ToDate = cr.ToDate,
                                        FromDate = cr.FromDate,
                                        CreatedBy = cr.CreatedBy,
                                        ToDateName = cr.ToDate.HasValue ? cr.ToDate.Value.ToString("dd/MM/yyyy") : "",
                                        FromDateName = cr.FromDate.HasValue ? cr.FromDate.Value.ToString("dd/MM/yyyy") : "",
                                        NguoiThem = us.FullName,
                                        CreateDateName = cr.CreatedDate.HasValue ? cr.CreatedDate.Value.ToString("dd/MM/yyyy") : ""
                                    };
                        }
                        else
                        {
                            query = from cr in db.Cards
                                    join ct in db.Customers on cr.CustomerId equals ct.Id
                                    join us in db.Users on cr.CreatedBy equals us.UserId
                                    join fc in db.Facilities on cr.FacilityId equals fc.Id
                                    join cp in db.CardTypes on cr.CardTypeId equals cp.Id
                                    join sv in db.Services on cr.ServiceId equals sv.Id
                                    select new CardViewModel
                                    {
                                        Id = cr.Id,
                                        CardCode = cr.CardCode,
                                        CardTypeId = cr.CardTypeId,
                                        CustomerId = cr.CustomerId,
                                        CustomerCode = ct.CustomerCode,
                                        CustomerName = ct.CustomerName,
                                        Address = ct.Address,
                                        NumberPhone = ct.NumberPhone,
                                        NameType = cp.NameType,
                                        FacilityName = fc.FacilityName,
                                        FacilityId = cr.FacilityId,
                                        ServiceName = sv.ServiceName,
                                        ServiceId = cr.ServiceId,
                                        Money = sv.Money,
                                        Price = cr.Price,
                                        ToDate = cr.ToDate,
                                        FromDate = cr.FromDate,
                                        CreatedBy = cr.CreatedBy,
                                        ToDateName = cr.ToDate.HasValue ? cr.ToDate.Value.ToString("dd/MM/yyyy") : "",
                                        FromDateName = cr.FromDate.HasValue ? cr.FromDate.Value.ToString("dd/MM/yyyy") : "",
                                        NguoiThem = us.FullName,
                                        CreateDateName = cr.CreatedDate.HasValue ? cr.CreatedDate.Value.ToString("dd/MM/yyyy") : ""
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
                                query = from cr in db.Cards
                                        join ct in db.Customers on cr.CustomerId equals ct.Id
                                        join us in db.Users on cr.CreatedBy equals us.UserId
                                        join fc in db.Facilities on cr.FacilityId equals fc.Id
                                        join cp in db.CardTypes on cr.CardTypeId equals cp.Id
                                        join sv in db.Services on cr.ServiceId equals sv.Id
                                        select new CardViewModel
                                        {
                                            Id = cr.Id,
                                            CardCode = cr.CardCode,
                                            CardTypeId = cr.CardTypeId,
                                            CustomerId = cr.CustomerId,
                                            CustomerCode = ct.CustomerCode,
                                            CustomerName = ct.CustomerName,
                                            Address = ct.Address,
                                            NumberPhone = ct.NumberPhone,
                                            NameType = cp.NameType,
                                            FacilityName = fc.FacilityName,
                                            FacilityId = cr.FacilityId,
                                            ServiceName = sv.ServiceName,
                                            ServiceId = cr.ServiceId,
                                            Money = sv.Money,
                                            Price = cr.Price,
                                            ToDate = cr.ToDate,
                                            FromDate = cr.FromDate,
                                            CreatedBy = cr.CreatedBy,
                                            ToDateName = cr.ToDate.HasValue ? cr.ToDate.Value.ToString("dd/MM/yyyy") : "",
                                            FromDateName = cr.FromDate.HasValue ? cr.FromDate.Value.ToString("dd/MM/yyyy") : "",
                                            NguoiThem = us.FullName,
                                            CreateDateName = cr.CreatedDate.HasValue ? cr.CreatedDate.Value.ToString("dd/MM/yyyy") : ""
                                        };
                            }
                            else
                            {
                                query = from cr in db.Cards
                                        join ct in db.Customers on cr.CustomerId equals ct.Id
                                        join us in db.Users on cr.CreatedBy equals us.UserId
                                        join fc in db.Facilities on cr.FacilityId equals fc.Id
                                        join cp in db.CardTypes on cr.CardTypeId equals cp.Id
                                        join sv in db.Services on cr.ServiceId equals sv.Id
                                        select new CardViewModel
                                        {
                                            Id = cr.Id,
                                            CardCode = cr.CardCode,
                                            CardTypeId = cr.CardTypeId,
                                            CustomerId = cr.CustomerId,
                                            CustomerCode = ct.CustomerCode,
                                            CustomerName = ct.CustomerName,
                                            Address = ct.Address,
                                            NumberPhone = ct.NumberPhone,
                                            NameType = cp.NameType,
                                            FacilityName = fc.FacilityName,
                                            FacilityId = cr.FacilityId,
                                            ServiceName = sv.ServiceName,
                                            ServiceId = cr.ServiceId,
                                            Money = sv.Money,
                                            Price = cr.Price,
                                            ToDate = cr.ToDate,
                                            FromDate = cr.FromDate,
                                            CreatedBy = cr.CreatedBy,
                                            ToDateName = cr.ToDate.HasValue ? cr.ToDate.Value.ToString("dd/MM/yyyy") : "",
                                            FromDateName = cr.FromDate.HasValue ? cr.FromDate.Value.ToString("dd/MM/yyyy") : "",
                                            NguoiThem = us.FullName,
                                            CreateDateName = cr.CreatedDate.HasValue ? cr.CreatedDate.Value.ToString("dd/MM/yyyy") : ""
                                        };
                            }
                        }
                        else
                        {
                            query = from cr in db.Cards
                                    join ct in db.Customers on cr.CustomerId equals ct.Id
                                    join us in db.Users on cr.CreatedBy equals us.UserId
                                    join fc in db.Facilities on cr.FacilityId equals fc.Id
                                    join cp in db.CardTypes on cr.CardTypeId equals cp.Id
                                    join sv in db.Services on cr.ServiceId equals sv.Id
                                    select new CardViewModel
                                    {
                                        Id = cr.Id,
                                        CardCode = cr.CardCode,
                                        CardTypeId = cr.CardTypeId,
                                        CustomerId = cr.CustomerId,
                                        CustomerCode = ct.CustomerCode,
                                        CustomerName = ct.CustomerName,
                                        Address = ct.Address,
                                        NumberPhone = ct.NumberPhone,
                                        NameType = cp.NameType,
                                        FacilityName = fc.FacilityName,
                                        FacilityId = cr.FacilityId,
                                        ServiceName = sv.ServiceName,
                                        ServiceId = cr.ServiceId,
                                        Money = sv.Money,
                                        Price = cr.Price,
                                        ToDate = cr.ToDate,
                                        FromDate = cr.FromDate,
                                        CreatedBy = cr.CreatedBy,
                                        ToDateName = cr.ToDate.HasValue ? cr.ToDate.Value.ToString("dd/MM/yyyy") : "",
                                        FromDateName = cr.FromDate.HasValue ? cr.FromDate.Value.ToString("dd/MM/yyyy") : "",
                                        NguoiThem = us.FullName,
                                        CreateDateName = cr.CreatedDate.HasValue ? cr.CreatedDate.Value.ToString("dd/MM/yyyy") : ""
                                    };
                        }

                    }
                }
                //het han the tap k hien
/*                if (!string.IsNullOrEmpty(pagingParams.fromDate) && !string.IsNullOrEmpty(pagingParams.toDate))
                {
                    DateTime fromDate = DateTime.Parse(pagingParams.fromDate);
                    DateTime toDate = DateTime.Parse(pagingParams.toDate);
                    query = query.Where(x => (x.CreatedDate) >= fromDate.Date && (x.CreatedDate) <= toDate.Date.AddDays(1));
                }*/
                if (!string.IsNullOrEmpty(pagingParams.SortKey))
                {
                    if (pagingParams.SortKey == "cardCode" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.CardCode);
                    }
                    if (pagingParams.SortKey == "cardCode" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.CardCode);
                    }
                    if (pagingParams.SortKey == "customerCode" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.CustomerCode);
                    }
                    if (pagingParams.SortKey == "customerCode" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.CustomerCode);
                    }
                    if (pagingParams.SortKey == "customerName" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.CustomerName);
                    }
                    if (pagingParams.SortKey == "customerName" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.CustomerName);
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
                    if (pagingParams.SortKey == "nameType" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.NameType);
                    }
                    if (pagingParams.SortKey == "nameType" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.NameType);
                    }
                    if (pagingParams.SortKey == "facilityName" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.FacilityName);
                    }
                    if (pagingParams.SortKey == "facilityName" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.FacilityName);
                    }
                    if (pagingParams.SortKey == "serviceName" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.ServiceName);
                    }
                    if (pagingParams.SortKey == "serviceName" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.ServiceName);
                    }
                    /*if (pagingParams.SortKey == "price" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.Price);
                    }
                    if (pagingParams.SortKey == "price" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.Price);
                    }*/
                    if (pagingParams.SortKey == "toDateName" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.ToDateName);
                    }
                    if (pagingParams.SortKey == "toDateName" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.ToDateName);
                    }
                    if (pagingParams.SortKey == "fromDateName" && pagingParams.SortValue == "ascend")
                    {
                        query = query.OrderBy(x => x.FromDateName);
                    }
                    if (pagingParams.SortKey == "fromDateName" && pagingParams.SortValue == "descend")
                    {
                        query = query.OrderByDescending(x => x.FromDateName);
                    }
                }
                if (pagingParams.PageSize == -1)
                {
                    pagingParams.PageSize = await query.CountAsync();
                }

                return await PagedList<CardViewModel>
                   .CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
            }
            else
                return null;

        }
        public async Task<string> ExportExcelAsync(PagingParams pagingParams, string selectedId)
        {
            User currentUser = await db.Users
                .AsNoTracking()
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.UserId == pagingParams.userId);

            var query = from cr in db.Cards
                        join ct in db.Customers on cr.CustomerId equals ct.Id
                        join fc in db.Facilities on cr.FacilityId equals fc.Id
                        join cp in db.CardTypes on cr.CardTypeId equals cp.Id
                        join sv in db.Services on cr.ServiceId equals sv.Id
                        select new CardViewModel
                        {
                            Id = cr.Id,
                            CardCode = cr.CardCode,
                            CustomerCode = ct.CustomerCode,
                            CustomerName = ct.CustomerName,
                            Address = ct.Address,
                            NumberPhone = ct.NumberPhone,
                            NameType = cp.NameType,
                            FacilityName = fc.FacilityName,
                            ServiceName = sv.ServiceName,
                            Price = sv.Money,
                            ToDate = cr.ToDate,
                            FromDate = cr.FromDate,
                            ToDateName = cr.ToDate.HasValue ? cr.ToDate.Value.ToString("dd/MM/yyyy") : "",
                            FromDateName = cr.FromDate.HasValue ? cr.FromDate.Value.ToString("dd/MM/yyyy") : "",
                        };

            //if (!string.IsNullOrEmpty(pagingParams.fromDate) && !string.IsNullOrEmpty(pagingParams.toDate))
            //{
            //    DateTime fromDate = DateTime.Parse(pagingParams.fromDate);
            //    DateTime toDate = DateTime.Parse(pagingParams.toDate);
            //    query = query.Where(x => DateTime.Parse(x.NgayLap) >= fromDate && DateTime.Parse(x.NgayLap) <= toDate);
            //}
           /* if (!string.IsNullOrEmpty(pagingParams.fromDate) && !string.IsNullOrEmpty(pagingParams.toDate))
            {
                DateTime fromDate = DateTime.Parse(pagingParams.fromDate);
                DateTime toDate = DateTime.Parse(pagingParams.toDate);
                query = query.Where(x => (x.CreatedDate >= fromDate && x.CreatedDate <= toDate.AddDays(1)));
            }*/
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
                                        (x.CardCode ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.CardCode ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.CustomerCode ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.CustomerCode ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.CustomerName ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.CustomerName ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.Address ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.Address ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.NumberPhone ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.NumberPhone ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.NameType ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.NameType ?? string.Empty).ToUpper().Contains(keyword) ||
                                        (x.FacilityName ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.FacilityName ?? string.Empty).ToUpper().Contains(keyword) ||
                                         (x.ServiceName ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.ServiceName ?? string.Empty).ToUpper().Contains(keyword) ||
                                         (x.Price.Value.ToString()).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.Price.Value.ToString()).ToUpper().Contains(keyword) ||
                                         (x.ToDateName ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.ToDateName ?? string.Empty).ToUpper().Contains(keyword) ||
                                         (x.FromDateName ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        (x.FromDateName ?? string.Empty).ToUpper().Contains(keyword)
                                        //(x.CreatedDate) == DateTime.Parse(keyword)
                                        );
            }

            if (!string.IsNullOrEmpty(pagingParams.KeywordCol))
            {
                var keyword = pagingParams.KeywordCol.ToUpper().ToTrim();

                if (pagingParams.ColName == "cardCode")
                {
                    query = query.Where(x => (x.CardCode ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.CardCode ?? string.Empty).ToUpper().Contains(keyword));
                }
                if (pagingParams.ColName == "customerName")
                {
                    query = query.Where(x => (x.CustomerName ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.CustomerName ?? string.Empty).ToUpper().Contains(keyword));
                }
                if (pagingParams.ColName == "customerCode")
                {
                    query = query.Where(x => (x.CustomerCode ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.CustomerCode ?? string.Empty).ToUpper().Contains(keyword));
                }
                if (pagingParams.ColName == "facilityName")
                {
                    query = query.Where(x => (x.FacilityName ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.FacilityName ?? string.Empty).ToUpper().Contains(keyword));
                }
                if (pagingParams.ColName == "numberPhone")
                {
                    query = query.Where(x => (x.NumberPhone ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.NumberPhone ?? string.Empty).ToUpper().Contains(keyword));
                }
                if (pagingParams.ColName == "nameType")
                {
                    query = query.Where(x => (x.NameType ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.NameType ?? string.Empty).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "facilityName")
                {
                    query = query.Where(x => (x.FacilityName ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.FacilityName ?? string.Empty).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "serviceName")
                {
                    query = query.Where(x => (x.ServiceName ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.ServiceName ?? string.Empty).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "price")
                {
                    query = query.Where(x => (x.Price.Value.ToString()).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.Price.Value.ToString()).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "toDateName")
                {
                    query = query.Where(x => (x.ToDateName ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.ToDateName ?? string.Empty).ToUpper().Contains(keyword));

                }
                if (pagingParams.ColName == "fromDateName")
                {
                    query = query.Where(x => (x.FromDateName ?? string.Empty).ToUpper().ToUnSign().Contains(keyword.ToUnSign()) || (x.FromDateName ?? string.Empty).ToUpper().Contains(keyword));

                }
            }

            if (!string.IsNullOrEmpty(pagingParams.SortKey))
            {
                if (pagingParams.SortKey == "cardCode" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.CardCode);
                }
                if (pagingParams.SortKey == "cardCode" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.CardCode);
                }
                if (pagingParams.SortKey == "customerCode" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.CustomerCode);
                }
                if (pagingParams.SortKey == "customerCode" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.CustomerCode);
                }
                if (pagingParams.SortKey == "customerName" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.CustomerName);
                }
                if (pagingParams.SortKey == "customerName" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.CustomerName);
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
                if (pagingParams.SortKey == "nameType" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.NameType);
                }
                if (pagingParams.SortKey == "nameType" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.NameType);
                }
                if (pagingParams.SortKey == "facilityName" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.FacilityName);
                }
                if (pagingParams.SortKey == "facilityName" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.FacilityName);
                }
                if (pagingParams.SortKey == "serviceName" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.ServiceName);
                }
                if (pagingParams.SortKey == "serviceName" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.ServiceName);
                }
                if (pagingParams.SortKey == "price" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.Price);
                }
                if (pagingParams.SortKey == "price" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.Price);
                }
                if (pagingParams.SortKey == "toDateName" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.ToDateName);
                }
                if (pagingParams.SortKey == "toDateName" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.ToDateName);
                }
                if (pagingParams.SortKey == "fromDateName" && pagingParams.SortValue == "ascend")
                {
                    query = query.OrderBy(x => x.FromDateName);
                }
                if (pagingParams.SortKey == "fromDateName" && pagingParams.SortValue == "descend")
                {
                    query = query.OrderByDescending(x => x.FromDateName);
                }
            }

            string _path_sample = Path.Combine(_hostingEnvironment.ContentRootPath, $"MyAssets/uploaded/sample/thong_ke_the_tap.xlsx");
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
                    List<CardViewModel> list = query.ToList();
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

                        worksheet.Cells[idx, 2].Value = it.CardCode;
                        worksheet.Cells[idx, 3].Value = it.CustomerCode;
                        worksheet.Cells[idx, 4].Value = it.CustomerName;
                        worksheet.Cells[idx, 5].Value = it.Address;
                        worksheet.Cells[idx, 6].Value = it.NumberPhone;
                        worksheet.Cells[idx, 7].Value = it.NameType;
                        worksheet.Cells[idx, 8].Value = it.FacilityName;
                        worksheet.Cells[idx, 9].Value = it.ServiceName;
                        worksheet.Cells[idx, 10].Value = it.Price;
                        worksheet.Cells[idx, 11].Value = it.ToDateName;
                        worksheet.Cells[idx, 12].Value = it.FromDateName;
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

        public Task GetById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CheckTrungMa(string CardCode)
        {
            var rs = await db.Cards.FirstOrDefaultAsync(x => x.CardCode.ToString().ToUpper().ToTrim() == (CardCode.ToString().ToUpper().ToTrim()));
            if (rs != null) return true;
            else return false;
        }
        public async Task<IList<DoanhThuTheoThangTheoNamParam>> GetDoanhThu(DoanhThuTheoThangTheoNamParam model, string Id, string selectedId)
        {
            try
            {
                var tg = new User();
                tg = await db.Users.FindAsync(Id);
                if (tg != null)
                {
                    if (tg.RoleId == "BLD" || tg.RoleId == "ADMIN")
                    {
                        if (selectedId == null || selectedId == "")
                        {
                            var query = (from kh in db.Cards
                                         join ur in db.Users on kh.CreatedBy equals ur.UserId
                                         where ur.Status == true && (kh.CreatedDate.Value.Year == model.Year) && (kh.CreatedDate.Value.Month == model.Month)
                                         select new CardViewModel
                                         {
                                             Id = kh.Id,
                                             CreatedBy = kh.CreatedBy,
                                             CreatedDate = kh.CreatedDate,
                                             NguoiThem = ur.FullName,
                                             Price = kh.Price,

                                         }).ToList();
                            var querytg = from b in query
                                          group b by b.NguoiThem.ToString() into g
                                          select new DoanhThuTheoThangTheoNamParam
                                          {
                                              FullName = g.Key,
                                              TongDoanhThu = g.Sum(x => x.Price),
                                          };
                            return querytg.ToList();
                        }
                        else
                        {
                            var query = (from kh in db.Cards
                                         where kh.CreatedBy == selectedId
                                         join ur in db.Users on kh.CreatedBy equals ur.UserId
                                         where ur.Status == true && (kh.CreatedDate.Value.Year == model.Year) && (kh.CreatedDate.Value.Month == model.Month)
                                         select new CardViewModel
                                         {
                                             Id = kh.Id,
                                             CreatedBy = kh.CreatedBy,
                                             CreatedDate = kh.CreatedDate,
                                             NguoiThem = ur.FullName,
                                             Price = kh.Price,

                                         }).ToList();
                            var querytg = from b in query
                                          group b by b.NguoiThem.ToString() into g
                                          select new DoanhThuTheoThangTheoNamParam
                                          {
                                              FullName = g.Key,
                                              TongDoanhThu = g.Sum(x => x.Price),
                                          };
                            return querytg.ToList();

                        }
                    }
                    else
                    {
                        if (tg.NguoiQuanLy == null || tg.NguoiQuanLy == "" || tg.NguoiQuanLy == string.Empty)
                        {

                            var temp = await db.Users.Where(x => x.NguoiQuanLy == tg.UserId).ToListAsync();
                            if (temp != null)
                            {

                                var query = (from kh in db.Cards
                                             join ur in db.Users on kh.CreatedBy equals ur.UserId
                                             where ur.UserId == Id || ur.NguoiQuanLy == Id
                                             where ur.Status == true && (kh.CreatedDate.Value.Year == model.Year) && (kh.CreatedDate.Value.Month == model.Month)
                                             select new CardViewModel
                                             {
                                                 Id = kh.Id,
                                                 CreatedBy = kh.CreatedBy,
                                                 CreatedDate = kh.CreatedDate,
                                                 NguoiThem = ur.FullName,
                                                 Price = kh.Price,

                                             }).ToList();
                                var querytg = from b in query
                                              group b by b.NguoiThem.ToString() into g
                                              select new DoanhThuTheoThangTheoNamParam
                                              {
                                                  FullName = g.Key,
                                                  TongDoanhThu = g.Sum(x => x.Price),
                                              };
                                return querytg.ToList();
                            }
                            else
                            {
                                var query = (from kh in db.Cards
                                             join ur in db.Users on kh.CreatedBy equals ur.UserId
                                             where kh.CreatedBy == Id
                                             where ur.Status == true && (kh.CreatedDate.Value.Year == model.Year) && (kh.CreatedDate.Value.Month == model.Month)
                                             select new CardViewModel
                                             {
                                                 Id = kh.Id,
                                                 CreatedBy = kh.CreatedBy,
                                                 CreatedDate = kh.CreatedDate,
                                                 NguoiThem = ur.FullName,
                                                 Price = kh.Price,

                                             }).ToList();
                                var querytg = from b in query
                                              group b by b.NguoiThem.ToString() into g
                                              select new DoanhThuTheoThangTheoNamParam
                                              {
                                                  FullName = g.Key,
                                                  TongDoanhThu = g.Sum(x => x.Price),
                                              };
                                return querytg.ToList();
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


                                    var query = (from kh in db.Cards
                                                 join ur in db.Users on kh.CreatedBy equals ur.UserId
                                                 where ur.Status == true && (kh.CreatedDate.Value.Year == model.Year) && (kh.CreatedDate.Value.Month == model.Month)
                                                 where ur.NguoiQuanLy == Id || ur.UserId == Id
                                                 select new CardViewModel
                                                 {
                                                     Id = kh.Id,
                                                     CreatedBy = kh.CreatedBy,
                                                     CreatedDate = kh.CreatedDate,
                                                     NguoiThem = ur.FullName,
                                                     Price = kh.Price,

                                                 }).ToList();
                                    var querytg = from b in query
                                                  group b by b.NguoiThem.ToString() into g
                                                  select new DoanhThuTheoThangTheoNamParam
                                                  {
                                                      FullName = g.Key,
                                                      TongDoanhThu = g.Sum(x => x.Price),
                                                  };

                                }
                                else
                                {
                                    var query = (from kh in db.Cards
                                                 join ur in db.Users on kh.CreatedBy equals ur.UserId
                                                 where kh.CreatedBy == Id
                                                 where ur.Status == true && (kh.CreatedDate.Value.Year == model.Year) && (kh.CreatedDate.Value.Month == model.Month)
                                                 select new CardViewModel
                                                 {
                                                     Id = kh.Id,
                                                     CreatedBy = kh.CreatedBy,
                                                     CreatedDate = kh.CreatedDate,
                                                     NguoiThem = ur.FullName,
                                                     Price = kh.Price,

                                                 }).ToList();
                                    var querytg = from b in query
                                                  group b by b.NguoiThem.ToString() into g
                                                  select new DoanhThuTheoThangTheoNamParam
                                                  {
                                                      FullName = g.Key,
                                                      TongDoanhThu = g.Sum(x => x.Price),
                                                  };
                                    return querytg.ToList();
                                }
                            }
                            else
                            {
                                var query = (from kh in db.Cards
                                             join ur in db.Users on kh.CreatedBy equals ur.UserId
                                             where kh.CreatedBy == Id
                                             where ur.Status == true && (kh.CreatedDate.Value.Year == model.Year) && (kh.CreatedDate.Value.Month == model.Month)
                                             select new CardViewModel
                                             {
                                                 Id = kh.Id,
                                                 CreatedBy = kh.CreatedBy,
                                                 CreatedDate = kh.CreatedDate,
                                                 NguoiThem = ur.FullName,
                                                 Price = kh.Price,

                                             }).ToList();
                                var querytg = from b in query
                                              group b by b.NguoiThem.ToString() into g
                                              select new DoanhThuTheoThangTheoNamParam
                                              {
                                                  FullName = g.Key,
                                                  TongDoanhThu = g.Sum(x => x.Price),
                                              };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return null;
        }
    }
}

