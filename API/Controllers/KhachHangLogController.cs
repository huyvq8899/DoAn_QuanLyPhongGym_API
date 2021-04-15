using API.Extentions;
using DLL;
using ManagementServices.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Services.Repositories.Interfaces;
using Services.ViewModels;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangLogController : BaseController
    {
        private readonly IAuthorizationService _authorizationService;
        IKhachHangLogRespositories _IKhachHangLogRespositories;
        Datacontext db;
        private readonly IConfiguration _config;
        public KhachHangLogController(IKhachHangLogRespositories IKhachHangLogRespositories, Datacontext Datacontext, IConfiguration IConfiguration, IAuthorizationService authorizationService)
        {
            _IKhachHangLogRespositories = IKhachHangLogRespositories;
            db = Datacontext;
            _config = IConfiguration;
            _authorizationService = authorizationService;
        }
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] PagingParams pagingParams, string selectedId)
        {
            var paged = await _IKhachHangLogRespositories.GetAllPagingAsync(pagingParams, selectedId);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(new { paged.Items, paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages });
        }
        [HttpGet("ExportExcel")]
        public async Task<IActionResult> ExportExcel([FromQuery] PagingParams pagingParams, string selectedId)
        {
            var base64String = await _IKhachHangLogRespositories.ExportExcelAsync(pagingParams, selectedId);
            return Ok(new { base64String = base64String });
        }
    }
}
