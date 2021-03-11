using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extentions;
using DLL;
using ManagementServices.Helper;
using Microsoft.AspNetCore.Mvc;
using Services.Repositories.Interfaces;
using Services.ViewModels;

namespace API.Controllers
{
    public class FunctionController : BaseController
    {
        IFunctionRespositories _IFunctionRespositories;
        Datacontext db;
        public FunctionController(IFunctionRespositories IFunctionRespositories, Datacontext Datacontext)
        {
            _IFunctionRespositories = IFunctionRespositories;
            db = Datacontext;
        }
        [HttpPost("Insert")]
        public async Task<IActionResult> Insert(FunctionViewModel model)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var result = await _IFunctionRespositories.Insert(model);
                    if (result == false)
                    {
                        throw new Exception("");
                    }
                    transaction.Commit();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return Ok(false);
                }
            }
        }
        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] PagingParams pagingParams)
        {
            var paged = await _IFunctionRespositories.GetAllPaging(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(new { paged.Items, paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages });
        }
        [HttpDelete("Delete/{functionId}")]
        public async Task<IActionResult> Delete(string functionId)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var result = await _IFunctionRespositories.Delete(functionId);
                    if (result == false)
                    {
                        throw new Exception("");
                    }
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