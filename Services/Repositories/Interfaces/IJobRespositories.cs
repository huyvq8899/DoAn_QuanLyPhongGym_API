using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Repositories.Interfaces
{
    public interface IJobRespositories
    {
        Task<int> Insert(JobViewModel model);
        Task<int> Delete(Guid Id);
        Task<int> Update(JobViewModel model);
        Task<List<JobViewModel>> GetAll();
        Task<JobViewModel> GetProductById(string Id);
    }
}
