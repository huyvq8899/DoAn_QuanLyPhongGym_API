using AutoMapper;
using AutoMapper.QueryableExtensions;
using DLL;
using DLL.Entity;
using ManagementServices.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Services.Helper;
using Services.Repositories.Interfaces;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Repositories.Implimentations
{
    public class UserRespositories : IUserRespositories
    {
        Datacontext db;
        IMapper mp;
        private readonly IHostingEnvironment _hostingEnvironment;
        IHttpContextAccessor _IHttpContextAccessor;
        IConfiguration _IConfiguration;
        public UserRespositories(Datacontext datacontext, IMapper mapper, IHostingEnvironment IHostingEnvironment, IHttpContextAccessor IHttpContextAccessor, IConfiguration IConfiguration)
        {
            this.db = datacontext;
            this.mp = mapper;
            _hostingEnvironment = IHostingEnvironment;
            _IHttpContextAccessor = IHttpContextAccessor;
            _IConfiguration = IConfiguration;

        }
        public async Task<int> Delete(Guid Id)
        {
            try
            {
                var entity = await db.Users.FirstOrDefaultAsync(x => x.UserId == Id.ToString());
                db.Users.Remove(entity);
                var rs = await db.SaveChangesAsync();
                return rs;
            }
            catch (DbUpdateException)
            {
                return -1;
            }
        }

        public async Task<List<UserViewModel>> GetAll()
        {
            var entity = await db.Users.ToListAsync();
            var model = mp.Map<List<UserViewModel>>(entity);
            return model;
            var query = from ur in db.Users
                        join rl in db.Roles on ur.RoleId equals rl.RoleId
                        orderby ur.CreatedDate descending
                        select new UserViewModel
                        {
                            UserId = ur.UserId,
                            UserName = ur.UserName ?? string.Empty,
                            Email = ur.Email ?? "Email not set",
                            FullName = ur.FullName ?? "Fullname not set",
                            NguoiQuanLy = ur.NguoiQuanLy ?? string.Empty,
                            Gender = ur.Gender,
                            Avatar = ur.Avatar == null ? string.Empty : this.GetAvatarByHost(ur.Avatar),
                            DateOfBirth = ur.DateOfBirth ?? DateTime.Parse("01/01/0001"),
                            CreatedDate = ur.CreatedDate ?? DateTime.Parse("01/01/0001"),
                            ModifiedDate = ur.ModifiedDate ?? DateTime.Parse("01/01/0001"),
                            CreatedBy = ur.CreatedBy ?? string.Empty,
                            Address = ur.Address ?? "Address not set",
                            Title = ur.Title ?? "Không rõ",
                            Phone = ur.Phone ?? "Phone not set",
                            Status = ur.Status,
                            RoleId = ur.RoleId,
                            RoleName = rl.RoleName ?? string.Empty,
                            IsOnline = ur.IsOnline,
                        };
            return await query.OrderBy(x => x.PhongName).ThenBy(x => x.FullName).ToListAsync();

            //return null;
        }
        public async Task<List<UserViewModel>> GetAllActive()
        {
            var query = from ur in db.Users
                        join rl in db.Roles on ur.RoleId equals rl.RoleId
                        where ur.Status == true
                        orderby ur.CreatedDate descending
                        select new UserViewModel
                        {
                            UserId = ur.UserId,
                            UserName = ur.UserName ?? string.Empty,
                            Email = ur.Email ?? "Email not set",
                            FullName = ur.FullName ?? "Fullname not set",
                            NguoiQuanLy = ur.NguoiQuanLy ?? string.Empty,
                            Gender = ur.Gender,
                            Avatar = ur.Avatar == null ? string.Empty : this.GetAvatarByHost(ur.Avatar),
                            DateOfBirth = ur.DateOfBirth ?? DateTime.Parse("01/01/0001"),
                            CreatedDate = ur.CreatedDate ?? DateTime.Parse("01/01/0001"),
                            ModifiedDate = ur.ModifiedDate ?? DateTime.Parse("01/01/0001"),
                            CreatedBy = ur.CreatedBy ?? string.Empty,
                            Address = ur.Address ?? "Address not set",
                            Title = ur.Title ?? "Không rõ",
                            Phone = ur.Phone ?? "Phone not set",
                            Status = ur.Status,
                            RoleId = ur.RoleId,
                            RoleName = rl.RoleName ?? string.Empty,
                            IsOnline = ur.IsOnline,
                        };

            return await query.OrderBy(x => x.PhongName).ThenBy(x => x.FullName).ToListAsync();
            //return null;
        }
        public async Task<UserViewModel> GetById(string Id)
        {
            var query = from ur in db.Users
                        join rl in db.Roles on ur.RoleId equals rl.RoleId
                        where ur.UserId == Id
                        select new UserViewModel
                        {
                            UserId = ur.UserId,
                            UserName = ur.UserName ?? string.Empty,
                            Email = ur.Email,
                            FullName = ur.FullName,
                            NguoiQuanLy = ur.NguoiQuanLy ?? string.Empty,
                            Gender = ur.Gender,
                            Avatar = ur.Avatar == null ? string.Empty : this.GetAvatarByHost(ur.Avatar),
                            DateOfBirth = ur.DateOfBirth ?? DateTime.Parse("01/01/0001"),
                            CreatedDate = ur.CreatedDate ?? DateTime.Parse("01/01/0001"),
                            ModifiedDate = ur.ModifiedDate ?? DateTime.Parse("01/01/0001"),
                            CreatedBy = ur.CreatedBy ?? string.Empty,
                            Address = ur.Address,
                            Title = ur.Title,
                            Phone = ur.Phone,
                            Status = ur.Status,
                            RoleId = ur.RoleId,
                            IsOnline = ur.IsOnline,
                            RoleName = rl.RoleName ?? string.Empty,
                        };
            return await query.FirstOrDefaultAsync();

            //return null;
        }
        public async Task<int> Insert(UserViewModel model)
        {
            model.UserName = model.UserName.ToTrim();
            model.Password = "password";
            model.ConfirmPassword = "password";
            model.UserId = Guid.NewGuid().ToString();
            model.Status = true;
            model.Password = CreateMD5.ConvertoMD5(model.Password + model.UserName.ToUpper().Trim());
            model.ConfirmPassword = CreateMD5.ConvertoMD5(model.ConfirmPassword + model.UserName.ToUpper().Trim());
            model.CreatedDate = DateTime.Now;
            var entity = mp.Map<User>(model);
            await db.Users.AddAsync(entity);
            var rs = await db.SaveChangesAsync();
            // thanh cong 1, o loi
            return rs;
        }
        public async Task<bool> CheckUserName(string userName)
        {
            var rs = await db.Users.FirstOrDefaultAsync(x => x.UserName.ToString().ToUpper().ToTrim() == (userName.ToString().ToUpper().ToTrim()));
            if (rs != null) return true;
            else return false;
        }
        public async Task<bool> CheckPhone(string phone)
        {
            var rs = await db.Users.FirstOrDefaultAsync(x => x.Phone == phone);
            if (rs != null) return true;
            else return false;
        }
        public async Task<bool> ChangeStatus(string userId)
        {
            var rs = await db.Users.FirstOrDefaultAsync(x => x.UserId == (userId));
            rs.Status = !rs.Status;
            db.Users.Update(rs);
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<bool> CheckEmail(string Email)
        {
            var rs = await db.Users.FirstOrDefaultAsync(x => x.Email.ToString().ToUpper().ToTrim() == (Email.ToString().ToUpper().ToTrim()));
            if (rs != null) return true;
            else return false;

        }
        public async Task<int> Update(UserViewModel model)
        {
            var ur = await db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == model.UserId);
            ur.FullName = model.FullName;
            ur.Title = model.Title;
            ur.DateOfBirth = model.DateOfBirth;
            ur.Phone = model.Phone;
            ur.Address = model.Address;
            ur.Email = model.Email;
            ur.RoleId = model.RoleId;
            ur.NguoiQuanLy = model.NguoiQuanLy;
            ur.UserName = model.UserName;

            //var entity = mp.Map<User>(model);
            db.Users.Update(ur);
            var rs = await db.SaveChangesAsync();
            return rs; // 1 thanh cong, 0 that bai
        }
        public async Task<int> UpdatePassWord(UserViewModel model)
        {
            var entity = await db.Users.FindAsync(model.UserId);
            model.Password = CreateMD5.ConvertoMD5(model.Password + entity.UserName.ToUpper().Trim());
            entity.Password = model.Password;
            entity.ConfirmPassword = model.Password;
            db.Users.Update(entity);
            var rs = await db.SaveChangesAsync();
            return rs; // 1 thanh cong, 0 that bai
        }
        public async Task<PagedList<UserViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            var query = from p in db.Users
                        join rl in db.Roles on p.RoleId equals rl.RoleId 
                        select new UserViewModel
                        {
                            UserId = p.UserId,
                            UserName = p.UserName ?? string.Empty,
                            Email = p.Email ?? string.Empty,
                            FullName = p.FullName ?? string.Empty,
                            NguoiQuanLy = p.NguoiQuanLy ?? string.Empty,
                            Gender = p.Gender,
                            Avatar = p.Avatar == null ? string.Empty : this.GetAvatarByHost(p.Avatar),
                            DateOfBirth = p.DateOfBirth,
                            CreatedDate = p.CreatedDate,
                            Address = p.Address ?? string.Empty,
                            Phone = p.Phone ?? string.Empty,
                            Status = p.Status,
                            RoleId = p.RoleId,
                            RoleName = rl.RoleName,
                            Title = p.Title ?? string.Empty,
                            IsOnline = p.IsOnline
                        };
            // var query = db.Phones.Where(x => x.Status == true);
            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                var keyword = pagingParams.Keyword.ToUpper().ToTrim();
                if (DateTime.TryParseExact(keyword, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                {
                    query = query.Where(x => x.DateOfBirth.Value.Day == date.Day && x.DateOfBirth.Value.Month == date.Month && x.DateOfBirth.Value.Year == date.Year);
                }
                query = query.Where(x => x.UserName.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.UserName.ToUpper().Contains(keyword) ||
                                        x.PhongName.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.PhongName.ToUpper().Contains(keyword) ||
                                        x.Email.ToUpper().Contains(keyword) ||
                                        x.FullName.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.FullName.ToUpper().Contains(keyword) ||
                                        x.Address.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.Address.ToUpper().Contains(keyword) ||
                                        x.Phone.Contains(keyword) ||
                                        x.RoleName.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.RoleName.ToUpper().Contains(keyword) ||
                                        x.NguoiQuanLy.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.NguoiQuanLy.ToUpper().Contains(keyword)
                                        );
            }
            if (!string.IsNullOrEmpty(pagingParams.SortValue) && !pagingParams.SortValue.Equals("null") && !pagingParams.SortValue.Equals("undefined"))
            {
                switch (pagingParams.SortKey)
                {
                    case "phongName":
                        if (pagingParams.SortValue == "descend")
                        {
                            query = query.OrderByDescending(x => x.PhongName);
                        }
                        else
                        {
                            query = query.OrderBy(x => x.PhongName);
                        }
                        break;
                    case "nguoiQuanLy":
                        if (pagingParams.SortValue == "descend")
                        {
                            query = query.OrderByDescending(x => x.Phone);
                        }
                        else
                        {
                            query = query.OrderBy(x => x.NguoiQuanLy);
                        }
                        break;
                    case "userName":
                        if (pagingParams.SortValue == "descend")
                        {
                            query = query.OrderByDescending(x => x.UserName);
                        }
                        else
                        {
                            query = query.OrderBy(x => x.UserName);
                        }
                        break;
                    case "email":
                        if (pagingParams.SortValue == "descend")
                        {
                            query = query.OrderByDescending(x => x.Email);
                        }
                        else
                        {
                            query = query.OrderBy(x => x.Email);
                        }
                        break;
                    case "fullName":
                        if (pagingParams.SortValue == "descend")
                        {
                            query = query.OrderByDescending(x => x.FullName);
                        }
                        else
                        {
                            query = query.OrderBy(x => x.FullName);
                        }
                        break;
                    case "address":
                        if (pagingParams.SortValue == "descend")
                        {
                            query = query.OrderByDescending(x => x.Address);
                        }
                        else
                        {
                            query = query.OrderBy(x => x.Address);
                        }
                        break;
                    case "phone":
                        if (pagingParams.SortValue == "descend")
                        {
                            query = query.OrderByDescending(x => x.Phone);
                        }
                        else
                        {
                            query = query.OrderBy(x => x.Phone);
                        }
                        break;
                    case "roleName":
                        if (pagingParams.SortValue == "descend")
                        {
                            query = query.OrderByDescending(x => x.RoleName);
                        }
                        else
                        {
                            query = query.OrderBy(x => x.RoleName);
                        }
                        break;
                    default:
                        break;
                }
            }
            return await PagedList<UserViewModel>.CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
            //return null;
        }
        public async Task<PagedList<UserViewModel>> GetMoreAsync(PagingParams pagingParams)
        {
            var query = from p in db.Users
                        join r in db.Roles on p.RoleId equals r.RoleId 
                        select new UserViewModel
                        {
                            UserId = p.UserId,
                            Password = p.Password ?? string.Empty,
                            ConfirmPassword = p.ConfirmPassword ?? string.Empty,
                            UserName = p.UserName ?? string.Empty,
                            Email = p.Email ?? string.Empty,
                            FullName = p.FullName ?? string.Empty,
                            NguoiQuanLy = p.NguoiQuanLy ?? string.Empty,
                            Gender = p.Gender,
                            Avatar = p.Avatar == null ? string.Empty : this.GetAvatarByHost(p.Avatar),
                            DateOfBirth = p.DateOfBirth,
                            CreatedDate = p.CreatedDate,
                            Address = p.Address ?? string.Empty,
                            Phone = p.Phone ?? string.Empty,
                            Status = p.Status,
                            RoleId = p.RoleId,
                            RoleName = r.RoleName,
                            IsOnline = p.IsOnline,
                        };
            // var query = db.Phones.Where(x => x.Status == true);
            return await PagedList<UserViewModel>.CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);

            //return null;
        }
        public async Task<bool> DeleteAll(List<string> Ids)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in Ids)
                    {
                        var entity = await db.Users.FirstOrDefaultAsync(x => x.UserId.ToString() == item);
                        db.Remove(entity);
                    }
                    transaction.Commit();
                    return await db.SaveChangesAsync() > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public async Task<bool> CheckPass(string username, string pass)
        {
            var passMD5 = CreateMD5.ConvertoMD5(pass.Trim() + username.ToUpper().Trim());
            var entity = await db.Users.FirstOrDefaultAsync(x => x.UserName.ToUpper().Trim() == username.ToUpper().Trim());
            //if (entity == null) return false;
            if (entity.Password == passMD5) return true;
            else return false;
        }
        public async Task<int> Login(string username, string pass)
        {
            var checkUsername = await this.CheckUserName(username.Trim());
            if (checkUsername == true)
            {
                var checkPass = await this.CheckPass(username, pass);
                if (checkPass == true)
                {
                    var entity = (await db.Users.FirstOrDefaultAsync(x => x.UserName == username.ToTrim()));
                    if (entity.Status == false && entity.UserName != "superadmin") return 2; // tài khoản bị khóa
                    else
                    {
                        //entity.IsOnline = true;
                        //db.Users.Update(entity);
                        //var rs = await db.SaveChangesAsync();
                        //if (rs != 1) return -2; // lỗi không xác định
                        return 1;
                    }; // đăng nhập thành công
                }
                else
                {
                    return 0; // sai pass
                }
            }
            else
            {
                return -1; // tài khoản không tồn tại
            }
        }

        public async Task<bool> SetRole(SetRoleParam param)
        {
            var roleId = (await db.Roles.FirstOrDefaultAsync(x => x.RoleName == param.RoleName)).RoleId;
            var entity = await db.Users.FirstOrDefaultAsync(x => x.UserId == param.UserId);
            entity.RoleId = roleId;
            db.Users.Update(entity);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> SetOnline(string userId, bool isOnline)
        {
            var entity = await db.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            entity.IsOnline = isOnline;
            db.Users.Update(entity);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<UserViewModel> GetByUserName(string UserName)
        {
            var query = from ur in db.Users
                        where ur.UserName == UserName
                        select new UserViewModel
                        {
                            UserId = ur.UserId,
                            UserName = ur.UserName ?? string.Empty,
                            Email = ur.Email ?? "Email not set",
                            FullName = ur.FullName ?? "Fullname not set",
                            NguoiQuanLy = ur.NguoiQuanLy ?? "Không có",
                            Gender = ur.Gender,
                            Avatar = ur.Avatar == null ? string.Empty : this.GetAvatarByHost(ur.Avatar),
                            DateOfBirth = ur.DateOfBirth ?? DateTime.Parse("01/01/0001"),
                            CreatedDate = ur.CreatedDate ?? DateTime.Parse("01/01/0001"),
                            ModifiedDate = ur.ModifiedDate ?? DateTime.Parse("01/01/0001"),
                            CreatedBy = ur.CreatedBy ?? string.Empty,
                            Address = ur.Address ?? "Address not set",
                            Title = ur.Title ?? "Không rõ",
                            Phone = ur.Phone ?? "Phone not set",
                            Status = ur.Status,
                            RoleId = ur.RoleId,
                            IsOnline = ur.IsOnline,
                        };
            return await query.FirstOrDefaultAsync();
        }

        public async Task<ResultParam> UpdateAvatar(IList<IFormFile> files, string fileName, string fileSize, string userId)
        {
            var entity = await db.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            var upload = new UploadFile(_hostingEnvironment);
            string name = "";
            var fileUrl = upload.InsertFileAvatar(out name, files, _IConfiguration);
            if (!String.IsNullOrEmpty(fileUrl))
            {
                // xóa avatar cũ
                if (entity.Avatar != null)
                {
                    var rsDeleteFile = upload.DeleteFileAvatar(entity.Avatar, _IConfiguration);
                    if (rsDeleteFile != true) return new ResultParam
                    {
                        Result = false,
                        User = null

                    };
                }
                // thêm avatar mới
                try
                {
                    FileInfo file = new FileInfo(fileUrl);
                    // Lưu vào csdl
                    entity.Avatar = name;
                    db.Users.Update(entity);
                    var rs = await db.SaveChangesAsync() > 0;
                    if (rs != true)
                    {
                        return new ResultParam
                        {
                            Result = rs,
                            User = null

                        };
                    }
                    entity.Avatar = this.GetAvatarByHost(entity.Avatar);
                    return new ResultParam
                    {
                        Result = rs,
                        User = mp.Map<UserViewModel>(entity)

                    };
                }
                catch (Exception ex)
                {
                    FileInfo fileInfo = new FileInfo(fileUrl);
                    fileInfo.Delete();
                    throw ex;
                }
            }
            else
            {
                return new ResultParam
                {
                    Result = false,
                    User = null

                };
            }
        }
        public string GetAvatarByHost(string avatar)
        {
            var filename = "FilesUpload/Avatar/" + avatar;
            // string folder = _hostingEnvironment.WebRootPath + $@"\uploaded\excels";
            //string folder = _hostingEnvironment.WebRootPath + $@"\FilesUpload";
            //string filePath = Path.Combine(folder, filename);
            //string url = _IConfiguration["FolderFileBase:wework"] + filename;
            string url = "";
            if (_IHttpContextAccessor.HttpContext.Request.IsHttps)
            {
                url = "https://" + _IHttpContextAccessor.HttpContext.Request.Host;
            }
            else
            {
                url = "http://" + _IHttpContextAccessor.HttpContext.Request.Host;
            }
            url = url + "/" + filename;

            return url;
        }

        public async Task<List<UserViewModel>> GetByRoleId(string Id)
        {
            var query = from p in db.Users
                        join r in db.Roles on p.RoleId equals r.RoleId
                        where p.RoleId == Id
                        select new UserViewModel
                        {
                            UserId = p.UserId,
                            Password = p.Password ?? string.Empty,
                            ConfirmPassword = p.ConfirmPassword ?? string.Empty,
                            UserName = p.UserName ?? string.Empty,
                            Email = p.Email ?? string.Empty,
                            FullName = p.FullName ?? string.Empty,
                            NguoiQuanLy = p.NguoiQuanLy ?? string.Empty,
                            Gender = p.Gender,
                            Avatar = p.Avatar == null ? string.Empty : this.GetAvatarByHost(p.Avatar),
                            DateOfBirth = p.DateOfBirth,
                            CreatedDate = p.CreatedDate,
                            Address = p.Address ?? string.Empty,
                            Phone = p.Phone ?? string.Empty,
                            Status = p.Status,
                            RoleId = p.RoleId,
                            RoleName = r.RoleName,
                            Title = p.Title ?? string.Empty,
                            IsOnline = p.IsOnline,
                        };
            return await query.ToListAsync();

            //return null;
        }

        public async Task<List<UserViewModel>> GetListByMaSoThueAsync(string maSoThue)
        {
            List<UserViewModel> result = new List<UserViewModel>();

            List<UserViewModel> listAllUser = await (from u in db.Users
                                                     join r in db.Roles on u.RoleId equals r.RoleId
                                                     select new UserViewModel
                                                     {
                                                         UserId = u.UserId,
                                                         UserName = u.UserName,
                                                         FullName = u.FullName,
                                                         Avatar = u.Avatar,
                                                         RoleId = r.RoleId,
                                                         RoleName = r.RoleName,
                                                     }).ToListAsync();

            return result.DistinctBy(x => x.UserId).ToList();

            // return null;
        }

        public async Task<List<UserViewModel>> GetAllLastChildById(string id)
        {
            User user = await db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == id);
            List<Role> roleAlls = await db.Roles.AsNoTracking().ToListAsync();
            List<Role> roles = roleAlls.Where(x => x.ParentRoleId == user.RoleId).ToList();
            List<Role> roleLastChilds = new List<Role>();
            Queue<Role> queue = new Queue<Role>(roles);
            List<Role> queueList = new List<Role>(roles);
            while (queue.Count != 0)
            {
                Role dequeue = queue.Dequeue();
                if (db.Roles.Any(x => x.ParentRoleId == dequeue.RoleId))
                {
                    List<Role> roles1 = roleAlls.Where(x => x.ParentRoleId == dequeue.RoleId).ToList();
                    foreach (Role item in roles1)
                    {
                        queue.Enqueue(item);
                    }
                }
                else
                {
                    roleLastChilds.Add(dequeue);
                }
            }

            List<UserViewModel> result = await db.Users.AsNoTracking()
                                        .Where(x => roleLastChilds.Select(y => y.RoleId).Contains(x.RoleId))
                                        .ProjectTo<UserViewModel>(mp.ConfigurationProvider).ToListAsync();

            return result;
        }
        public async Task<List<UserViewModel>> GetAllActiveByUser(string Id)
        {
            var tg = new User();
            tg = await db.Users.FindAsync(Id);
            if (tg.NguoiQuanLy == null || tg.NguoiQuanLy == "" || tg.NguoiQuanLy == string.Empty)
            {
                if (tg.RoleId == "BLD" || tg.RoleId == "ADMIN")
                {
                    var query = from ur in db.Users
                                join rl in db.Roles on ur.RoleId equals rl.RoleId
                                    //where ur.Status == true
                                orderby ur.CreatedDate descending
                                select new UserViewModel
                                {
                                    UserId = ur.UserId,
                                    UserName = ur.UserName ?? string.Empty,
                                    Email = ur.Email ?? "Email not set",
                                    FullName = ur.FullName ?? "Fullname not set",
                                    NguoiQuanLy = ur.NguoiQuanLy ?? string.Empty,
                                    Gender = ur.Gender,
                                    Avatar = ur.Avatar == null ? string.Empty : this.GetAvatarByHost(ur.Avatar),
                                    DateOfBirth = ur.DateOfBirth ?? DateTime.Parse("01/01/0001"),
                                    CreatedDate = ur.CreatedDate ?? DateTime.Parse("01/01/0001"),
                                    ModifiedDate = ur.ModifiedDate ?? DateTime.Parse("01/01/0001"),
                                    CreatedBy = ur.CreatedBy ?? string.Empty,
                                    Address = ur.Address ?? "Address not set",
                                    Title = ur.Title ?? "Không rõ",
                                    Phone = ur.Phone ?? "Phone not set",
                                    Status = ur.Status,
                                    RoleId = ur.RoleId,
                                    RoleName = rl.RoleName ?? string.Empty,
                                    IsOnline = ur.IsOnline,
                                };

                    return await query.OrderByDescending(x => x.PhongName).ThenBy(x => x.FullName).ToListAsync();
                }
                else
                {
                    var query = from ur in db.Users
                                join rl in db.Roles on ur.RoleId equals rl.RoleId
                                    //where ur.Status == true
                                where ur.NguoiQuanLy == Id || ur.UserId == Id
                                orderby ur.CreatedDate descending
                                select new UserViewModel
                                {
                                    UserId = ur.UserId,
                                    UserName = ur.UserName ?? string.Empty,
                                    Email = ur.Email ?? "Email not set",
                                    FullName = ur.FullName ?? "Fullname not set",
                                    NguoiQuanLy = ur.NguoiQuanLy ?? string.Empty,
                                    Gender = ur.Gender,
                                    Avatar = ur.Avatar == null ? string.Empty : this.GetAvatarByHost(ur.Avatar),
                                    DateOfBirth = ur.DateOfBirth ?? DateTime.Parse("01/01/0001"),
                                    CreatedDate = ur.CreatedDate ?? DateTime.Parse("01/01/0001"),
                                    ModifiedDate = ur.ModifiedDate ?? DateTime.Parse("01/01/0001"),
                                    CreatedBy = ur.CreatedBy ?? string.Empty,
                                    Address = ur.Address ?? "Address not set",
                                    Title = ur.Title ?? "Không rõ",
                                    Phone = ur.Phone ?? "Phone not set",
                                    Status = ur.Status,
                                    RoleId = ur.RoleId,
                                    RoleName = rl.RoleName ?? string.Empty,
                                    IsOnline = ur.IsOnline,
                                };

                    return await query.OrderByDescending(x => x.PhongName).ThenBy(x => x.FullName).ToListAsync();
                }
            }
            else
            {
                tg = await db.Users.FindAsync(tg.NguoiQuanLy);
                if (tg.RoleId == "BLD" || tg.RoleId == "ADMIN")
                {
                    var query = from ur in db.Users
                                join rl in db.Roles on ur.RoleId equals rl.RoleId
                                    //where ur.Status == true
                                where ur.NguoiQuanLy == Id || ur.UserId == Id
                                orderby ur.CreatedDate descending
                                select new UserViewModel
                                {
                                    UserId = ur.UserId,
                                    UserName = ur.UserName ?? string.Empty,
                                    Email = ur.Email ?? "Email not set",
                                    FullName = ur.FullName ?? "Fullname not set",
                                    NguoiQuanLy = ur.NguoiQuanLy ?? string.Empty,
                                    Gender = ur.Gender,
                                    Avatar = ur.Avatar == null ? string.Empty : this.GetAvatarByHost(ur.Avatar),
                                    DateOfBirth = ur.DateOfBirth ?? DateTime.Parse("01/01/0001"),
                                    CreatedDate = ur.CreatedDate ?? DateTime.Parse("01/01/0001"),
                                    ModifiedDate = ur.ModifiedDate ?? DateTime.Parse("01/01/0001"),
                                    CreatedBy = ur.CreatedBy ?? string.Empty,
                                    Address = ur.Address ?? "Address not set",
                                    Title = ur.Title ?? "Không rõ",
                                    Phone = ur.Phone ?? "Phone not set",
                                    Status = ur.Status,
                                    RoleId = ur.RoleId,
                                    RoleName = rl.RoleName ?? string.Empty,
                                    IsOnline = ur.IsOnline,
                                };

                    return await query.OrderByDescending(x => x.PhongName).ThenBy(x => x.FullName).ToListAsync();
                }
                else
                {
                    return null;
                }
            }


            //return null;
        }
        public async Task<List<UserViewModel>> GetBySoLuong(PagingParams pagingParams)
        {
            var tg = new User();
            tg = await db.Users.FindAsync(pagingParams.userId);
            if (tg.NguoiQuanLy == null || tg.NguoiQuanLy == "" || tg.NguoiQuanLy == string.Empty)
            {
                if (tg.RoleId == "BLD" || tg.RoleId == "ADMIN")
                {
                    if (!string.IsNullOrEmpty(pagingParams.fromDate) && !string.IsNullOrEmpty(pagingParams.toDate))
                    {
                        var query = from ur in db.Users
                                    join dt in db.Customers on ur.UserId equals dt.CreatedBy
                                    where ur.Status == true && dt.CreatedDate >= DateTime.Parse(pagingParams.fromDate)
                                    && dt.CreatedDate <= DateTime.Parse(pagingParams.toDate).AddDays(1)
                                    group ur by ur.UserId into userGroup
                                    select new UserViewModel
                                    {
                                        FullName = userGroup.Select(m => m.FullName).FirstOrDefault(),
                                        SoLuongKhachHang = userGroup.Count(),
                                    };

                        return await query.OrderByDescending(x => x.SoLuongKhachHang).ToListAsync();
                    }
                    else
                    {
                        var query = from ur in db.Users
                                    join dt in db.Customers on ur.UserId equals dt.CreatedBy
                                    where ur.Status == true
                                    group ur by ur.UserId into userGroup
                                    select new UserViewModel
                                    {
                                        FullName = userGroup.Select(m => m.FullName).FirstOrDefault(),
                                        SoLuongKhachHang = userGroup.Count(),
                                    };

                        return await query.OrderByDescending(x => x.SoLuongKhachHang).ToListAsync();
                    }

                }
                else
                {
                    if (!string.IsNullOrEmpty(pagingParams.fromDate) && !string.IsNullOrEmpty(pagingParams.toDate))
                    {
                        var query = from ur in db.Users
                                    join dt in db.Customers on ur.UserId equals dt.CreatedBy
                                    where ur.Status == true && dt.CreatedDate >= DateTime.Parse(pagingParams.fromDate)
                                    && dt.CreatedDate <= DateTime.Parse(pagingParams.toDate).AddDays(1)
                                    where ur.NguoiQuanLy == pagingParams.userId || ur.UserId == pagingParams.userId
                                    group ur by ur.UserId into userGroup
                                    select new UserViewModel
                                    {
                                        FullName = userGroup.Select(m => m.FullName).FirstOrDefault(),
                                        SoLuongKhachHang = userGroup.Count(),
                                    };

                        return await query.OrderByDescending(x => x.SoLuongKhachHang).ToListAsync();
                    }
                    else
                    {
                        var query = from ur in db.Users
                                    join dt in db.Customers on ur.UserId equals dt.CreatedBy
                                    where ur.Status == true
                                    where ur.NguoiQuanLy == pagingParams.userId || ur.UserId == pagingParams.userId
                                    group ur by ur.UserId into userGroup
                                    select new UserViewModel
                                    {
                                        FullName = userGroup.Select(m => m.FullName).FirstOrDefault(),
                                        SoLuongKhachHang = userGroup.Count(),
                                    };

                        return await query.OrderByDescending(x => x.SoLuongKhachHang).ToListAsync();
                    }

                }
            }
            else
            {
                tg = await db.Users.FindAsync(tg.NguoiQuanLy);
                if (tg.RoleId == "BLD" || tg.RoleId == "ADMIN")
                {
                    if (!string.IsNullOrEmpty(pagingParams.fromDate) && !string.IsNullOrEmpty(pagingParams.toDate))
                    {
                        var query = from ur in db.Users
                                    join dt in db.Customers on ur.UserId equals dt.CreatedBy
                                    where ur.Status == true && dt.CreatedDate >= DateTime.Parse(pagingParams.fromDate)
                                    && dt.CreatedDate <= DateTime.Parse(pagingParams.toDate).AddDays(1)
                                    where ur.NguoiQuanLy == pagingParams.userId || ur.UserId == pagingParams.userId
                                    group ur by ur.UserId into userGroup
                                    select new UserViewModel
                                    {
                                        FullName = userGroup.Select(m => m.FullName).FirstOrDefault(),
                                        SoLuongKhachHang = userGroup.Count(),
                                    };

                        return await query.OrderByDescending(x => x.SoLuongKhachHang).ToListAsync();
                    }
                    else
                    {
                        var query = from ur in db.Users
                                    join dt in db.Customers on ur.UserId equals dt.CreatedBy
                                    where ur.Status == true
                                    where ur.NguoiQuanLy == pagingParams.userId || ur.UserId == pagingParams.userId
                                    group ur by ur.UserId into userGroup
                                    select new UserViewModel
                                    {
                                        FullName = userGroup.Select(m => m.FullName).FirstOrDefault(),
                                        SoLuongKhachHang = userGroup.Count(),
                                    };

                        return await query.OrderByDescending(x => x.SoLuongKhachHang).ToListAsync();
                    }

                }
                else
                {
                    if (!string.IsNullOrEmpty(pagingParams.fromDate) && !string.IsNullOrEmpty(pagingParams.toDate))
                    {
                        var query = from ur in db.Users
                                    join dt in db.Customers on ur.UserId equals dt.CreatedBy
                                    where ur.Status == true && dt.CreatedDate >= DateTime.Parse(pagingParams.fromDate)
                                    && dt.CreatedDate <= DateTime.Parse(pagingParams.toDate).AddDays(1)
                                    where ur.UserId == pagingParams.userId
                                    group ur by ur.UserId into userGroup
                                    select new UserViewModel
                                    {
                                        FullName = userGroup.Select(m => m.FullName).FirstOrDefault(),
                                        SoLuongKhachHang = userGroup.Count(),
                                    };

                        return await query.OrderByDescending(x => x.SoLuongKhachHang).ToListAsync();
                    }
                    else
                    {
                        var query = from ur in db.Users
                                    join dt in db.Customers on ur.UserId equals dt.CreatedBy
                                    where ur.Status == true
                                    where ur.UserId == pagingParams.userId
                                    group ur by ur.UserId into userGroup
                                    select new UserViewModel
                                    {
                                        FullName = userGroup.Select(m => m.FullName).FirstOrDefault(),
                                        SoLuongKhachHang = userGroup.Count(),
                                    };

                        return await query.OrderByDescending(x => x.SoLuongKhachHang).ToListAsync();
                    }
                }
            }


            //return null;
        }
        public async Task<string> ExportExcelBaoCaoAsync(PagingParams pagingParams)
        {
            var query = from ur in db.Users
                        join dt in db.Customers on ur.UserId equals dt.CreatedBy
                        where ur.Status == true && dt.CreatedDate >= DateTime.Parse(pagingParams.fromDate)
                        && dt.CreatedDate <= DateTime.Parse(pagingParams.toDate).AddDays(1)
                        group ur by ur.UserId into userGroup
                        select new UserViewModel
                        {
                            FullName = userGroup.Select(m => m.FullName).FirstOrDefault(),
                            SoLuongKhachHang = userGroup.Count(),
                        };
            var tg = new User();
            tg = await db.Users.FindAsync(pagingParams.userId);
            if (tg.NguoiQuanLy == null || tg.NguoiQuanLy == "" || tg.NguoiQuanLy == string.Empty)
            {
                if (tg.RoleId == "BLD" || tg.RoleId == "ADMIN")
                {
                    if (!string.IsNullOrEmpty(pagingParams.fromDate) && !string.IsNullOrEmpty(pagingParams.toDate))
                    {
                        query = from ur in db.Users
                                join dt in db.Customers on ur.UserId equals dt.CreatedBy
                                where ur.Status == true && dt.CreatedDate >= DateTime.Parse(pagingParams.fromDate)
                                && dt.CreatedDate <= DateTime.Parse(pagingParams.toDate).AddDays(1)
                                group ur by ur.UserId into userGroup
                                select new UserViewModel
                                {
                                    FullName = userGroup.Select(m => m.FullName).FirstOrDefault(),
                                    SoLuongKhachHang = userGroup.Count(),
                                };
                    }
                    else
                    {
                        query = from ur in db.Users
                                join dt in db.Customers on ur.UserId equals dt.CreatedBy
                                where ur.Status == true
                                group ur by ur.UserId into userGroup
                                select new UserViewModel
                                {
                                    FullName = userGroup.Select(m => m.FullName).FirstOrDefault(),
                                    SoLuongKhachHang = userGroup.Count(),
                                };
                    }

                }
                else
                {
                    if (!string.IsNullOrEmpty(pagingParams.fromDate) && !string.IsNullOrEmpty(pagingParams.toDate))
                    {
                        query = from ur in db.Users
                                join dt in db.Customers on ur.UserId equals dt.CreatedBy
                                where ur.Status == true && dt.CreatedDate >= DateTime.Parse(pagingParams.fromDate)
                                && dt.CreatedDate <= DateTime.Parse(pagingParams.toDate).AddDays(1)
                                where ur.NguoiQuanLy == pagingParams.userId || ur.UserId == pagingParams.userId
                                group ur by ur.UserId into userGroup
                                select new UserViewModel
                                {
                                    FullName = userGroup.Select(m => m.FullName).FirstOrDefault(),
                                    SoLuongKhachHang = userGroup.Count(),
                                };
                    }
                    else
                    {
                        query = from ur in db.Users
                                join dt in db.Customers on ur.UserId equals dt.CreatedBy
                                where ur.Status == true
                                where ur.NguoiQuanLy == pagingParams.userId || ur.UserId == pagingParams.userId
                                group ur by ur.UserId into userGroup
                                select new UserViewModel
                                {
                                    FullName = userGroup.Select(m => m.FullName).FirstOrDefault(),
                                    SoLuongKhachHang = userGroup.Count(),
                                };
                    }

                }
            }
            else
            {
                tg = await db.Users.FindAsync(tg.NguoiQuanLy);
                {
                    if (tg.RoleId == "BLD" || tg.RoleId == "ADMIN")
                    {
                        if (!string.IsNullOrEmpty(pagingParams.fromDate) && !string.IsNullOrEmpty(pagingParams.toDate))
                        {
                            query = from ur in db.Users
                                    join dt in db.Customers on ur.UserId equals dt.CreatedBy
                                    where ur.Status == true && dt.CreatedDate >= DateTime.Parse(pagingParams.fromDate)
                                    && dt.CreatedDate <= DateTime.Parse(pagingParams.toDate).AddDays(1)
                                    where ur.NguoiQuanLy == pagingParams.userId || ur.UserId == pagingParams.userId
                                    group ur by ur.UserId into userGroup
                                    select new UserViewModel
                                    {
                                        FullName = userGroup.Select(m => m.FullName).FirstOrDefault(),
                                        SoLuongKhachHang = userGroup.Count(),
                                    };
                        }
                        else
                        {
                            query = from ur in db.Users
                                    join dt in db.Customers on ur.UserId equals dt.CreatedBy
                                    where ur.Status == true
                                    where ur.NguoiQuanLy == pagingParams.userId || ur.UserId == pagingParams.userId
                                    group ur by ur.UserId into userGroup
                                    select new UserViewModel
                                    {
                                        FullName = userGroup.Select(m => m.FullName).FirstOrDefault(),
                                        SoLuongKhachHang = userGroup.Count(),
                                    };
                        }

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(pagingParams.fromDate) && !string.IsNullOrEmpty(pagingParams.toDate))
                        {
                            query = from ur in db.Users
                                    join dt in db.Customers on ur.UserId equals dt.CreatedBy
                                    where ur.Status == true && dt.CreatedDate >= DateTime.Parse(pagingParams.fromDate)
                                    && dt.CreatedDate <= DateTime.Parse(pagingParams.toDate).AddDays(1)
                                    where ur.UserId == pagingParams.userId
                                    group ur by ur.UserId into userGroup
                                    select new UserViewModel
                                    {
                                        FullName = userGroup.Select(m => m.FullName).FirstOrDefault(),
                                        SoLuongKhachHang = userGroup.Count(),
                                    };
                        }
                        else
                        {
                            query = from ur in db.Users
                                    join dt in db.Customers on ur.UserId equals dt.CreatedBy
                                    where ur.Status == true
                                    where ur.UserId == pagingParams.userId
                                    group ur by ur.UserId into userGroup
                                    select new UserViewModel
                                    {
                                        FullName = userGroup.Select(m => m.FullName).FirstOrDefault(),
                                        SoLuongKhachHang = userGroup.Count(),
                                    };
                        }
                    }
                }
            }


            string _path_sample = Path.Combine(_hostingEnvironment.ContentRootPath, $"MyFile/uploaded/sample/Thong_ke_them_khach_hang.xlsx");
            string desFileName = Guid.NewGuid() + ".xlsx";
            string desFilePath = Path.Combine(_hostingEnvironment.ContentRootPath, $"MyFile/uploaded/excels/{desFileName}");
            string uploadFolder = Path.Combine(_hostingEnvironment.ContentRootPath, "MyFile/uploaded/excels");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            // Excel
            FileInfo file = new FileInfo(_path_sample);
            string dateReport = string.Format("Thống kê số lượng khách hàng đã thêm (Từ ngày {0} đến ngày {1})",
                string.IsNullOrEmpty(pagingParams.fromDate) ? string.Empty : DateTime.Parse(pagingParams.fromDate).ToString("dd/MM/yyyy"),
                string.IsNullOrEmpty(pagingParams.toDate) ? string.Empty : DateTime.Parse(pagingParams.toDate).ToString("dd/MM/yyyy"));

            using (ExcelPackage package = new ExcelPackage(file))
            {
                List<UserViewModel> list = query.ToList();
                // Open sheet1
                int totalRows = list.Count;

                // Begin row
                int begin_row = 3;

                // Open sheet1
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                // Add Row
                worksheet.InsertRow(begin_row + 1, totalRows - 1, begin_row);

                // Fill data
                int idx = begin_row;
                for (int i = 0; i < list.Count; i++)
                {
                    var it = list[i];
                    worksheet.Cells[idx, 1].Value = i + 1;
                    worksheet.Cells[idx, 2].Value = it.FullName;
                    worksheet.Cells[idx, 3].Value = it.SoLuongKhachHang;

                    idx += 1;
                }
                worksheet.Cells[1, 1].Value = dateReport.ToUpper();
                worksheet.Row(1).Style.Font.Italic = true;
                worksheet.Row(1).Style.Font.Bold = true;
                worksheet.Cells[string.Format("A{0}:B{0}", idx)].Merge = true;
                worksheet.Cells[idx, 1].Value = "Tổng số khách hàng đã thêm: ";
                worksheet.Cells[idx, 3].Value = string.Format("{0}", list.Sum(x => x.SoLuongKhachHang));
                worksheet.Row(idx).Style.Font.Bold = true;
                Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#BDD7EE");
                worksheet.Cells[string.Format("A{0}:C{0}", idx)].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[string.Format("A{0}:C{0}", idx)].Style.Fill.BackgroundColor.SetColor(colFromHex);
                worksheet.Cells[string.Format("A{0}:C{0}", idx)].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[string.Format("A{0}:C{0}", idx)].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells[idx, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                worksheet.Row(idx).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Row(idx).Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                package.SaveAs(new FileInfo(desFilePath));
                Byte[] bytes = File.ReadAllBytes(desFilePath);
                String base64String = Convert.ToBase64String(bytes);

                if (File.Exists(desFilePath))
                {
                    File.Delete(desFilePath);
                }

                return base64String;
            }
        }

        public async Task<IList<int>> GetYears()
        {
            var Years = new List<int>();
            var query = await db.Customers.GroupBy(x => x.CreatedDate.Value.Year).ToListAsync();
            foreach (var item in query)
            {
                Years.Add(item.Key);
            }
            return Years;
        }
        public async Task<IList<int>> GetAddKHMonths()
        {
            var Months = new List<int>();
            var query = await db.Customers.GroupBy(x => x.CreatedDate.Value.Month).ToListAsync();
            foreach (var item in query)
            {
                Months.Add(item.Key);
            }
            return Months;
        }

        public Task<IList<int>> GetYearsLoi()
        {
            throw new NotImplementedException();
        }

        public async Task<IList<BaoCaoThemKhachHangTheoNamParam>> GetAddKHByYear(BaoCaoThemKhachHangTheoNamParam model)
        {
            try
            {
                var query = (from dt in db.Customers
                             join ur in db.Users on dt.CreatedBy equals ur.UserId
                             where ur.Status == true && (dt.CreatedDate.Value.Year == model.Year)
                             select new CustomerViewModel
                             {
                                 Id = dt.Id,
                                 CreatedBy = dt.CreatedBy,
                                 CreatedDate = dt.CreatedDate,
                             }).ToList();
                if (!string.IsNullOrEmpty(model.UserId))
                {
                    query = query.Where(x => x.CreatedBy == model.UserId).ToList();
                }

                var querytg = from b in query
                              group b by b.CreatedDate.Value.Month into g
                              select new BaoCaoThemKhachHangTheoNamParam
                              {
                                  Thang = g.Key,
                                  TongSoKhachHang = g.Count()
                              };

                return querytg.ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<IList<BaoCaoThemKhachHangByNhanVienTheoThangParam>> GetAddKhachHangByNhanVien(BaoCaoThemKhachHangByNhanVienTheoThangParam model)
        {
            try
            {
                var query = (from dt in db.Customers
                             join ur in db.Users on dt.CreatedBy equals ur.UserId
                             where ur.Status == true && (dt.CreatedDate.Value.Year == model.Year) && (dt.CreatedDate.Value.Month == model.Month)
                             select new CustomerViewModel
                             {
                                 Id = dt.Id,
                                 CreatedDate = dt.CreatedDate,
                                 CreatedBy = ur.FullName

                             }).ToList();
                /*                if (!string.IsNullOrEmpty(model.UserId))
                                {
                                    query = query.Where(x => x.CreatedBy == model.UserId).ToList();
                                }*/

                var querytg = from b in query
                              group b by b.CreatedBy.ToString() into g
                              select new BaoCaoThemKhachHangByNhanVienTheoThangParam
                              {
                                  FullName = g.Key,
                                  TongSoKhachHang = g.Count()
                              };

                return querytg.ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<IList<BaoCaoThemKhachHangByNhanVienTheoThangParam>> GetAddKhachHangByNhanVienKD(BaoCaoThemKhachHangByNhanVienTheoThangParam model)
        {
            try
            {
                var query = (from dt in db.Customers
                             join ur in db.Users on dt.CreatedBy equals ur.UserId
                             where ur.Status == true &&
                             (dt.CreatedDate.Value.Year == model.Year) &&
                             (dt.CreatedDate.Value.Month == model.Month) &&
                             (ur.RoleId == "NVKD")
                             select new CustomerViewModel
                             {
                                 Id = dt.Id,
                                 CreatedBy = ur.FullName,
                                 CreatedDate = dt.CreatedDate,

                             }).ToList();
                /*if (!string.IsNullOrEmpty(model.UserId))
                {
                    query = query.Where(x => x.CreatedBy == model.UserId).ToList();
                }*/

                var querytg = from b in query
                              group b by b.CreatedBy.ToString() into g
                              select new BaoCaoThemKhachHangByNhanVienTheoThangParam
                              {
                                  FullName = g.Key,
                                  TongSoKhachHang = g.Count()
                              };

                return querytg.ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<List<UserViewModel>> GetAllUserById(string Id)
        {
            var tg = new User();
            tg = await db.Users.FindAsync(Id);
            if (tg.RoleId == "BLD" || tg.RoleId == "ADMIN")
            {
                var query = from ur in db.Users
                            where ur.Status == true
                            orderby ur.CreatedDate descending
                            select new UserViewModel
                            {
                                UserId = ur.UserId,
                                UserName = ur.UserName ?? string.Empty,
                                FullName = ur.FullName,
                                Title = ur.Title ?? "Không rõ",
                                Status = ur.Status,
                                RoleId = ur.RoleId,
                                IsOnline = ur.IsOnline,
                                Avatar = ur.Avatar == null ? string.Empty : this.GetAvatarByHost(ur.Avatar),
                            };

                return await query.OrderBy(x => x.FullName).ToListAsync();
            }
            else if (tg.RoleId == "PPKD")
            {
                var query = from ur in db.Users
                            where ur.Status == true
                            where ur.RoleId != "BLD" && ur.RoleId != "ADMIN"
                            orderby ur.CreatedDate descending
                            select new UserViewModel
                            {
                                UserId = ur.UserId,
                                UserName = ur.UserName ?? string.Empty,
                                FullName = ur.FullName,
                                Title = ur.Title ?? "Không rõ",
                                Status = ur.Status,
                                RoleId = ur.RoleId,
                                IsOnline = ur.IsOnline,
                                Avatar = ur.Avatar == null ? string.Empty : this.GetAvatarByHost(ur.Avatar),
                            };

                return await query.OrderBy(x => x.FullName).ToListAsync();
            }
            else
            {
                var temp = await db.Users.FirstOrDefaultAsync(x => x.NguoiQuanLy == Id);
                if (temp != null)
                {
                    var query = from ur in db.Users
                                where ur.Status == true
                                where ur.NguoiQuanLy == Id || ur.UserId == Id
                                orderby ur.CreatedDate descending
                                select new UserViewModel
                                {
                                    UserId = ur.UserId,
                                    UserName = ur.UserName ?? string.Empty,
                                    FullName = ur.FullName,
                                    Title = ur.Title ?? "Không rõ",
                                    Status = ur.Status,
                                    RoleId = ur.RoleId,
                                    IsOnline = ur.IsOnline,
                                    Avatar = ur.Avatar == null ? string.Empty : this.GetAvatarByHost(ur.Avatar),
                                };

                    return await query.OrderBy(x => x.FullName).ToListAsync();
                }
                else
                {
                    var query = from ur in db.Users
                                where ur.UserId == Id
                                select new UserViewModel
                                {
                                    UserId = ur.UserId,
                                    UserName = ur.UserName ?? string.Empty,
                                    FullName = ur.FullName,
                                    Title = ur.Title ?? "Không rõ",
                                    Status = ur.Status,
                                    RoleId = ur.RoleId,
                                    IsOnline = ur.IsOnline,
                                    Avatar = ur.Avatar == null ? string.Empty : this.GetAvatarByHost(ur.Avatar),
                                };

                    return await query.ToListAsync();
                }
            }
            //return null;
        }
        public async Task<bool> CheckQuyen(string userId)
        {
            var tg = new User();
            tg = await db.Users.FindAsync(userId);
            if (tg != null)
            {
                if (tg.RoleId == "BLD" || tg.RoleId == "ADMIN" || tg.RoleId == "PPKD")
                {
                    return true;

                }
                else
                {

                    var temp = await db.Users.FirstOrDefaultAsync(x => x.NguoiQuanLy == userId);
                    if (temp != null) return true;
                    else return false;
                }

            }
            else
                return false;
            //if (tg.NguoiQuanLy == null || tg.NguoiQuanLy == "" || tg.NguoiQuanLy == string.Empty)
            //{
            //    if (tg.RoleId == "BLD" || tg.RoleId == "ADMIN" || tg.RoleId== "PPKD")
            //        return true;
            //    else
            //        return false;
            //}   
            //else
            //{
            //    tg = await db.Users.FindAsync(tg.NguoiQuanLy);
            //    if (tg.RoleId == "BLD" || tg.RoleId == "ADMIN" || tg.RoleId == "PPKD")
            //    {
            //        return true;
            //    }
            //    else
            //        return false;
            //}

        }
    }

}
