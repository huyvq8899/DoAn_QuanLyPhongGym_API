using System;

namespace DLL.LogEntity
{
    public class AuditableEntity : IAuditableEntity
    {
        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? Status { get; set; }
    }
}
