using DLL.Entity;
using DLL.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DLL.Configurations
{
    public class KhachHangLogConfiguration : DbEntityConfiguration<KhachHangLog>
    {
        public override void Configure(EntityTypeBuilder<KhachHangLog> entity)
        {
            entity.HasKey(c => new { c.Id });
            entity.HasOne<Customer>(u => u.Customer)
               .WithMany(s => s.KhachHangLogs)
               .HasForeignKey(sc => sc.CustomerId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
