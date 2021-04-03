using DLL.Entity;
using DLL.Extentions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DLL.Configurations
{
    public class JobConfiguration : DbEntityConfiguration<Job>
    {
        public override void Configure(EntityTypeBuilder<Job> entity)
        {
            entity.HasKey(c => new { c.Id });
         
        }
    }
}
