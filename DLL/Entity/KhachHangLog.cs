using DLL.LogEntity;

namespace DLL.Entity
{
    public class KhachHangLog : AuditableEntity
    {
        public string Id { get; set; }
        public string KhachHangId { get; set; }
        public string TenTruongThayDoi { get; set; }
        public string DienGiai { get; set; }
        public string DuLieuCu { get; set; }
        public string DuLieuMoi { get; set; }
    }
}
