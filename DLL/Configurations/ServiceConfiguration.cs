using DLL.Entity;
using DLL.Extentions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DLL.Configurations
{
    public class ServiceConfiguration : DbEntityConfiguration<Service>
    {
        public override void Configure(EntityTypeBuilder<Service> entity)
        {
            entity.HasKey(c => new { c.Id });
        }
    }
}
