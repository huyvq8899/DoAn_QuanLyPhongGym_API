using DLL.LogEntity;
using System.Collections.Generic;

namespace DLL.Entity
{
    public class Facility : AuditableEntity
    {
        public string Id { get; set; }
        public string FacilityName { get; set; }
        public string TaxCode { get; set; }
        public string Address { get; set; }
        public string EstablishedDay { get; set; }
        public string NumberPhone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public List<Card> Cards { get; set; }
    }
}