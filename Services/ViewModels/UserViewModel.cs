using DLL.LogEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.ViewModels
{
    public class UserViewModel : AuditableEntity
    {
        public UserViewModel()
        {
            Status = true;
            IsOnline = false;
        }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhongBanPhongId { get; set; }
        public string UserName { get; set; } // lấy làm tag
        public string Email { get; set; }
        public string FullName { get; set; }
        public string NguoiQuanLy { get; set; }
        public int? Gender { get; set; } // 1 nam 0 nữ còn lại không xác định
        public string Avatar { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Title { get; set; }
        public string RoleId { get; set; }
        public RoleViewModel Role { get; set; }
        public bool? IsOnline { get; set; }
        public int? LoginCount { get; set; }

        public string RoleName { get; set; }
        public string PhongName { get; set; }
        public int? SoLuongKhachHang { get; set; }
    }
}
