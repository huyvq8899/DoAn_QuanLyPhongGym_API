using ManagementServices.Helper;
using Microsoft.AspNetCore.Http;
using Services.Helper;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repositories.Interfaces
{
    public interface IUserRespositories
    {
        Task<List<UserViewModel>> GetAll();
        Task<List<UserViewModel>> GetAllActive();
        Task<UserViewModel> GetById(string Id);
        Task<List<UserViewModel>> GetByRoleId(string Id);
        Task<UserViewModel> GetByUserName(string UserName);
        Task<int> Insert(UserViewModel model);
        Task<int> Delete(Guid Id);
        Task<int> Update(UserViewModel model);
        Task<int> UpdatePassWord(UserViewModel model);
        Task<PagedList<UserViewModel>> GetAllPagingAsync(PagingParams pagingParams);
        Task<PagedList<UserViewModel>> GetMoreAsync(PagingParams pagingParams);
        Task<bool> CheckUserName(string userName);
        Task<bool> CheckEmail(string Email);
        Task<bool> CheckPhone(string phone);
        Task<bool> CheckPass(string username, string pass);
        Task<bool> ChangeStatus(string userId);
        Task<bool> DeleteAll(List<string> Ids);
        Task<int> Login(string username, string pass);
        Task<bool> SetRole(SetRoleParam param);
        Task<bool> SetOnline(string userId, bool isOnline);
        Task<ResultParam> UpdateAvatar(IList<IFormFile> files, string fileName, string fileSize, string userId);
        string GetAvatarByHost(string avatar);
        Task<List<UserViewModel>> GetListByMaSoThueAsync(string maSoThue);
        Task<List<UserViewModel>> GetAllLastChildById(string id);
        Task<List<UserViewModel>> GetAllActiveByUser(string Id);
        Task<List<UserViewModel>> GetBySoLuong(PagingParams pagingParams);
        Task<string> ExportExcelBaoCaoAsync(PagingParams pagingParams);

        Task<IList<int>> GetYears();
        Task<IList<int>> GetAddKHMonths();
        Task<IList<int>> GetYearsLoi();
        Task<IList<BaoCaoThemKhachHangTheoNamParam>> GetAddKHByYear(BaoCaoThemKhachHangTheoNamParam model);
        Task<IList<BaoCaoThemKhachHangByNhanVienTheoThangParam>> GetAddKhachHangByNhanVien(BaoCaoThemKhachHangByNhanVienTheoThangParam model);
        Task<IList<BaoCaoThemKhachHangByNhanVienTheoThangParam>> GetAddKhachHangByNhanVienKD(BaoCaoThemKhachHangByNhanVienTheoThangParam model);
        Task<List<UserViewModel>> GetAllUserById(string Id);
        Task<bool> CheckQuyen(string userId);
    }
}
