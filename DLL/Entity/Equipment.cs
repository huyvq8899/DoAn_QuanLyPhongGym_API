using DLL.LogEntity;
using System;

namespace DLL.Entity
{
    public class Equipment : AuditableEntity
    {
        public string Id { get; set; }
        public string Name { set; get; }
        public int Amount { set; get; }
        public string Description { set; get; }
    }
}
