using DLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Repositories.Interfaces;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthorizationService _authorizationService;
        IUserRespositories _IUserRespositories;
        Datacontext db;
        private readonly IConfiguration _config;
        public AuthController(IUserRespositories IUserRespositories, Datacontext Datacontext, IConfiguration IConfiguration, IAuthorizationService authorizationService)
        {
            _IUserRespositories = IUserRespositories;
            db = Datacontext;
            _config = IConfiguration;
            _authorizationService = authorizationService;
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            var result = await _IUserRespositories.Login(login.username, login.password);
            if (result == 1)
            {
                //var userModel = await db.Users.FirstOrDefaultAsync(x => x.UserName == login.username);
                var userModel = await _IUserRespositories.GetByUserName(login.username);
                //var userModel = await (from ur in db.Users
                //                       join rl in db.Roles on ur.RoleId equals rl.RoleId
                //                       where ur.UserName == login.username
                //                       select new UserViewModel
                //                       {
                //                           UserId = ur.UserId,
                //                           UserName = ur.UserName ?? string.Empty,
                //                           Email = ur.Email ?? "Email not set",
                //                           FullName = ur.FullName ?? "Fullname not set",
                //                           Gender = ur.Gender,
                //                           Avatar = ur.Avatar == null ? string.Empty : _IUserRespositories.GetAvatarByHost(ur.Avatar),
                //                           DateOfBirth = ur.DateOfBirth ?? DateTime.Parse("01/01/0001"),
                //                           CreatedDate = ur.CreatedDate ?? DateTime.Parse("01/01/0001"),
                //                           ModifyDate = ur.ModifyDate ?? DateTime.Parse("01/01/0001"),
                //                           CreatedBy = ur.CreatedBy ?? string.Empty,
                //                           Address = ur.Address ?? "Address not set",
                //                           Title = ur.Title ?? "Không rõ",
                //                           Phone = ur.Phone ?? "Phone not set",
                //                           Status = ur.Status,
                //                           RoleId = ur.RoleId,
                //                           IsOnline = ur.IsOnline,
                //                           RoleName = rl.RoleName ?? string.Empty,

                //                       }).FirstOrDefaultAsync();
                return Ok(new
                {
                    result,
                    userName = userModel.UserName,
                    tokenKey = await GenerateJwtAsync(userModel),
                    userId = userModel.UserId,
                    model = userModel,
                });
            }
            return Ok(new { result, userName = "", tokenKey = "" });
        }
        #region Private Methods
        private async Task<string> GenerateJwtAsync(UserViewModel user)
        {

            var claims = new List<Claim>
            {
                 new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                 new Claim(ClaimTypes.Name, user.UserName),
                 //new Claim(ClaimTypes.Role, user.RoleName),
             };

            //var roles = await _IRoleRespositories.GetListRoleByUserId(user.UserId);
            //foreach (var role in roles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
            //}

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));


            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                //Expires = DateTime.Now.AddMinutes(30),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        #endregion
    }
}