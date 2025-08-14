using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Million.BackEnd.Domain.Common.Contracts.Persistence;

namespace Million.BackEnd.Infrastructure.Common.Persistence.Interceptors
{
    public class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (eventData.Context == null)
            {
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            foreach (EntityEntry<ISoftDeletable> item in from e in eventData.Context.ChangeTracker.Entries<ISoftDeletable>()
                                                         where e.State == EntityState.Deleted
                                                         select e)
            {
                item.State = EntityState.Modified;
                item.Entity.ApplySoftDelete();
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
