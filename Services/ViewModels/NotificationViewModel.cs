using DLL.LogEntity;

namespace Services.ViewModels
{
    public class NotificationViewModel : AuditableEntity
    {
        public string NotificationId { get; set; }
        public string NotificationName { get; set; }
        public string Content { get; set; }
        public string XeId { get; set; }
        public string Link { get; set; } // ghi list id những thằng nhận notic
        public int? Type { get; set; } // // 1 công việc,2 hợp đồng ,3 phản hồi,4 MainHopDong
        public bool? Read { get; set; } // chưa đọc =0 đã đọc =1
        public bool? View { get; set; } // 1 xem  0 chưa xem
        public UserViewModel ActionUser { get; set; }
        public UserViewModel CreatedByUser { get; set; }
        public UserViewModel ModifiByUser { get; set; }
        public string CardId { get; set; }
        public CardViewModel CardViewM { get; set; }
    }
}
