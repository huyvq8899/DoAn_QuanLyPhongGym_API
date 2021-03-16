using DLL.LogEntity;
using System.Collections.Generic;

namespace DLL.Entity
{
    public class Facility : AuditableEntity
    {
        public string Id { get; set; }
        public string FacilityName { get; set; }
        public string Address { get; set; }
        public List<Card> Cards { get; set; }
    }
}