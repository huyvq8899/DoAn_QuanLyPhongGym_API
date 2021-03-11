using DLL.Entity;
using DLL.Extentions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DLL.Configurations
{
    public class CardTypeConfiguration : DbEntityConfiguration<CardType>
    {
        public override void Configure(EntityTypeBuilder<CardType> entity)
        {
            entity.HasKey(c => new { c.Id });
        }
    }
}
