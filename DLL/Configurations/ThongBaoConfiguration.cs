using DLL.Entity;
using DLL.Extentions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DLL.Configurations
{
    public class ThongBaoConfiguration : DbEntityConfiguration<ThongBao>
    {
        public override void Configure(EntityTypeBuilder<ThongBao> entity)
        {
            entity.HasKey(x => x.ThongBaoId);
        }
    }
}
