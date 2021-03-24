using DLL.LogEntity;

namespace Services.ViewModels
{
    public class ServiceViewModel : AuditableEntity
    {
        public string Id { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public decimal Money { get; set; }
    }
}
