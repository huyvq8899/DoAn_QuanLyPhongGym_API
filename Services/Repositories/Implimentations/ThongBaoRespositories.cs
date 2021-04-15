using AutoMapper;
using AutoMapper.QueryableExtensions;
using DLL;
using DLL.Entity;
using ManagementServices.Helper;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Interfaces;
using Services.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Repositories.Implimentations
{
    public class ThongBaoService : IThongBaoService
    {
        private readonly Datacontext _db;
        private readonly IMapper _mp;
        private readonly IUserRespositories _userRespositories;

        public ThongBaoService(Datacontext db, IMapper mp, IUserRespositories userRespositories)
        {
            _db = db;
            _mp = mp;
            _userRespositories = userRespositories;
        }

        public async Task<ThongBaoViewModel> CreateAsync(ThongBaoViewModel viewModel)
        {
            ThongBao model = _mp.Map<ThongBao>(viewModel);
            await _db.ThongBaos.AddAsync(model);
            await _db.SaveChangesAsync();
            ThongBaoViewModel result = _mp.Map<ThongBaoViewModel>(model);
            return result;
        }

        public async Task<List<ThongBaoViewModel>> CreateRangeAsync(List<ThongBaoViewModel> viewModels)
        {
            List<ThongBao> models = _mp.Map<List<ThongBao>>(viewModels);
            await _db.ThongBaos.AddRangeAsync(models);
            await _db.SaveChangesAsync();
            List<ThongBaoViewModel> result = _mp.Map<List<ThongBaoViewModel>>(models);
            return result;
        }

        public async Task DeleteAllByNguoiNhanIdAsync(string nguoiNhanId)
        {
            List<ThongBao> models = await _db.ThongBaos.Where(x => x.NguoiNhanId == nguoiNhanId).ToListAsync();
            _db.ThongBaos.RemoveRange(models);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            ThongBao model = await _db.ThongBaos.FirstOrDefaultAsync(x => x.ThongBaoId == id);
            _db.ThongBaos.Remove(model);
            await _db.SaveChangesAsync();
        }

        public async Task<PagedList<ThongBaoViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            IQueryable<ThongBaoViewModel> query = from tb in _db.ThongBaos
                                                  join ug in _db.Users on tb.NguoiGuiId equals ug.UserId into tmpUserGuis
                                                  from ug in tmpUserGuis.DefaultIfEmpty()
                                                  join un in _db.Users on tb.NguoiNhanId equals un.UserId into tmpUserNhans
                                                  from un in tmpUserNhans.DefaultIfEmpty()
                                                  where tb.NguoiNhanId == pagingParams.userId
                                                  orderby tb.CreatedDate descending
                                                  select new ThongBaoViewModel
                                                  {
                                                      ThongBaoId = tb.ThongBaoId,
                                                      NguoiGuiId = tb.NguoiGuiId,
                                                      NguoiNhanId = tb.NguoiNhanId,
                                                      TieuDe = tb.TieuDe,
                                                      NoiDung = tb.NoiDung,
                                                      Url = tb.Url,
                                                      LoaiThongBao = tb.LoaiThongBao,
                                                      CreatedDate = tb.CreatedDate,
                                                      IsOpend = tb.IsOpend,
                                                      IsRead = tb.IsRead,
                                                      UserNameNguoiGui = ug != null ? ug.UserName : string.Empty,
                                                      FullNameNguoiGui = ug != null ? ug.FullName : string.Empty,
                                                      UserNameNguoiNhan = un != null ? un.UserName : string.Empty,
                                                      FullNameNguoiNhan = un != null ? un.FullName : string.Empty
                                                  };

            //if (!string.IsNullOrEmpty(pagingParams.Keyword))
            //{
            //    string keyword = pagingParams.Keyword.ToUpper().ToTrim();
            //    query = query.Where(x => x.MaDieuKien.ToUpper().Contains(keyword) ||
            //                                        x.TenDieuKien.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
            //                                        x.TenDieuKien.ToUpper().Contains(keyword));
            //}

            if (pagingParams.PageSize == -1)
            {
                pagingParams.PageSize = await query.CountAsync();
            }

            return await PagedList<ThongBaoViewModel>
               .CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public async Task<ThongBaoViewModel> GetByIdAsync(string id)
        {
            ThongBaoViewModel result = await _db.ThongBaos
                .Where(x => x.ThongBaoId == id)
                .ProjectTo<ThongBaoViewModel>(_mp.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<int> GetCountNotOpenYetByNguoiNhanAsync(string nguoiNhanId)
        {
            int result = await _db.ThongBaos.CountAsync(x => x.NguoiNhanId == nguoiNhanId && x.IsOpend == false);
            return result;
        }

        public async Task<ThongBaoViewModel> GetDetailAsync(string id)
        {
            ThongBao model = await _db.ThongBaos.FirstOrDefaultAsync(x => x.ThongBaoId == id);
            if (model.IsRead == false)
            {
                model.IsRead = true;
                model.IsOpend = true;
                await _db.SaveChangesAsync();
            }

            ThongBaoViewModel result = _mp.Map<ThongBaoViewModel>(model);

            //if (result.LoaiThongBao == LoaiThongBao.HoaDonXuatChoDNRuiRo)
            //{
            //    if (!string.IsNullOrEmpty(result.ObjectId))
            //    {
            //        result.HoaDon = await _db.HoaDons
            //            .Include(x => x.HoaDonFile)
            //            .Where(x => x.HoaDonId == result.ObjectId)
            //            .ProjectTo<HoaDonViewModel>(_mp.ConfigurationProvider)
            //            .FirstOrDefaultAsync();

            //        if (!string.IsNullOrEmpty(result.HoaDon.MaSoThueNguoiMua))
            //        {
            //            result.DoanhNghiep = await _db.DoanhNghieps
            //                .Where(x => x.MaSoThue == result.HoaDon.MaSoThueNguoiMua)
            //                .ProjectTo<DoanhNghiepViewModel>(_mp.ConfigurationProvider)
            //                .FirstOrDefaultAsync();

            //            result.DoanhNghiep.TenLoaiDoanhNghiep = result.DoanhNghiep.LoaiDoanhNghiep.GetDescription();
            //            result.DoanhNghiep.TenTrangThai = result.DoanhNghiep.TrangThai.GetDescription();
            //        }
            //    }
            //}

            return result;
        }

        public async Task UpdateAllOpenedByNguoiNhanAsync(string nguoiNhanId)
        {
            List<ThongBao> notOpenYets = await _db.ThongBaos.Where(x => x.IsOpend == false && x.NguoiNhanId == nguoiNhanId).ToListAsync();
            if (notOpenYets.Count > 0)
            {
                notOpenYets.ForEach(x => x.IsOpend = true);
                await _db.SaveChangesAsync();
            }
        }
    }
}
