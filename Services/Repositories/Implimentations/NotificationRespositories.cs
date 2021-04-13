
using AutoMapper;
using DLL;
using DLL.Entity;
using ManagementServices.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Interfaces;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repositories.Implimentations
{
    public class NotificationRespositories : INotificationRespositories
    {
        Datacontext _db;
        IMapper _mp;
        IHttpContextAccessor _IHttpContextAccessor;
        public NotificationRespositories(Datacontext datacontext, IMapper mapper, IHttpContextAccessor IHttpContextAccessor)
        {
            this._db = datacontext;
            this._mp = mapper;
            _IHttpContextAccessor = IHttpContextAccessor;
        }

        public async Task<bool> Delete(string id)
        {
            var entity = await _db.Notifications.FirstOrDefaultAsync(x => x.NotificationId == id);
            _db.Notifications.Remove(entity);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<int> GetAllUnView(string role, string xeid, string id)
        {
            var entity = await _db.Notifications.Where(x => x.View == false && x.Content == role && (role == "NVKD" ? (x.Link == id) : true) && (role == "PPKD" ? (x.Link == id) : true)).CountAsync();
            return entity;
        }
        public async Task<int> DeleteById(string Id)
        {
            var entity = await _db.Notifications.FirstOrDefaultAsync(x => x.NotificationId == Id);
            _db.Notifications.Remove(entity);
            var rs = await _db.SaveChangesAsync();
            return rs;
        }
        public async Task<PagedList<NotificationViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            var tg = new User();
            tg = await _db.Users.FindAsync(pagingParams.userId);
            var query = from kh in _db.Notifications

                            //join cv in _db.CongViecs on kh.CongViecId equals cv.CongViecId into tmpCV
                            //from cv in tmpCV.DefaultIfEmpty()
                            //join hd in _db.MainHopDongs on kh.MainHopDongId equals hd.MainHopDongId into tmpHD
                            //from hd in tmpHD.DefaultIfEmpty()
                        join dhtt in _db.Cards on kh.CardId equals dhtt.Id into tmpCards
                        from dhtt in tmpCards.DefaultIfEmpty()
                        orderby kh.CreatedDate descending
                        select new NotificationViewModel
                        {
                            NotificationId = kh.NotificationId,
                            NotificationName = kh.NotificationName,
                            Content = kh.Content,
                            Link = kh.Link,
                            CardId = kh.CardId,
                            Type = kh.Type,
                            Read = kh.Read,
                            //CongViecViewModel = _mp.Map<CongViecViewModel>(cv),
                            CardViewM = dhtt == null ? null : _mp.Map<CardViewModel>(dhtt),
                            CreatedDate = kh.CreatedDate,
                            CreatedBy = kh.CreatedBy,
                            ModifiedDate = kh.ModifiedDate,
                            ModifiedBy = kh.ModifiedBy,
                            Status = kh.Status,
                            CreatedByUser = (from ur in _db.Users
                                             where ur.UserId == kh.CreatedBy
                                             select new UserViewModel
                                             {
                                                 UserId = ur.UserId,
                                                 UserName = ur.UserName ?? string.Empty,
                                                 Email = ur.Email ?? "Email not set",
                                                 FullName = ur.FullName ?? "Fullname not set",
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
                                             }).FirstOrDefault()

                        };
            return await PagedList<NotificationViewModel>.CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }
        public async Task<NotificationViewModel> GetById(string id)
        {
            var query = from kh in _db.Notifications
                        where kh.NotificationId == id
                        //join cv in _db.CongViecs on kh.CongViecId equals cv.CongViecId into tmpCV
                        //from cv in tmpCV.DefaultIfEmpty()
                        //join hd in _db.MainHopDongs on kh.MainHopDongId equals hd.MainHopDongId into tmpHD
                        //from hd in tmpHD.DefaultIfEmpty()
                        join dhtt in _db.Cards on kh.CardId equals dhtt.Id into tmpCards
                        from dhtt in tmpCards.DefaultIfEmpty()
                        orderby kh.CreatedDate descending
                        select new NotificationViewModel
                        {
                            NotificationId = kh.NotificationId,
                            NotificationName = kh.NotificationName,
                            Content = kh.Content,
                            Link = kh.Link,
                            CardId = kh.CardId,
                            Type = kh.Type,
                            Read = kh.Read,
                            View = kh.View,
                            //CongViecViewModel = _mp.Map<CongViecViewModel>(cv),
                            CardViewM = dhtt == null ? null : _mp.Map<CardViewModel>(dhtt),
                            CreatedDate = kh.CreatedDate,
                            CreatedBy = kh.CreatedBy,
                            ModifiedDate = kh.ModifiedDate,
                            ModifiedBy = kh.ModifiedBy,
                            Status = kh.Status,
                            CreatedByUser = (from ur in _db.Users
                                             where ur.UserId == kh.CreatedBy
                                             select new UserViewModel
                                             {
                                                 UserId = ur.UserId,
                                                 UserName = ur.UserName ?? string.Empty,
                                                 Email = ur.Email ?? "Email not set",
                                                 FullName = ur.FullName ?? "Fullname not set",
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
                                             }).FirstOrDefault()

                        };

            //var entity = await _db.Notifications.FirstOrDefaultAsync(x => x.NotificationId == id);
            //var model = _mp.Map<NotificationViewModel>(entity);
            var model = await query.FirstOrDefaultAsync();
            return model;
        }

        public async Task<string> Insert(NotificationViewModel model)
        {
            var entity = _mp.Map<Notification>(model);
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedDate = entity.CreatedDate;
            entity.CreatedBy = model.ActionUser.UserId;
            entity.ModifiedBy = entity.CreatedBy;
            entity.Status = true;
            await _db.Notifications.AddAsync(entity);
            var rs = await _db.SaveChangesAsync() > 0;
            if (!rs) return "";
            return entity.NotificationId;
        }

        public async Task<bool> Update(NotificationViewModel model)
        {
            var entity = _mp.Map<Notification>(model);
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = model.ActionUser.UserId;
            _db.Notifications.Update(entity);
            return await _db.SaveChangesAsync() > 0;
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

        public async Task<int> GetNoticNotViewByUser(string id)
        {
            var query = from kh in _db.Notifications
                        orderby kh.CreatedDate descending
                        where kh.Link.Contains(id) && kh.View.Value == false
                        select new NotificationViewModel
                        {
                            NotificationId = kh.NotificationId
                        };
            return query.Count();
        }

        public async Task<bool> UpdateViewNotic(string id)
        {
            var query = from kh in _db.Notifications
                        orderby kh.CreatedDate descending
                        where kh.Link.Contains(id) && kh.View == false
                        select new NotificationViewModel
                        {
                            NotificationId = kh.NotificationId,
                            NotificationName = kh.NotificationName,
                            Content = kh.Content,
                            Link = kh.Link,
                            CardId = kh.CardId,
                            Type = kh.Type,
                            Read = kh.Read,
                            View = kh.View,
                            CreatedDate = kh.CreatedDate,
                            CreatedBy = kh.CreatedBy,
                            ModifiedDate = kh.ModifiedDate,
                            ModifiedBy = kh.ModifiedBy,
                            Status = kh.Status,
                        };
            foreach (var item in query)
            {
                item.View = true;
                var entity = _mp.Map<Notification>(item);
                entity.ModifiedDate = DateTime.Now;
                _db.Notifications.Update(entity);
            }
            if (query.Count() > 0) return await _db.SaveChangesAsync() > 0;
            else return true;
        }

        public async Task<bool> DeleteNoticByUser(string id)
        {
            var list = await _db.Notifications.Where(x => x.Content == id).ToListAsync();
            _db.RemoveRange(list);
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
