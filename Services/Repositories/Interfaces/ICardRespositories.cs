using ManagementServices.Helper;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Repositories.Interfaces
{
    public interface ICardRespositories
    {
        Task<int> Insert(CardViewModel model);
        Task<int> Delete(Guid Id);
        Task<int> Update(CardViewModel model);
        Task<List<CardViewModel>> GetAll();
        Task<PagedList<CardViewModel>> GetAllPagingAsync(PagingParams pagingParams, string Id, string selectedId);
        Task<string> ExportExcelAsync(PagingParams pagingParams, string selectedId);
    }
}
