using DLL.LogEntity;
using System;
using System.Collections.Generic;

namespace DLL.Entity
{
    public class User : AuditableEntity
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserName { get; set; } // lấy làm tag
        public string Email { get; set; }
        public string FullName { get; set; }
        public string NguoiQuanLy { get; set; }
        public int? Gender { get; set; } // 1 nam 0 nữ còn lại không xác định
        public string Avatar { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string RoleId { get; set; }
        public string Title { get; set; }
        public Role Role { get; set; }
        public bool? IsOnline { get; set; }
        public int? LoginCount { get; set; }

        public List<Function_User> Function_Users { get; set; }
        public List<Card> Cards { get; set; }
        public List<Equipment> Equipments { get; set; }
    }
}
