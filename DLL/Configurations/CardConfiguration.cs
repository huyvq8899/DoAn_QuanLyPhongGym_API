using DLL.Entity;
using DLL.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DLL.Configurations
{
    public class CardConfiguration : DbEntityConfiguration<Card>
    {
        public override void Configure(EntityTypeBuilder<Card> entity)
        {
            entity.HasKey(c => new { c.Id });
            entity.HasOne<CardType>(u => u.CardType)
               .WithMany(s => s.Cards)
               .HasForeignKey(sc => sc.CardTypeId)
               .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne<Facility>(u => u.Facility)
              .WithMany(s => s.Cards)
              .HasForeignKey(sc => sc.FacilityId)
              .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne<Service>(u => u.Service)
            .WithMany(s => s.Cards)
            .HasForeignKey(sc => sc.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne<User>(u => u.User)
           .WithMany(s => s.Cards)
           .HasForeignKey(sc => sc.UserId)
           .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne<Customer>(u => u.Customer)
         .WithMany(s => s.Cards)
         .HasForeignKey(sc => sc.CustomerId)
         .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
