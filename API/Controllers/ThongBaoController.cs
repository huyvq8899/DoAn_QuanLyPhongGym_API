using API.Hubs;
using DLL;
using ManagementServices.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
    public class ThongBaoController : BaseController
    {
        private readonly IThongBaoService _thongBaoService;
        private readonly Datacontext _datacontext;
        private readonly IHubContext<SignalRHub> _hubContext;

        public ThongBaoController(IThongBaoService thongBaoService, Datacontext datacontext, IHubContext<SignalRHub> hubContext)
        {
            _thongBaoService = thongBaoService;
            _datacontext = datacontext;
            _hubContext = hubContext;
        }

        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] PagingParams pagingParams)
        {
            PagedList<ThongBaoViewModel> paged = await _thongBaoService.GetAllPagingAsync(pagingParams);
            return Ok(new { paged.Items, paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            ThongBaoViewModel data = await _thongBaoService.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        [HttpGet("GetDetail/{id}")]
        public async Task<IActionResult> GetDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            ThongBaoViewModel data = await _thongBaoService.GetDetailAsync(id);
            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        [HttpGet("GetCountNotOpenYetByNguoiNhan/{nguoiNhanId}")]
        public async Task<IActionResult> GetCountNotOpenYetByNguoiNhan(string nguoiNhanId)
        {
            if (string.IsNullOrEmpty(nguoiNhanId))
            {
                return BadRequest();
            }

            int data = await _thongBaoService.GetCountNotOpenYetByNguoiNhanAsync(nguoiNhanId);
            return Ok(data);
        }

        [HttpPut("UpdateAllOpenedByNguoiNhan")]
        public async Task<IActionResult> UpdateAllOpenedByNguoiNhan(UserViewModel userVM)
        {
            if (string.IsNullOrEmpty(userVM.UserId))
            {
                return BadRequest();
            }

            using (var transaction = _datacontext.Database.BeginTransaction())
            {
                try
                {
                    await _thongBaoService.UpdateAllOpenedByNguoiNhanAsync(userVM.UserId);
                    transaction.Commit();
                    return Ok(true);
                }
                catch (Exception ex)
                {
                    return Ok(false);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ThongBaoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ThongBaoViewModel result = await _thongBaoService.CreateAsync(viewModel);
                return CreatedAtAction(nameof(GetById), new { id = result.ThongBaoId }, result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest();
            }

            try
            {
                await _thongBaoService.DeleteAsync(id);
                return Ok(true);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("DeleteAllByNguoiNhanId/{nguoiNhanId}")]
        public async Task<IActionResult> DeleteAllByNguoiNhanId(string nguoiNhanId)
        {
            if (string.IsNullOrEmpty(nguoiNhanId))
            {
                return BadRequest();
            }

            try
            {
                await _thongBaoService.DeleteAllByNguoiNhanIdAsync(nguoiNhanId);
                return Ok(true);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
