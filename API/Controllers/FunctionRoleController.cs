using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DLL;
using Microsoft.AspNetCore.Mvc;
using Services.Repositories.Interfaces;
using Services.ViewModels;

namespace API.Controllers
{
    public class FunctionRoleController : BaseController
    {
        Datacontext db;
        public FunctionRoleController(Datacontext Datacontext)
        {
            db = Datacontext;
        }
        //[HttpGet("GetAll/{type}")]
        //public async Task<IActionResult> GetAll(string type)
        //{
        //    var rs = await _IFunction_RoleRespositories.GetAll(type);
        //    return Ok(rs);
        //}
        //[HttpGet("GetAllFunctionRole")]
        //public async Task<IActionResult> GetAllFunctionRole()
        //{
        //    var rs = await _IFunction_RoleRespositories.GetAllFunctionRole();
        //    return Ok(rs);
        //}
        //[HttpPut("UpdateFunctionRole")]
        //public async Task<IActionResult> UpdateFunctionRole(List<RoleViewModel> list)
        //{
        //    var rs = await _IFunction_RoleRespositories.UpdateFunctionRole(list);
        //    return Ok(rs);
        //}
        //[HttpGet("InnitRoleFuntion")]
        //public async Task<IActionResult> InnitRoleFuntion()
        //{
        //    var rs = await _IFunction_RoleRespositories.InnitRoleFuntion();
        //    return Ok(rs);
        //}
        //[HttpPost("SetActive")]
        //public async Task<IActionResult> SetActive(Function_UserViewModel model)
        //{
        //    var rols = User.FindAll(ClaimTypes.Role);
        //    List<string> roles = new List<string>();
        //    foreach (var item in rols)
        //    {
        //        roles.Add(item.Value);
        //    }
        //    if (roles.Count > 0)
        //    {
        //        if (roles[0] != "OWNER")
        //        {
        //            return Unauthorized();// loi 401
        //        }
        //    }
        //    else
        //    {
        //        return Unauthorized();// loi 401
        //    }

        //    var rs = await _IFunction_RoleRespositories.SetActive(model);
        //    return Ok(rs);
        //}
        //[HttpGet("GetFunctionByRoleName/{roleName}")]
        //public async Task<IActionResult> GetFunctionByRoleName(string roleName)
        //{
        //    var rs = await _IFunction_RoleRespositories.GetFunctionByRoleName(roleName);
        //    return Ok(rs);
        //}
    }
}