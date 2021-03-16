using DLL.LogEntity;
using System.Collections.Generic;

namespace DLL.Entity
{
    public class CardType : AuditableEntity
    {

        public CardType() : base() { }
        public string Id { get; set; }
        public string NameType { set; get; }
        public string Description { set; get; }
        public List<Card> Cards { get; set; }
    }
}