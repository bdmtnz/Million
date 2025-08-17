using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Million.BackEnd.Domain.Common.Contracts.Persistence;
using Million.BackEnd.Infrastructure.Common.Persistence;
using MongoDB.Driver;

namespace Million.BackEnd.Infrastructure
{
    public static class Dependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddPersistence(configuration);

            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var persistenceSettingsSection = configuration.GetSection(nameof(PersistenceSettings));
            var persistenceSettings = persistenceSettingsSection.Get<PersistenceSettings>();

            ArgumentNullException.ThrowIfNull(persistenceSettings, nameof(persistenceSettings));

            services.Configure<PersistenceSettings>(opt =>
            {
                opt.Db = persistenceSettings.Db;
                opt.Collection = persistenceSettings.Collection;
            });

            services.AddScoped<IMongoClient>((prop) => {
                return new MongoClient(persistenceSettings.Db);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

    }

}
