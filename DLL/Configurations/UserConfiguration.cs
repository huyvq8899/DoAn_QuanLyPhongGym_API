using DLL.Entity;
using DLL.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DLL.Configurations
{
    public class UserConfiguration : DbEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasKey(c => new { c.UserId });

            entity.HasOne<Role>(u => u.Role)
               .WithMany(s => s.Users)
               .HasForeignKey(sc => sc.RoleId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
