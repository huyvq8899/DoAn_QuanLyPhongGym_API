using DLL.LogEntity;
using System.Collections.Generic;

namespace Services.ViewModels
{
    public class FunctionViewModel : AuditableEntity
    {
        public string FunctionId { get; set; }
        public string FunctionName { get; set; }
        public string Description { get; set; }

        public List<Function_UserViewModel> Function_Users { get; set; }
    }
}
