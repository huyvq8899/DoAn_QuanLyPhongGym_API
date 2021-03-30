using DLL.LogEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.ViewModels
{
  public class KhachHangLogViewModel : AuditableEntity
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string TenTruongThayDoi { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string DienGiai { get; set; }
        public string DuLieuCu { get; set; }
        public string DuLieuMoi { get; set; }
        public string NguoiThem { get; set; }
    }
}
