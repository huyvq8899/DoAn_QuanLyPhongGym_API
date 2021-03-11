using AutoMapper;
using DLL;
using DLL.Entity;
using ManagementServices.Helper;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Interfaces;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Repositories.Implimentations
{
    public class FunctionRespositories : IFunctionRespositories
    {
        Datacontext db;
        IMapper mp;
        public FunctionRespositories(Datacontext datacontext, IMapper mapper)
        {
            this.db = datacontext;
            this.mp = mapper;
        }

        public async Task<bool> Delete(string functionId)
        {
            var listFR = await db.Function_Roles.Where(x => x.FunctionId == functionId).ToListAsync();
            db.Function_Roles.RemoveRange(listFR);
            var eFunction = await db.Functions.FirstOrDefaultAsync(x => x.FunctionId == functionId);
            db.Functions.Remove(eFunction);
            return await db.SaveChangesAsync() > 0;
        }


        public async Task<bool> Insert(FunctionViewModel model)
        {
            // thêm vào bảng function
            model.FunctionId = Guid.NewGuid().ToString();
            var entity = mp.Map<Function>(model);
            await db.Functions.AddAsync(entity);
            // thêm vào bảng functionRole
            var listRole = await db.Roles.ToListAsync();
            //foreach (var item in listRole)
            //{
            //    var mFR = new Function_RoleViewModel()
            //    {
            //        FRID = Guid.NewGuid().ToString(),
            //        FunctionId = entity.FunctionId,
            //        RoleId = item.RoleId,
            //    };
            //    if (item.RoleName == "OWNER")
            //    {
            //        mFR.Active = true;
            //    }
            //    var eMR = mp.Map<Function_Users>(mFR);
            //    await db.Function_Roles.AddAsync(eMR);
            //}
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<PagedList<FunctionViewModel>> GetAllPaging(PagingParams pagingParams)
        {
            var query = from r in db.Functions
                        select new FunctionViewModel
                        {
                            FunctionId = r.FunctionId,
                            FunctionName = r.FunctionName ?? string.Empty,
                            Description = r.Description ?? string.Empty,
                            CreatedDate = r.CreatedDate,
                            CreatedBy = r.CreatedBy ?? string.Empty,
                            //ModifyDate = r.ModifyDate,
                            //Status = r.Status,
                            //Type = r.Type,
                            //Active = r.Active,
                        };

            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                var keyword = pagingParams.Keyword.ToUpper().ToTrim();
                query = query.Where(x => x.FunctionName.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.FunctionName.ToUpper().Contains(keyword) ||
                                        x.Description.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.Description.ToUpper().Contains(keyword)
                                        );
            }
            if (!string.IsNullOrEmpty(pagingParams.SortValue) && !pagingParams.SortValue.Equals("null") && !pagingParams.SortValue.Equals("undefined"))
            {
                switch (pagingParams.SortKey)
                {
                    case "fucntionName":
                        if (pagingParams.SortValue == "descend")
                        {
                            query = query.OrderByDescending(x => x.FunctionName);
                        }
                        else
                        {
                            query = query.OrderBy(x => x.FunctionName);
                        }
                        break;
                    case "title":
                        if (pagingParams.SortValue == "descend")
                        {
                            query = query.OrderByDescending(x => x.Description);
                        }
                        else
                        {
                            query = query.OrderBy(x => x.Description);
                        }
                        break;
                    default:
                        break;
                }
            }
            return await PagedList<FunctionViewModel>.CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }
    }
}
