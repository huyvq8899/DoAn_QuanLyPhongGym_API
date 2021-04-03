using DLL.Entity;
using DLL.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DLL.Configurations
{
    public class CustomerConfiguration : DbEntityConfiguration<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> entity)
        {
            entity.ToTable("Customers");
            entity.HasKey(c => new { c.Id });
            entity.HasOne(u => u.Job)
        .WithMany(s => s.Customers)
        .HasForeignKey(sc => sc.JobId)
        .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
