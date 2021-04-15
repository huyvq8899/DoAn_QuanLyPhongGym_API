using ManagementServices.Helper;
using Services.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Repositories.Interfaces
{
    public interface IThongBaoService
    {
        Task<PagedList<ThongBaoViewModel>> GetAllPagingAsync(PagingParams pagingParams);
        Task<ThongBaoViewModel> GetByIdAsync(string id);
        Task<ThongBaoViewModel> GetDetailAsync(string id);
        Task<ThongBaoViewModel> CreateAsync(ThongBaoViewModel viewModel);
        Task<List<ThongBaoViewModel>> CreateRangeAsync(List<ThongBaoViewModel> viewModels);
        Task<int> GetCountNotOpenYetByNguoiNhanAsync(string nguoiNhanId);
        Task UpdateAllOpenedByNguoiNhanAsync(string nguoiNhanId);
        Task DeleteAsync(string id);
        Task DeleteAllByNguoiNhanIdAsync(string nguoiNhanId);
    }
}
