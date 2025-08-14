using Microsoft.Extensions.Options;
using Million.BackEnd.Domain.Common.Contracts.Persistence;
using MongoDB.Driver;

namespace Million.BackEnd.Infrastructure.Common.Persistence
{
    public class UnitOfWork(IMongoClient client, IOptions<PersistenceSettings> settings) : IUnitOfWork
    {
        public IGenericRepository<TD> GenericRepository<TD>()
            where TD : class
        {
            return new GenericRepository<TD>(client.GetDatabase(settings.Value.Collection));
        }
    }
}
