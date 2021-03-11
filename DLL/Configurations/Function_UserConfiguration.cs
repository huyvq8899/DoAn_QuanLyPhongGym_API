using DLL.Entity;
using DLL.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DLL.Configurations
{
    public class Function_UserConfiguration : DbEntityConfiguration<Function_User>
    {
        public override void Configure(EntityTypeBuilder<Function_User> entity)
        {
            entity.HasKey(c => new { c.Function_UserId });

            entity.HasOne(u => u.Function)
               .WithMany(s => s.Function_Users)
               .HasForeignKey(sc => sc.FunctionId)
               .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(u => u.User)
               .WithMany(s => s.Function_Users)
               .HasForeignKey(sc => sc.UserId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
