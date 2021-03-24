using DLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Services.Repositories.Interfaces;
using Services.ViewModels;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : BaseController
    {
        private readonly IAuthorizationService _authorizationService;
        IServiceRespositories _IServiceRespositories;
        Datacontext db;
        private readonly IConfiguration _config;
        public ServiceController(IServiceRespositories IServiceRespositories, Datacontext Datacontext, IConfiguration IConfiguration, IAuthorizationService authorizationService)
        {
            _IServiceRespositories = IServiceRespositories;
            db = Datacontext;
            _config = IConfiguration;
            _authorizationService = authorizationService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _IServiceRespositories.GetAll();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(ServiceViewModel model)
        {
            var result = await _IServiceRespositories.Insert(model);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(ServiceViewModel model)
        {
            var result = await _IServiceRespositories.Update(model);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _IServiceRespositories.Delete(Id);
            return Ok(result);
        }
    }
}