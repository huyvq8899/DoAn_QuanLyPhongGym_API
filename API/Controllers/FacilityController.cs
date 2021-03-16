using DLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
    public class FacilityController : BaseController
    {
        private readonly IAuthorizationService _authorizationService;
        IFacilityRespositories _IFacilityRespositories;
        Datacontext db;
        private readonly IConfiguration _config;
        public FacilityController(IFacilityRespositories IFacilityRespositories, Datacontext Datacontext, IConfiguration IConfiguration, IAuthorizationService authorizationService)
        {
            _IFacilityRespositories = IFacilityRespositories;
            db = Datacontext;
            _config = IConfiguration;
            _authorizationService = authorizationService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _IFacilityRespositories.GetAll();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(FacilityViewModel model)
        {
            var result = await _IFacilityRespositories.Insert(model);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(FacilityViewModel model)
        {
            var result = await _IFacilityRespositories.Update(model);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _IFacilityRespositories.Delete(Id);
            return Ok(result);
        }
        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById(string Id)
        {
            var result = await _IFacilityRespositories.GetProductById(Id);
            return Ok(result);
        }
    }
}
