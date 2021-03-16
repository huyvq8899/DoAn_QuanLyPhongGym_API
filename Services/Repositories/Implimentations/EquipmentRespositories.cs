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
using System.Linq;
using System.Threading.Tasks;

namespace Services.Repositories.Implimentations
{
    public class EquipmentRespositories : IEquipmentRespositories
    {
        Datacontext db;
        IMapper mp;
        private readonly IHostingEnvironment _hostingEnvironment;
        IHttpContextAccessor _IHttpContextAccessor;
        IConfiguration _IConfiguration;
        public EquipmentRespositories(Datacontext datacontext, IMapper mapper, IHostingEnvironment IHostingEnvironment, IHttpContextAccessor IHttpContextAccessor, IConfiguration IConfiguration)
        {
            this.db = datacontext;
            this.mp = mapper;
            _hostingEnvironment = IHostingEnvironment;
            _IHttpContextAccessor = IHttpContextAccessor;
            _IConfiguration = IConfiguration;
        }
        public async Task<int> Delete(Guid Id)
        {
            var entity = await db.Equipments.FirstOrDefaultAsync(x => x.Id == Id.ToString());
            db.Equipments.Remove(entity);
            var rs = await db.SaveChangesAsync();
            return rs;
        }

        public async Task<List<EquipmentViewModel>> GetAll()
        {
            var query = from eq in db.Equipments
                        select new EquipmentViewModel
                        {
                            Id = eq.Id,
                            Name = eq.Name,
                            Amount = eq.Amount,
                            Description = eq.Description,
                            UserId = eq.UserId,
                        };
            return await query.OrderBy(x => x.Name).ToListAsync();
        }

        public Task<EquipmentViewModel> GetProductById(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Insert(EquipmentViewModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            model.Name = model.Name;
            model.Description = model.Description;
            model.Amount = model.Amount;
            model.CreatedDate = DateTime.Now;
            model.CreatedBy = model.CreatedBy;
            var entity = mp.Map<Equipment>(model);
            await db.Equipments.AddAsync(entity);
            var rs = await db.SaveChangesAsync();
            // thanh cong 1, o loi
            return rs;
        }

        public async Task<int> Update(EquipmentViewModel model)
        {
            var eq = await db.Equipments.AsNoTracking().FirstOrDefaultAsync(x => x.Id == model.Id);
            eq.Name = model.Name.ToTrim();
            eq.Amount = model.Amount;
            eq.UserId = model.UserId;
            eq.Description = model.Description;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = model.CreatedBy;
            db.Equipments.Update(eq);
            var rs = await db.SaveChangesAsync();
            return rs; // 1 thanh cong, 0 that bai
        }
    }


}
