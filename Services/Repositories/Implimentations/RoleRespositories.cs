using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class RoleRespositories : IRoleRespositories
    {
        IUserRespositories _IUserRespositories;
        Datacontext db;
        IMapper mp;
        public RoleRespositories(Datacontext datacontext, IMapper mapper, IUserRespositories _iUserRespositories)
        {
            this.db = datacontext;
            this.mp = mapper;
            this._IUserRespositories = _iUserRespositories;
        }

        public async Task<bool> CheckRoleName(string roleName)
        {
            var entity = await db.Roles.FirstOrDefaultAsync(x => x.RoleName.ToUpper().Trim() == roleName.ToUpper().Trim());
            if (entity != null) return true;
            return false;
        }

        public async Task<int> Delete(string Id)
        {
            try
            {
                //var listFR = await db.Function_Roles.Where(x => x.RoleId == Id).ToListAsync();
                //db.Function_Roles.RemoveRange(listFR);
                var entity = await db.Roles.FindAsync(Id);
                db.Roles.Remove(entity);
                var rs = await db.SaveChangesAsync();
                return rs;
            }
            catch (DbUpdateException)
            {
                return -1; // khong xoa duoc khoa ngoai
            }
        }

        public async Task<List<RoleViewModel>> GetAll()
        {
            var entity = await db.Roles.ToListAsync();
            var model = mp.Map<List<RoleViewModel>>(entity);
            return model;
        }

        public async Task<PagedList<RoleViewModel>> GetAllPaging(PagingParams pagingParams)
        {
            try
            {
                var query = from r in db.Roles
                            orderby r.RoleName
                            select new RoleViewModel
                            {
                                RoleId = r.RoleId,
                                RoleName = r.RoleName ?? string.Empty,
                                CreatedDate = r.CreatedDate,
                                CreatedBy = r.CreatedBy ?? string.Empty,
                                ModifyDate = r.ModifiedDate,
                                Status = r.Status,
                                ParentRoleId = r.ParentRoleId,
                                ParentRoleViewModel = (from pr in db.Roles
                                                       where r.ParentRoleId == pr.RoleId
                                                       select new RoleViewModel
                                                       {
                                                           RoleId = pr.RoleId,
                                                           RoleName = pr.RoleName
                                                       }).FirstOrDefault()
                            };

                if (!string.IsNullOrEmpty(pagingParams.Keyword))
                {
                    var keyword = pagingParams.Keyword.ToUpper().ToTrim();
                    query = query.Where(x => x.RoleName.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                            x.RoleName.ToUpper().Contains(keyword)
                                            );
                }
                if (!string.IsNullOrEmpty(pagingParams.SortValue) && !pagingParams.SortValue.Equals("null") && !pagingParams.SortValue.Equals("undefined"))
                {
                    switch (pagingParams.SortKey)
                    {
                        case "roleName":
                            if (pagingParams.SortValue == "descend")
                            {
                                query = query.OrderByDescending(x => x.RoleName);
                            }
                            else
                            {
                                query = query.OrderBy(x => x.RoleName);
                            }
                            break;
                        default:
                            break;
                    }
                }
                return await PagedList<RoleViewModel>.CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<RoleViewModel> GetById(string Id)
        {
            var entity = await db.Roles.FindAsync(Id);
            return mp.Map<RoleViewModel>(entity);
        }

        public async Task<List<RoleViewModel>> GetRoleByTree()
        {
            List<RoleViewModel> roles = await db.Roles
                                            .AsNoTracking()
                                            .OrderBy(x => x.RoleName)
                                            .ProjectTo<RoleViewModel>(mp.ConfigurationProvider)
                                            .ToListAsync();

            var childsHash = roles.ToLookup(cat => cat.ParentRoleId);
            //var childsHash = roles;
            foreach (var cat in roles)
            {

                cat.Key = cat.RoleId;
                cat.Title = cat.RoleName;
                cat.children = childsHash[cat.RoleId].ToList();
                if (cat.children != null && cat.children.Count == 0)
                {
                    cat.isLeaf = true;
                }
                cat.expanded = true;
            }
            return roles.Where(x => x.ParentRoleId == null).ToList();
        }

        public async Task<bool> Insert(RoleViewModel model)
        {
            model.RoleId = Guid.NewGuid().ToString();
            model.CreatedDate = DateTime.Now;
            model.ModifyDate = model.CreatedDate;
            var entity = mp.Map<Role>(model);
            await db.Roles.AddAsync(entity);
            var rs = await db.SaveChangesAsync() > 0;
            if (rs == false) return false;
            //
            var listFunction = await db.Functions.ToListAsync();

            //foreach (var efuntion in listFunction)
            //{
            //    var mFR = new Function_RoleViewModel();
            //    mFR.Active = entity.RoleName == "OWNER" ? true : false;
            //    mFR.RoleId = entity.RoleId;
            //    mFR.FunctionId = efuntion.FunctionId;
            //    await db.Function_Roles.AddAsync(mp.Map<Function_Users>(mFR));
            //}
            var rs1 = await db.SaveChangesAsync() > 0;
            if (rs1 == false) return false;
            return true;
        }

        public async Task<bool> Reset()
        {
            var listFR = await db.Function_Roles.ToListAsync();
            db.Function_Roles.RemoveRange(listFR);
            var rs = await db.SaveChangesAsync() > 0;
            if (!rs) return false;
            //var rs1 = await _IFunction_RoleRespositories.InnitRoleFuntion();
            //if (!rs1) return false;
            return true;
        }

        public async Task<int> Update(RoleViewModel model)
        {
            var entity = mp.Map<Role>(model);
            db.Roles.Update(entity);
            var rs = await db.SaveChangesAsync();
            return rs;
        }
    }
}
