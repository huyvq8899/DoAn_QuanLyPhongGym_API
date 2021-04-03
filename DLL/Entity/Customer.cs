using DLL.LogEntity;
using System;
using System.Collections.Generic;

namespace DLL.Entity
{
    public class Customer : AuditableEntity
    {
        public string Id { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public DateTime? DoB { get; set; }
        public String JobId { get; set; }
        public string NumberPhone { get; set; }
        public string Note { get; set; }
        public string Email { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public string HealthStatus { get; set; }
        public DateTime ToDate { set; get; }
        public DateTime FromDate { get; set; }
        public List<KhachHangLog> KhachHangLogs { get; set; }
        public List<Card> Cards { get; set; }
        public Job Job { get; set; }
    }
}
