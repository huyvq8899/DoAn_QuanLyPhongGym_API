using DLL.Enums;
using DLL.LogEntity;

namespace Services.ViewModels
{
    public class ThongBaoViewModel : AuditableEntity
    {
        public string ThongBaoId { get; set; }
        public string NguoiGuiId { get; set; }
        public string NguoiNhanId { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public string Url { get; set; }
        public LoaiThongBao LoaiThongBao { get; set; }
        public string TenLoaiThongBao { get; set; }
        public string ObjectId { get; set; }
        public bool? IsRead { get; set; }
        public bool? IsOpend { get; set; }

        public string UserNameNguoiGui { get; set; }
        public string FullNameNguoiGui { get; set; }
        public string UserNameNguoiNhan { get; set; }
        public string FullNameNguoiNhan { get; set; }
        public string PdfPath { get; set; }
    }
}
