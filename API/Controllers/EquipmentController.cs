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
    public class EquipmentController : BaseController
    {
        private readonly IAuthorizationService _authorizationService;
        IEquipmentRespositories _IEquipmentRespositories;
        Datacontext db;
        private readonly IConfiguration _config;
        public EquipmentController(IEquipmentRespositories IEquipmentRespositories, Datacontext Datacontext, IConfiguration IConfiguration, IAuthorizationService authorizationService)
        {
            _IEquipmentRespositories = IEquipmentRespositories;
            db = Datacontext;
            _config = IConfiguration;
            _authorizationService = authorizationService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _IEquipmentRespositories.GetAll();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(EquipmentViewModel model)
        {
            var result = await _IEquipmentRespositories.Insert(model);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(EquipmentViewModel model)
        {
            var result = await _IEquipmentRespositories.Update(model);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _IEquipmentRespositories.Delete(Id);
            return Ok(result);
        }
    }
}
