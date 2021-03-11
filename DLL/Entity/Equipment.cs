using DLL.LogEntity;
using System;

namespace DLL.Entity
{
    public class Equipment : AuditableEntity
    {
        public Equipment() : base() { }
        public string Name { set; get; }
        public int Amount { set; get; }
        public string Description { set; get; }
        public String UserId { set; get; }
        public User User { get; set; }
    }
}
