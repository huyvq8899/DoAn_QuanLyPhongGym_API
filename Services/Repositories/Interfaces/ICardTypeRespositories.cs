using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Repositories.Interfaces
{
    public interface ICardTypeRespositories
    {
        Task<int> Insert(CardTypeViewModel model);
        Task<int> Delete(Guid Id);
        Task<int> Update(CardTypeViewModel model);
        Task<List<CardTypeViewModel>> GetAll();
        Task<CardTypeViewModel> GetProductById(string Id);
    }
}
