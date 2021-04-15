using DLL.Enums;
using DLL.LogEntity;

namespace DLL.Entity
{
    public class ThongBao : AuditableEntity
    {
        public string ThongBaoId { get; set; }
        public string NguoiGuiId { get; set; }
        public string NguoiNhanId { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public string Url { get; set; }
        public LoaiThongBao LoaiThongBao { get; set; }
        public string ObjectId { get; set; }
        public bool? IsRead { get; set; }
        public bool? IsOpend { get; set; }
    }
}
