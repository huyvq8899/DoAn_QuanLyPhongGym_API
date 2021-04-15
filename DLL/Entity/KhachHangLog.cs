using DLL.LogEntity;
using System.Collections.Generic;

namespace DLL.Entity
{
    public class KhachHangLog : AuditableEntity
    {
        public string Id { get; set; }
        public string CustomerCode { get; set; }
        public string TenTruongThayDoi { get; set; }
        public string DienGiai { get; set; }
        public string DuLieuCu { get; set; }
        public string DuLieuMoi { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
