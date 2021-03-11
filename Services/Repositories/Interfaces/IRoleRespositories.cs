using ManagementServices.Helper;
using Services.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Repositories.Interfaces
{
    public interface IRoleRespositories
    {
        Task<List<RoleViewModel>> GetAll();
        Task<RoleViewModel> GetById(string Id);
        Task<bool> CheckRoleName(string roleName);
        Task<bool> Reset();
        Task<bool> Insert(RoleViewModel model);
        Task<int> Delete(string Id);
        Task<int> Update(RoleViewModel model);
        Task<PagedList<RoleViewModel>> GetAllPaging(PagingParams pagingParams);
        Task<List<RoleViewModel>> GetRoleByTree();
    }
}
