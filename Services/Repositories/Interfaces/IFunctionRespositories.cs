
using ManagementServices.Helper;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repositories.Interfaces
{
    public interface IFunctionRespositories
    {
        Task<bool> Insert(FunctionViewModel model);
        Task<bool> Delete(string functionId);
        Task<PagedList<FunctionViewModel>> GetAllPaging(PagingParams pagingParams);
    }
}
