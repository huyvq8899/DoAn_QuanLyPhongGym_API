using ManagementServices.Helper;
using Services.Helper;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Repositories.Interfaces
{
    public interface ICardRespositories
    {
        Task<int> Insert(int TN, CardViewModel model);
        Task<int> Delete(Guid Id);
        Task<int> Update(int TN, CardViewModel model);
        Task<List<CardViewModel>> GetAll();
        Task<PagedList<CardViewModel>> GetAllPagingAsync(PagingParams pagingParams, string Id, string selectedId);
        Task<string> ExportExcelAsync(PagingParams pagingParams, string selectedId);
        Task GetById(string id);
        Task<bool> CheckTrungMa(string CardCode);
        Task<IList<DoanhThuTheoThangTheoNamParam>> GetDoanhThu(DoanhThuTheoThangTheoNamParam model, string Id, string selectedId);
    }
}
