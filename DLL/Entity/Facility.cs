using DLL.LogEntity;
using System.Collections.Generic;

namespace DLL.Entity
{
    public class Facility : AuditableEntity
    {
        public Facility() : base() { }
        public string FacilityName { get; set; }
        public string Address { get; set; }
        public List<Card> Cards { get; set; }
    }
}