using DLL.LogEntity;
using System.Collections.Generic;

namespace DLL.Entity
{
    public  class Job : AuditableEntity
    {
        public string Id { get; set; }
        public string JobName { set; get; }
        public string PlaceWork { get; set; }
        public string Description { set; get; }
        public List<Customer> Customers { get; set; }
    }
}
