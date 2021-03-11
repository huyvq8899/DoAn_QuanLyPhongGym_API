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
    public class CardTypeController : BaseController
    {
        private readonly IAuthorizationService _authorizationService;
        ICardTypeRespositories _ICardTypeRespositories;
        Datacontext db;
        private readonly IConfiguration _config;
        public CardTypeController(ICardTypeRespositories ICardTypeRespositories, Datacontext Datacontext, IConfiguration IConfiguration, IAuthorizationService authorizationService)
        {
            _ICardTypeRespositories = ICardTypeRespositories;
            db = Datacontext;
            _config = IConfiguration;
            _authorizationService = authorizationService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _ICardTypeRespositories.GetAll();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(CardTypeViewModel model)
        {
            var result = await _ICardTypeRespositories.Insert(model);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(CardTypeViewModel model)
        {
            var result = await _ICardTypeRespositories.Update(model);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _ICardTypeRespositories.Delete(Id);
            return Ok(result);
        }
        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById(string Id)
        {
            var result = await _ICardTypeRespositories.GetProductById(Id);
            return Ok(result);
        }
    }
}
