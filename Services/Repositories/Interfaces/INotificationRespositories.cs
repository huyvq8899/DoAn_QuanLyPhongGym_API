using ManagementServices.Helper;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repositories.Interfaces
{
    public interface INotificationRespositories
    {
        Task<string> Insert(NotificationViewModel model);
        Task<bool> Update(NotificationViewModel model);
        Task<bool> Delete(string id);
        Task<NotificationViewModel> GetById(string id);
        Task<bool> UpdateViewNotic(string id);
        Task<bool> DeleteNoticByUser(string id);
        Task<int> GetNoticNotViewByUser(string id);
        Task<int> GetAllUnView(string role, string xeid, string id);
        Task<PagedList<NotificationViewModel>> GetAllPagingAsync(PagingParams pagingParams);
        Task<int> DeleteById(string Id);
    }
}
