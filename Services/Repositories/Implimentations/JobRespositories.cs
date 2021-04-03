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
    public class JobRespositories : IJobRespositories
    {
        Datacontext db;
        IMapper mp;
        private readonly IHostingEnvironment _hostingEnvironment;
        IHttpContextAccessor _IHttpContextAccessor;
        IConfiguration _IConfiguration;
        public JobRespositories(Datacontext datacontext, IMapper mapper, IHostingEnvironment IHostingEnvironment, IHttpContextAccessor IHttpContextAccessor, IConfiguration IConfiguration)
        {
            this.db = datacontext;
            this.mp = mapper;
            _hostingEnvironment = IHostingEnvironment;
            _IHttpContextAccessor = IHttpContextAccessor;
            _IConfiguration = IConfiguration;
        }
        public async Task<int> Delete(Guid Id)
        {
            var entity = await db.Jobs.FirstOrDefaultAsync(x => x.Id == Id.ToString());
            db.Jobs.Remove(entity);
            var rs = await db.SaveChangesAsync();
            return rs;
        }

        public async Task<List<JobViewModel>> GetAll()
        {
            var query = from jb in db.Jobs
                        select new JobViewModel
                        {
                            Id = jb.Id,
                            JobName = jb.JobName,
                            PlaceWork = jb.PlaceWork,
                            Description = jb.Description,
                        };
            return await query.OrderBy(x => x.JobName).ToListAsync();
        }

        public Task<JobViewModel> GetProductById(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Insert(JobViewModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            model.JobName = model.JobName;
            model.PlaceWork = model.PlaceWork;
            model.Description = model.Description;
            var entity = mp.Map<Job>(model);
            await db.Jobs.AddAsync(entity);
            var rs = await db.SaveChangesAsync();
            // thanh cong 1, o loi
            return rs;
        }

        public async Task<int> Update(JobViewModel model)
        {
            var pd = await db.Jobs.AsNoTracking().FirstOrDefaultAsync(x => x.Id == model.Id);
            pd.JobName = model.JobName;
            pd.PlaceWork = model.PlaceWork;
            pd.Description = model.Description;
            db.Jobs.Update(pd);
            var rs = await db.SaveChangesAsync();
            return rs; // 1 thanh cong, 0 that bai
        }
    }
}
