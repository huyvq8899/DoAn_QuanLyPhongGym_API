using DLL.LogEntity;
using System.Collections.Generic;

namespace DLL.Entity
{
    public class Function : AuditableEntity
    {
        public string FunctionId { get; set; }
        public string FunctionName { get; set; }
        public string Description { get; set; }

        public List<Function_User> Function_Users { get; set; }
    }
}
