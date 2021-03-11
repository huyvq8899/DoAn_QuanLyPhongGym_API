using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DLL;
using DLL.Entity;
using ManagementServices.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Helper;
using Services.Repositories.Interfaces;
using Services.ViewModels;
using API.Extentions;

namespace API.Controllers
{
    public class UserController : BaseController
    {
        private readonly IAuthorizationService _authorizationService;
        IUserRespositories _IUserRespositories;
        Datacontext db;
        private readonly IConfiguration _config;
        public UserController(IUserRespositories IUserRespositories, Datacontext Datacontext, IConfiguration IConfiguration, IAuthorizationService authorizationService)
        {
            _IUserRespositories = IUserRespositories;
            db = Datacontext;
            _config = IConfiguration;
            _authorizationService = authorizationService;
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _IUserRespositories.Delete(Id);
            return Ok(result);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _IUserRespositories.GetAll();
            return Ok(result);
        }
        [HttpGet("GetByRoleId/{Id}")]
        public async Task<IActionResult> GetByRoleId(string Id)
        {
            var result = await _IUserRespositories.GetByRoleId(Id);
            return Ok(result);
        }
        [HttpGet("GetAllActive")]
        public async Task<IActionResult> GetAllActive()
        {
            var result = await _IUserRespositories.GetAllActive();
            return Ok(result);
        }
        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetById(string Id)
        {
            var result = await _IUserRespositories.GetById(Id);
            return Ok(result);
        }
        [HttpGet("GetDataLogin/{userName}")]
        public async Task<IActionResult> GetDataLogin(string userName)
        {
            var userModel = await _IUserRespositories.GetByUserName(userName);
            return Ok(new
            {
                userName = userModel.UserName,
                userId = userModel.UserId,
                model = userModel,
            });
        }
        [HttpGet("GetByUserName/{UserName}")]
        public async Task<IActionResult> GetByUserName(string UserName)
        {
            var result = await _IUserRespositories.GetByUserName(UserName);
            return Ok(result);
        }
        [HttpGet("CheckUserName/{userName}")]
        public async Task<IActionResult> CheckUserName(string userName)
        {
            var result = await _IUserRespositories.CheckUserName(userName);
            return Ok(result);
        }
        [HttpGet("CheckEmail")]
        public async Task<IActionResult> CheckEmail(string Email)
        {
            var result = await _IUserRespositories.CheckEmail(Email);
            return Ok(result);
        }
        [HttpGet("CheckEmail/{phone}")]
        public async Task<IActionResult> CheckPhone(string phone)
        {
            var result = await _IUserRespositories.CheckPhone(phone);
            return Ok(result);
        }
        [HttpGet("CheckPass/{pass}")]
        public async Task<IActionResult> CheckPass(string username, string pass)
        {
            var result = await _IUserRespositories.CheckPass(username, pass);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(UserViewModel model)
        {
            var result = await _IUserRespositories.Insert(model);
            return Ok(result);
        }
        [HttpGet("ChangeStatus/{userId}")]
        public async Task<IActionResult> ChangeStatus(string userId)
        {
            var result = await _IUserRespositories.ChangeStatus(userId);
            return Ok(result);
        }
        [HttpGet("SetOnline/{userId}/{isOnline}")]
        public async Task<IActionResult> SetOnline(string userId, bool isOnline)
        {
            var result = await _IUserRespositories.SetOnline(userId, isOnline);
            return Ok(result);
        }
        [HttpPost("SetRole")]
        public async Task<IActionResult> SetRole(SetRoleParam param)
        {
            var result = false;
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    result = await _IUserRespositories.SetRole(param);
                    if (!result)
                    {
                        throw new Exception("");
                    }
                    transaction.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return Ok(result);
                }
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(UserViewModel model)
        {
            var result = await _IUserRespositories.Update(model);
            return Ok(result);
        }
        [HttpPut("UpdatePassWord")]
        public async Task<IActionResult> UpdatePassWord(UserViewModel model)
        {
            var result = await _IUserRespositories.UpdatePassWord(model);
            return Ok(result);
        }
        [HttpPost("DeleteAll")]
        public async Task<IActionResult> deleteAll(List<string> Ids)
        {
            var result = await _IUserRespositories.DeleteAll(Ids);
            return Ok(result);
        }
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] PagingParams pagingParams)
        {
            var paged = await _IUserRespositories.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(new { paged.Items, paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages });

        }
        [HttpGet("GetMore")]
        public async Task<IActionResult> GetMoreAsync([FromQuery] PagingParams pagingParams)
        {
            var paged = await _IUserRespositories.GetMoreAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(new { paged.Items, paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages });

        }
        [HttpPost("UpdateAvatar")]
        public async Task<IActionResult> UpdateAvatar(IList<IFormFile> files)
        {
            string fileName = Request.Form["fileName"];
            string fileSize = Request.Form["fileSize"];
            string userId = Request.Form["userId"];
            var result = await _IUserRespositories.UpdateAvatar(files, fileName, fileSize, userId);
            if (result.Result != true) throw new Exception("");
            return Ok(new
            {
                Result = result.Result,
                User = result.User
            });
        }
        [HttpGet("GetAllActiveByUser")]
        public async Task<IActionResult> GetAllActiveByUser(string Id)
        {
            var result = await _IUserRespositories.GetAllActiveByUser(Id);
            return Ok(result);
        }
        [HttpGet("GetBySoLuong")]
        public async Task<IActionResult> GetBySoLuong([FromQuery] PagingParams pagingParams)
        {
            var result = await _IUserRespositories.GetBySoLuong(pagingParams);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("ExportExcelBaoCao")]
        public async Task<IActionResult> ExportExcelBaoCao([FromQuery] PagingParams pagingParams)
        {
            var base64String = await _IUserRespositories.ExportExcelBaoCaoAsync(pagingParams);
            return Ok(new { base64String = base64String });
        }
        [HttpGet("GetYears")]
        public async Task<IActionResult> GetYears()
        {
            var result = await _IUserRespositories.GetYears();
            return Ok(result);
        }
        [HttpGet("GetAddKHMonths")]
        public async Task<IActionResult> GetAddKHMonths()
        {
            var result = await _IUserRespositories.GetAddKHMonths();
            return Ok(result);
        }
        [HttpPost("GetAddKHByYear")]
        public async Task<IActionResult> GetLoiByYear(BaoCaoThemKhachHangTheoNamParam model)
        {
            var result = await _IUserRespositories.GetAddKHByYear(model);
            return Ok(result);
        }
        [HttpPost("GetAddKhachHangByNhanVien")]
        public async Task<IActionResult> GetLoiByNhanVien(BaoCaoThemKhachHangByNhanVienTheoThangParam model)
        {
            var result = await _IUserRespositories.GetAddKhachHangByNhanVien(model);
            return Ok(result);
        }

        [HttpPost("GetAddKhachHangByNhanVienKD")]
        public async Task<IActionResult> GetLoiByNhanVienKD(BaoCaoThemKhachHangByNhanVienTheoThangParam model)
        {
            var result = await _IUserRespositories.GetAddKhachHangByNhanVienKD(model);
            return Ok(result);
        }
        [HttpGet("GetAllUserById")]
        public async Task<IActionResult> GetAllUserById(string Id)
        {
            var result = await _IUserRespositories.GetAllUserById(Id);
            return Ok(result);
        }
        [HttpGet("CheckQuyen/{userId}")]
        public async Task<IActionResult> CheckQuyen(string userId)
        {
            var result = await _IUserRespositories.CheckQuyen(userId);
            return Ok(result);
        }
    }
}
