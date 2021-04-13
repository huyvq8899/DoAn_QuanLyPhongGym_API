using DLL.LogEntity;

namespace DLL.Entity
{
    public class Notification : AuditableEntity
    {
        public string NotificationId { get; set; }
        public string NotificationName { get; set; }
        public string Content { get; set; }
        public string Link { get; set; } // ghi list id những thằng nhận notic
        public int? Type { get; set; } // // 1 tao don bh hang tm , 2 tao don bh cn , 3 tao don mua hang tm , 4 tao don mh cn 
        public bool? Read { get; set; } // chưa đọc =0 đã đọc =1
        public bool? View { get; set; } // 1 xem  0 chưa xem
        public string CardId { get; set; }
    }
}