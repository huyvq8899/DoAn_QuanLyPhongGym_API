using AutoMapper;
using DLL;
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
using DLL.Entity;

namespace Services.Repositories.Implimentations
{
    public class ServiceRespositories : IServiceRespositories
    {
        Datacontext db;
        IMapper mp;
        private readonly IHostingEnvironment _hostingEnvironment;
        IHttpContextAccessor _IHttpContextAccessor;
        IConfiguration _IConfiguration;
        public ServiceRespositories(Datacontext datacontext, IMapper mapper, IHostingEnvironment IHostingEnvironment, IHttpContextAccessor IHttpContextAccessor, IConfiguration IConfiguration)
        {
            this.db = datacontext;
            this.mp = mapper;
            _hostingEnvironment = IHostingEnvironment;
            _IHttpContextAccessor = IHttpContextAccessor;
            _IConfiguration = IConfiguration;
        }
        public async Task<int> Delete(Guid Id)
        {
            var entity = await db.Services.FirstOrDefaultAsync(x => x.Id == Id.ToString());
            db.Services.Remove(entity);
            var rs = await db.SaveChangesAsync();
            return rs;
        }

        public async Task<List<ServiceViewModel>> GetAll()
        {
            var query = from sv in db.Services
                        select new ServiceViewModel
                        {
                            Id = sv.Id,
                            ServiceName = sv.ServiceName,
                            Description = sv.Description,
                            Money = sv.Money,
                        };
            return await query.OrderBy(x => x.ServiceName).ToListAsync();
        }

        public Task<ServiceViewModel> GetProductById(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Insert(ServiceViewModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            model.ServiceName = model.ServiceName;
            model.Money = model.Money;
            model.Description = model.Description;
            model.CreatedDate = DateTime.Now;
            model.CreatedBy = model.CreatedBy;
            var entity = mp.Map<Service>(model);
            await db.Services.AddAsync(entity);
            var rs = await db.SaveChangesAsync();
            // thanh cong 1, o loi
            return rs;
        }

        public async Task<int> Update(ServiceViewModel model)
        {
            var sv = await db.Services.AsNoTracking().FirstOrDefaultAsync(x => x.Id == model.Id);
            sv.ServiceName = model.ServiceName;
            sv.Description = model.Description;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = model.CreatedBy;
            sv.Money = model.Money;
            db.Services.Update(sv);
            var rs = await db.SaveChangesAsync();
            return rs; // 1 thanh cong, 0 that bai
        }
    }
}
