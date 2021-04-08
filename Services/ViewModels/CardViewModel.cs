using DLL.LogEntity;
using System;

namespace Services.ViewModels
{
    public class CardViewModel : AuditableEntity
    {
        public string Id { get; set; }
        public string CardCode { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public String CardTypeId { set; get; }
        public String CustomerId { set; get; }
        public DateTime? ToDate { set; get; }
        public DateTime? FromDate { get; set; }
        public string NumberPhone { get; set; }
        public string NameType { set; get; }
        public String UserId { set; get; }
        public String FacilityId { get; set; }
        public string FacilityName { get; set; }
        public String ServiceId { get; set; }
        public string ServiceName { get; set; }
        public decimal? Price { set; get; }
        public string Note { set; get; }
        public string ToDateName { set; get; }
        public string FromDateName { get; set; }
        public string DoBName { get; set; }
        public string Job { get; set; }
        public string Email { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public string HealthStatus { get; set; }
        public string NguoiThem { get; set; }
        public string CreateDateName { get; set; }
        public decimal Money { get; set; }
        public decimal TongDoanhThu { get; set; }
        public string FullName { get; internal set; }
    }
}
