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
    public interface ICustomerRespositories
    {
        Task<int> Insert(int TN, CustomerViewModel model);
        Task<int> Delete(Guid Id);
        Task<int> Update(int TN, CustomerViewModel model);
        Task<PagedList<CustomerViewModel>> GetAllPagingAsync(PagingParams pagingParams, string Id,string selectedId);
        Task<CustomerViewModel> GetById(string Id);
       // Task<List<DoiTuongViewModel>> GetAllKH();
        Task<List<CustomerViewModel>> GetKHByUser(string Id);
        Task<bool> CheckTrungMa(string MaKH);
        Task<string> CreateMa();
        Task<string> ExportExcelAsync(PagingParams pagingParams,string selectedId);
    }
}
