using DLL.Entity;
using DLL.Extentions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DLL.Configurations
{
    public class FacilityConfiguration : DbEntityConfiguration<Facility>
    {
        public override void Configure(EntityTypeBuilder<Facility> entity)
        {
            entity.HasKey(c => new { c.Id });
        }
    }
}
