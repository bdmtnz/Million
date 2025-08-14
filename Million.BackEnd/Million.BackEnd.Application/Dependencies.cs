using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Million.BackEnd.Application
{
    public static class Dependencies
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(typeof(Dependencies).Assembly);
            });

            return services;
        }
    }
}
