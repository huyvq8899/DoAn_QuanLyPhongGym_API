using DLL.LogEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.ViewModels
{
   public class CardTypeViewModel : AuditableEntity
    {
        public string Id { get; set; }
        public string NameType { set; get; }
        public string Description { set; get; }
    }
}
