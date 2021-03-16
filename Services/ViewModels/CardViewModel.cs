using DLL.LogEntity;
using System;

namespace Services.ViewModels
{
    public class CardViewModel : AuditableEntity
    {
        public string Id { get; set; }
        public string CardCode { get; set; }
        public String CardTypeId { set; get; }
        public String CustomerId { set; get; }
        public String UserId { set; get; }
        public String FacilityId { get; set; }
        public String ServiceId { get; set; }
        public String FacilityName { get; set; }
        public decimal Price { set; get; }
        public int NumOfDay { set; get; }
        public string Note { set; get; }
    }
}
