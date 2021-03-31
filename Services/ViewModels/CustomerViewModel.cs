using DLL.LogEntity;
using System;

namespace Services.ViewModels
{
    public class CustomerViewModel : AuditableEntity
    {
        public string Id { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public DateTime? DoB { get; set; }
        public string DoBName { get; set; }
        public string Job { get; set; }
        public string NumberPhone { get; set; }
        public string Note { get; set; }
        public string Email { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public string HealthStatus { get; set; }
        public string CreateDateName { get; set; }
        public string NguoiThem { get; set; }
    }
}
