using RandomPairer.Common.RequestContext;
using RandomPairer.Common.TimeService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RandomPairer.Dal.Context
{
    public partial class RandomPairerDbContext
    {
        private readonly IRequestContext requestContext;
        private readonly ITimeService timeService;

        public RandomPairerDbContext(DbContextOptions<RandomPairerDbContext> options, IRequestContext requestContext, ITimeService timeService) : base(options)
        {
            this.requestContext = requestContext;
            this.timeService = timeService;
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.RegisterSoftDeleteQueryFilters();
        }

        public override int SaveChanges()
        {
            SaveChangesCore();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SaveChangesCore();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SaveChangesCore()
        {
            var utcNow = timeService.UtcNow;

            var deletedEntries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is ISoftDeletableEntity)
                .Where(e => e.State == EntityState.Deleted);

            UpdateDeletedEntries(deletedEntries);

            var modifiedEntries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is IAuditableEntity)
                .Where(e => e.State == EntityState.Modified);

            UpdateModifiedEntires(modifiedEntries, utcNow);

            var addedEntries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is IAuditableEntity)
                .Where(e => e.State == EntityState.Added);

            UpdateAddedEntries(addedEntries, utcNow);
        }

        private void UpdateDeletedEntries(IEnumerable<EntityEntry> deletedEntries)
        {
            foreach (var entry in deletedEntries)
            {
                ((ISoftDeletableEntity)entry.Entity).IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }

        private void UpdateAddedEntries(IEnumerable<EntityEntry> addedEntries, DateTime utcNow)
        {
            foreach (var entry in addedEntries)
            {
                ((IAuditableEntity)entry.Entity).CreatedAt = utcNow;
                ((IAuditableEntity)entry.Entity).LastModAt = utcNow;
                ((IAuditableEntity)entry.Entity).CreatedBy = requestContext.CurrentUserAd;
                ((IAuditableEntity)entry.Entity).LastModBy = requestContext.CurrentUserAd;
            }
        }

        private void UpdateModifiedEntires(IEnumerable<EntityEntry> modifiedEntries, DateTime utcNow)
        {
            foreach (var entry in modifiedEntries)
            {
                ((IAuditableEntity)entry.Entity).LastModAt = utcNow;
                ((IAuditableEntity)entry.Entity).LastModBy = requestContext.CurrentUserAd;
            }
        }
    }
}