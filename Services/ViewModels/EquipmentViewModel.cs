using DLL.LogEntity;
using System;

namespace Services.ViewModels
{
    public class EquipmentViewModel : AuditableEntity
    {
        public string Id { get; set; }
        public string Name { set; get; }
        public int Amount { set; get; }
        public string Description { set; get; }
        public string UserId { set; get; }
    }
}
