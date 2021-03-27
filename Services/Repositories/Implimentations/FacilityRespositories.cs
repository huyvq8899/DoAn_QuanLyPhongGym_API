using AutoMapper;
using DLL;
using DLL.Entity;
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
    public class FacilityRespositories : IFacilityRespositories
    {
        Datacontext db;
        IMapper mp;
        private readonly IHostingEnvironment _hostingEnvironment;
        IHttpContextAccessor _IHttpContextAccessor;
        IConfiguration _IConfiguration;
        public FacilityRespositories(Datacontext datacontext, IMapper mapper, IHostingEnvironment IHostingEnvironment, IHttpContextAccessor IHttpContextAccessor, IConfiguration IConfiguration)
        {
            this.db = datacontext;
            this.mp = mapper;
            _hostingEnvironment = IHostingEnvironment;
            _IHttpContextAccessor = IHttpContextAccessor;
            _IConfiguration = IConfiguration;
        }
        public async Task<int> Delete(Guid Id)
        {
            var entity = await db.Facilities.FirstOrDefaultAsync(x => x.Id == Id.ToString());
            db.Facilities.Remove(entity);
            var rs = await db.SaveChangesAsync();
            return rs;
        }

        public async Task<List<FacilityViewModel>> GetAll()
        {
            var query = from fc in db.Facilities
                        select new FacilityViewModel
                        {
                            Id = fc.Id,
                            FacilityName = fc.FacilityName,
                            TaxCode = fc.TaxCode,
                            Address = fc.Address,
                            NumberPhone = fc.NumberPhone,
                            Email = fc.Email,
                            Fax = fc.Fax,
                            EstablishedDay = fc.EstablishedDay,
                        };
            return await query.OrderBy(x => x.FacilityName).ToListAsync();
        }

        public Task<FacilityViewModel> GetProductById(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Insert(FacilityViewModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            model.FacilityName = model.FacilityName;
            model.Address = model.Address;
            model.TaxCode = model.TaxCode;
            model.Email = model.Email;
            model.Fax = model.Fax;
            model.NumberPhone = model.NumberPhone;
            model.EstablishedDay = model.EstablishedDay;
            model.CreatedDate = DateTime.Now;
            model.CreatedBy = model.CreatedBy;
            var entity = mp.Map<Facility>(model);
            await db.Facilities.AddAsync(entity);
            var rs = await db.SaveChangesAsync();
            // thanh cong 1, o loi
            return rs;
        }

        public async Task<int> Update(FacilityViewModel model)
        {
            var fc = await db.Facilities.AsNoTracking().FirstOrDefaultAsync(x => x.Id == model.Id);
            fc.FacilityName = model.FacilityName;
            fc.Address = model.Address;
            fc.TaxCode = model.TaxCode;
            fc.Fax = model.Fax;
            fc.EstablishedDay = model.EstablishedDay;
            fc.NumberPhone = model.NumberPhone;
            fc.Email = model.Email;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = model.CreatedBy;
            db.Facilities.Update(fc);
            var rs = await db.SaveChangesAsync();
            return rs; // 1 thanh cong, 0 that bai
        }

    }
}
