using DLL.LogEntity;

namespace Services.ViewModels
{
    public class Function_UserViewModel : AuditableEntity
    {
        public string Function_UserId { get; set; }
        public string FunctionId { get; set; }
        public string UserId { get; set; }

        public FunctionViewModel Function { get; set; }
        public UserViewModel User { get; set; }
    }
}
