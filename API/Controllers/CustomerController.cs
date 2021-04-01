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
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseController
    {
        private readonly IAuthorizationService _authorizationService;
        ICustomerRespositories _ICustomerRespositories;
        Datacontext db;
        private readonly IConfiguration _config;
        public CustomerController(ICustomerRespositories ICustomerRespositories, Datacontext Datacontext, IConfiguration IConfiguration, IAuthorizationService authorizationService)
        {
            _ICustomerRespositories = ICustomerRespositories;
            db = Datacontext;
            _config = IConfiguration;
            _authorizationService = authorizationService;
        }
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPagingAsync([FromQuery] PagingParams pagingParams, string Id,string selectedId)
        {
            PagedList<CustomerViewModel> paged = await _ICustomerRespositories.GetAllPagingAsync(pagingParams, Id,selectedId);
            return Ok(new { paged.Items, paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages });
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string Id)
        {
            var result = await _ICustomerRespositories.GetById(Id);
            return Ok(result);
        }
        [HttpGet("GetKHByUser")]
        public async Task<IActionResult> GetKHByUser(string Id)
        {
            var result = await _ICustomerRespositories.GetKHByUser(Id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(int TN, CustomerViewModel model)
        {
                var result = await _ICustomerRespositories.Insert(TN,model);
                return Ok(result);     
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _ICustomerRespositories.Delete(Id);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(int TN,CustomerViewModel model)
        {
            var result = await _ICustomerRespositories.Update(TN,model);
            return Ok(result);
        }
        [HttpGet("CheckTrungMa/{MaKH}")]
        public async Task<IActionResult> CheckTrungMa(string MaKH)
        {
            var result = await _ICustomerRespositories.CheckTrungMa(MaKH);
            return Ok(result);
        }
        [HttpGet("CreateMaKH")]
        public async Task<IActionResult> CreateMaKhachHang()
        {
            var result = await _ICustomerRespositories.CreateMa();
            return Ok(result);
        }
        [HttpGet("ExportExcel")]
        public async Task<IActionResult> ExportExcel([FromQuery] PagingParams pagingParams,string selectedId)
        {
            var base64String = await _ICustomerRespositories.ExportExcelAsync(pagingParams, selectedId);
            return Ok(new { base64String = base64String });
        }
        [HttpGet("GetAllKH")]
        public async Task<IActionResult> GetAllKH()
        {
            var result = await _ICustomerRespositories.GetAllKH();
            return Ok(result);
        }
    }
}
