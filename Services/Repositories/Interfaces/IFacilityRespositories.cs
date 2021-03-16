using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Repositories.Interfaces
{
    public interface IFacilityRespositories
    {
        Task<int> Insert(FacilityViewModel model);
        Task<int> Delete(Guid Id);
        Task<int> Update(FacilityViewModel model);
        Task<List<FacilityViewModel>> GetAll();
        Task<FacilityViewModel> GetProductById(string Id);
    }
}
