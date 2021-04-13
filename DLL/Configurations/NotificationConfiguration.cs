using DLL.Entity;
using DLL.Extentions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DLL.Configurations
{
    public class NotificationConfiguration : DbEntityConfiguration<Notification>
    {
        public override void Configure(EntityTypeBuilder<Notification> entity)
        {
            entity.HasKey(c => new { c.NotificationId });
        }
    }
}
