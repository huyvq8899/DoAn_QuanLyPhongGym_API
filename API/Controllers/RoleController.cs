using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extentions;
using DLL;
using ManagementServices.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Interfaces;
using Services.ViewModels;

namespace API.Controllers
{
    public class RoleController : BaseController
    {
        IRoleRespositories _IRoleRespositories;
        Datacontext db;
        public RoleController(IRoleRespositories IRoleRespositories,
            Datacontext datacontext
            )
        {
            _IRoleRespositories = IRoleRespositories;
            db = datacontext;
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string Id)
        {
            var result = 0;
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    result = await _IRoleRespositories.Delete(Id); //-1 bang co khoa ngoai khong xoa duoc
                    transaction.Commit();
                    return Ok(result);
                }
                catch (DbUpdateException)
                {
                    return Ok(-1); // khong xoa duoc khoa ngoai
                }
                catch (Exception ex)
                {
                    return Ok(result);
                }
            }

        }

        [HttpGet("GetAllRole")]
        public async Task<IActionResult> GetAllRole()
        {
            var result = await _IRoleRespositories.GetAll();
            return Ok(result);
        }

        [HttpGet("GetRoleByTree")]
        public async Task<IActionResult> GetRoleByTree()
        {
            var result = await _IRoleRespositories.GetRoleByTree();
            return Ok(result);
        }

        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetById(string Id)
        {
            var result = await _IRoleRespositories.GetById(Id);
            return Ok(result);
        }
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] PagingParams pagingParams)
        {
            var paged = await _IRoleRespositories.GetAllPaging(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(new { paged.Items, paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages });
        }
        [HttpPost]
        public async Task<IActionResult> Insert(RoleViewModel model)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var result = await _IRoleRespositories.Insert(model);
                    transaction.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return Ok(false);
                }
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(RoleViewModel model)
        {
            var result = await _IRoleRespositories.Update(model);
            return Ok(result);
        }
        [HttpGet("CheckRoleName/{roleName}")]
        public async Task<IActionResult> CheckRoleName(string roleName)
        {
            var result = await _IRoleRespositories.CheckRoleName(roleName);
            return Ok(result);
        }
        [HttpGet("Reset")]
        public async Task<IActionResult> Reset()
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var result = await _IRoleRespositories.Reset();
                    transaction.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return Ok(false);
                }
            }
        }
    }
}