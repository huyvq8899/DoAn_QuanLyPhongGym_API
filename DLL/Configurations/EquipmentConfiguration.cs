using DLL.Entity;
using DLL.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DLL.Configurations
{
    public class EquipmentConfiguration : DbEntityConfiguration<Equipment>
    {
        public override void Configure(EntityTypeBuilder<Equipment> entity)
        {
            entity.HasKey(c => new { c.Id });
        }
    }
}
