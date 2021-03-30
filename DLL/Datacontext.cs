using DLL.Configurations;
using DLL.Entity;
using DLL.Extentions;
using DLL.LogEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace DLL
{
    public class Datacontext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Datacontext(DbContextOptions<Datacontext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Function> Functions { get; set; }
        public DbSet<Function_User> Function_Roles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<CardType> CardTypes { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<KhachHangLog> KhachHangLogs { get; set; }


        public virtual DbSet<RetrieveOrderRecord> RetrieveOrderRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.AddConfiguration(new RoleConfiguration());
            modelBuilder.AddConfiguration(new UserConfiguration());
            modelBuilder.AddConfiguration(new FunctionConfiguration());
            modelBuilder.AddConfiguration(new Function_UserConfiguration());
            modelBuilder.AddConfiguration(new CustomerConfiguration());
            modelBuilder.AddConfiguration(new CardConfiguration());
            modelBuilder.AddConfiguration(new CardTypeConfiguration());
            modelBuilder.AddConfiguration(new EquipmentConfiguration());
            modelBuilder.AddConfiguration(new ServiceConfiguration());
            modelBuilder.Entity<RetrieveOrderRecord>()
           .HasKey(o => o.STT);
        }

        public override int SaveChanges()
        {
            UpdateAuditEntities();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateAuditEntities();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            UpdateAuditEntities();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditEntities();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditEntities()
        {
            IEnumerable<EntityEntry> entities = ChangeTracker.Entries().Where(x => x.State == EntityState.Added ||
                                                                                    x.State == EntityState.Modified);

            string nameIdentifier = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string currentUserId = null;
            if (!string.IsNullOrEmpty(nameIdentifier))
            {
                currentUserId = nameIdentifier;
            }

            foreach (EntityEntry item in entities)
            {
                IAuditableEntity changedOrAddedItem = item.Entity as IAuditableEntity;
                DateTime now = DateTime.Now;

                if (changedOrAddedItem != null)
                {
                    if (item.State == EntityState.Added)
                    {
                        if (changedOrAddedItem.CreatedBy == null && currentUserId != null)
                        {
                            changedOrAddedItem.CreatedBy = currentUserId;
                        }

                        changedOrAddedItem.CreatedDate = now;
                    }

                    if (changedOrAddedItem.ModifiedBy == null && currentUserId != null)
                    {
                        changedOrAddedItem.ModifiedBy = currentUserId;
                    }

                    changedOrAddedItem.ModifiedDate = now;
                }
            }
        }
    }
}
