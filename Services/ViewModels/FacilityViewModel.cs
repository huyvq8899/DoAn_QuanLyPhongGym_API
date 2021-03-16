using DLL.LogEntity;

namespace Services.ViewModels
{
    public class FacilityViewModel : AuditableEntity
    {
        public string Id { get; set; }
        public string FacilityName { get; set; }
        public string Address { get; set; }
    }
}
