using ManagementServices.Helper;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Repositories.Interfaces
{
    public interface IKhachHangLogRespositories
    {
        Task<int> Insert(KhachHangLogViewModels model);
        Task<int> Delete(Guid Id);
        Task<int> Update(KhachHangLogViewModels model);
        Task<List<KhachHangLogViewModels>> GetAllLichSuKh();
        Task<PagedList<KhachHangLogViewModels>> GetAllPagingAsync(PagingParams pagingParams, string selectedId);
        Task<string> ExportExcelAsync(PagingParams pagingParams, string selectedId);
    }
}
