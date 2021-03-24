using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Repositories.Interfaces
{
    public interface IServiceRespositories
    {
        Task<int> Insert(ServiceViewModel model);
        Task<int> Delete(Guid Id);
        Task<int> Update(ServiceViewModel model);
        Task<List<ServiceViewModel>> GetAll();
    }
}
