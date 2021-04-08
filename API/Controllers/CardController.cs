using DLL;
using ManagementServices.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Services.Helper;
using Services.Repositories.Interfaces;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : BaseController
    {
        private readonly IAuthorizationService _authorizationService;
        ICardRespositories _ICardRespositories;
        Datacontext db;
        private readonly IConfiguration _config;
        public CardController(ICardRespositories ICardRespositories, Datacontext Datacontext, IConfiguration IConfiguration, IAuthorizationService authorizationService)
        {
            _ICardRespositories = ICardRespositories;
            db = Datacontext;
            _config = IConfiguration;
            _authorizationService = authorizationService;
        }
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPagingAsync([FromQuery] PagingParams pagingParams, string Id, string selectedId)
        {
            PagedList<CardViewModel> paged = await _ICardRespositories.GetAllPagingAsync(pagingParams, Id, selectedId);
            return Ok(new { paged.Items, paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages });
        }
        [HttpGet("GetById")]
        /*       public async Task<IActionResult> GetById(string Id)
               {
                   var result = await _ICardRespositories.GetById(Id);
                   return Ok(result);
               }
               [HttpGet("GetKHByUser")]
               public async Task<IActionResult> GetKHByUser(string Id)
               {
                   var result = await _ICardRespositories.GetKHByUser(Id);
                   return Ok(result);
               }*/
        [HttpPost]
        public async Task<IActionResult> Insert(int TN, CardViewModel model)
        {
            var result = await _ICardRespositories.Insert(TN, model);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _ICardRespositories.Delete(Id);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(int TN, CardViewModel model)
        {
            var result = await _ICardRespositories.Update(TN, model);
            return Ok(result);
        }
        [HttpGet("CheckTrungMa/{MaKH}")]
        public async Task<IActionResult> CheckTrungMa(string MaKH)
        {
            var result = await _ICardRespositories.CheckTrungMa(MaKH);
            return Ok(result);
        }
        /*     [HttpGet("CreateMaKH")]
             public async Task<IActionResult> CreateMaKhachHang()
             {
                 var result = await _ICardRespositories.CreateMa();
                 return Ok(result);
             }*/
        [HttpGet("ExportExcel")]
        public async Task<IActionResult> ExportExcel([FromQuery] PagingParams pagingParams, string selectedId)
        {
            var base64String = await _ICardRespositories.ExportExcelAsync(pagingParams, selectedId);
            return Ok(new { base64String = base64String });
        }
        [HttpPost("GetDoanhThu")]
        public async Task<IActionResult> GetDoanhThu(DoanhThuTheoThangTheoNamParam model, string Id, string selectedId)
        {
            var result = await _ICardRespositories.GetDoanhThu(model, Id, selectedId);
            return Ok(result);
        }
    }
}
