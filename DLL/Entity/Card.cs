using DLL.LogEntity;
using System;

namespace DLL.Entity
{
    public class Card : AuditableEntity
    {
        public string Id { get; set; }
        public string CardCode { get; set; }
        public String CardTypeId { set; get; }
        public String CustomerId { set; get; }
        public String UserId { set; get; }
        public String FacilityId { get; set; }
        public String ServiceId { get; set; }
        public String FacilityName { get; set; }
        public DateTime? ToDate{ set; get; }
        public DateTime? FromDate { get; set; }
        // public string Note { set; get; }
        public decimal? Price { set; get; }
        public CardType CardType { get; set; }
        public Facility Facility { get; set; }
        public Service Service { get; set; }
        public User User { get; set; }
        public Customer Customer { get; set; }
    }
}