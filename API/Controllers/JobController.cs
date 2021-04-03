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
    public class JobController : BaseController
    {
        private readonly IAuthorizationService _authorizationService;
        IJobRespositories _IJobRespositories;
        Datacontext db;
        private readonly IConfiguration _config;
        public JobController(IJobRespositories IJobRespositories, Datacontext Datacontext, IConfiguration IConfiguration, IAuthorizationService authorizationService)
        {
            _IJobRespositories = IJobRespositories;
            db = Datacontext;
            _config = IConfiguration;
            _authorizationService = authorizationService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _IJobRespositories.GetAll();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(JobViewModel model)
        {
            var result = await _IJobRespositories.Insert(model);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(JobViewModel model)
        {
            var result = await _IJobRespositories.Update(model);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _IJobRespositories.Delete(Id);
            return Ok(result);
        }
        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById(string Id)
        {
            var result = await _IJobRespositories.GetProductById(Id);
            return Ok(result);
        }
    }
}
