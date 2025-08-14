using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Million.BackEnd.Domain.Common.Contracts.Persistence;

namespace Million.BackEnd.Infrastructure.Common.Persistence.Interceptors
{
    public class UpdateAuditableEntitiesInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default(CancellationToken))
        {
            DbContext context = eventData.Context;
            if (context == null)
            {
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            foreach (EntityEntry<IAuditable> item in context.ChangeTracker.Entries<IAuditable>())
            {
                if (item.State == EntityState.Added)
                {
                    item.Property((IAuditable x) => x.CreatedOnUtc).CurrentValue = DateTime.UtcNow;
                }

                if (item.State == EntityState.Modified)
                {
                    item.Property((IAuditable x) => x.UpdatedOnUtc).CurrentValue = DateTime.UtcNow;
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
