using AutoMapper;
using DLL;
using DLL.Entity;
using ManagementServices.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services.Repositories.Interfaces;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Services.Repositories.Implimentations
{
    public class CardTypeRespositories : ICardTypeRespositories
    {
        Datacontext db;
        IMapper mp;
        private readonly IHostingEnvironment _hostingEnvironment;
        IHttpContextAccessor _IHttpContextAccessor;
        IConfiguration _IConfiguration;
        public CardTypeRespositories(Datacontext datacontext, IMapper mapper, IHostingEnvironment IHostingEnvironment, IHttpContextAccessor IHttpContextAccessor, IConfiguration IConfiguration)
        {
            this.db = datacontext;
            this.mp = mapper;
            _hostingEnvironment = IHostingEnvironment;
            _IHttpContextAccessor = IHttpContextAccessor;
            _IConfiguration = IConfiguration;
        }
        public async Task<int> Delete(Guid Id)
        {
            var entity = await db.CardTypes.FirstOrDefaultAsync(x => x.Id == Id.ToString());
            db.CardTypes.Remove(entity);
            var rs = await db.SaveChangesAsync();
            return rs;
        }

        public async Task<List<CardTypeViewModel>> GetAll()
        {
            var query = from ct in db.CardTypes
                        select new CardTypeViewModel
                        {
                            Id = ct.Id,
                            NameType = ct.NameType,
                            Description = ct.Description,
                        };
            return await query.OrderBy(x => x.NameType).ToListAsync();
        }

        public Task<CardTypeViewModel> GetProductById(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Insert(CardTypeViewModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            model.NameType = model.NameType;
            model.Description = model.Description.ToTrim();
            var entity = mp.Map<CardType>(model);
            await db.CardTypes.AddAsync(entity);
            var rs = await db.SaveChangesAsync();
            // thanh cong 1, o loi
            return rs;
        }

        public async Task<int> Update(CardTypeViewModel model)
        {
            var pd = await db.CardTypes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == model.Id);
            pd.NameType = model.NameType.ToTrim();
            pd.Description = model.Description;
            db.CardTypes.Update(pd);
            var rs = await db.SaveChangesAsync();
            return rs; // 1 thanh cong, 0 that bai
        }
    }
}
