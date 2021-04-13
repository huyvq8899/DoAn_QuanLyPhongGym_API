using API.Extentions;
using DLL;
using ManagementServices.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Services.Repositories.Interfaces;
using Services.ViewModels;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class NotificationController : BaseController
    {
        private INotificationRespositories _INotificationRespositories;
        private Datacontext _db;

        public NotificationController(INotificationRespositories INotificationRespositories, Datacontext datacontext)
        {
            _INotificationRespositories = INotificationRespositories;
            _db = datacontext;
        }
        [HttpPost("Insert")]
        public async Task<IActionResult> Insert(NotificationViewModel model)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var result = await _INotificationRespositories.Insert(model);
                    transaction.Commit();
                    return Ok(result);
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }
        /*        [HttpPut("UpdateView")]
                public async Task<IActionResult> Update(NoticeParam noticeParam)
                {
                    using (var transaction = _db.Database.BeginTransaction())
                    {
                        try
                        {
                            await _INotificationRespositories.UpdateView(noticeParam);
                            transaction.Commit();
                            return Ok(true);
                        }
                        catch (Exception ex)
                        {
                            return Ok(false);
                        }
                    }

                }
        */
        [HttpPut("Update")]
        public async Task<IActionResult> Update(NotificationViewModel model)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var result = await _INotificationRespositories.Update(model);
                    transaction.Commit();
                    return Ok(result);
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> Delete(string id)
        {
            using (IDbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var result = await _INotificationRespositories.Delete(id);
                    transaction.Commit();
                    return Ok(result);
                }
                catch (DbUpdateException ex)
                {
                    return Ok(new
                    {
                        result = "DbUpdateException",
                        value = false
                    });
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        [HttpGet("GetAllUnView")]
        public async Task<IActionResult> GetAllUnView(string role, string xeid, string id)
        {
            var result = await _INotificationRespositories.GetAllUnView(role, xeid, id);
            return Ok(result);
        }
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] PagingParams pagingParams)
        {
            var paged = await _INotificationRespositories.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(new { paged.Items, paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages });
        }
        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _INotificationRespositories.GetById(id);
            return Ok(result);
        }
        [HttpGet("UpdateViewNotic/{Id}")]
        public async Task<IActionResult> UpdateViewNotic(string id)
        {
            var result = await _INotificationRespositories.UpdateViewNotic(id);
            return Ok(result);
        }
        [HttpGet("GetNoticNotViewByUser/{Id}")]
        public async Task<IActionResult> GetNoticNotViewByUser(string id)
        {
            var result = await _INotificationRespositories.GetNoticNotViewByUser(id);
            return Ok(result);
        }
        [HttpDelete("DeleteNoticByUser/{Id}")]
        public async Task<IActionResult> DeleteNoticByUser(string id)
        {
            var result = await _INotificationRespositories.DeleteNoticByUser(id);
            return Ok(result);
        }
        [HttpDelete("DeleteById/{Id}")]
        public async Task<IActionResult> DeleteById(string Id)
        {
            var result = await _INotificationRespositories.DeleteById(Id);
            return Ok(result);
        }
    }
}
