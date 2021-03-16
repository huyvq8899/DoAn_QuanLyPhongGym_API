using DLL.LogEntity;
using System.Collections.Generic;

namespace DLL.Entity
{
    public class Service : AuditableEntity
    {
        public string Id { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public decimal Money { get; set; }
        public List<Card> Cards { get; set; }
    }
}