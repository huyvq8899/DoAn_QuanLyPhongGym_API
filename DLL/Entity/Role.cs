using DLL.LogEntity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DLL.Entity
{
    public class Role : AuditableEntity
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<User> Users { get; set; }
        public string ParentRoleId { get; set; }
    }
}
