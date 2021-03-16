using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Repositories.Interfaces
{
    public interface IEquipmentRespositories
    {
        Task<int> Insert(EquipmentViewModel model);
        Task<int> Delete(Guid Id);
        Task<int> Update(EquipmentViewModel model);
        Task<List<EquipmentViewModel>> GetAll();
    }
}
