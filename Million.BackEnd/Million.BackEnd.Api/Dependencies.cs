using Microsoft.OpenApi.Models;
using Million.BackEnd.Application;
using Million.BackEnd.Infrastructure;

namespace Million.BackEnd.Api
{
    public static class Dependencies
    {
        public static IServiceCollection AddApiDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddPolicyCors()
                .AddSwagger()
                //.AddMappings()
                .AddMiscellaneous()
                //.AddCommonDependencies()
                .AddInfrastructureDependencies(configuration)
                .AddApplicationDependencies(configuration);

            return services;
        }

        public static IServiceCollection AddPolicyCors(this IServiceCollection services)
        {
            string? myAllowSpecificOrigins = "_myAllowSpecificOrigins";
            services.AddCors(options =>
            {
                options.AddPolicy(
                    myAllowSpecificOrigins,
                    policy =>
                    {
                        policy.AllowAnyHeader().AllowAnyOrigin();

                        policy.WithOrigins(
                                "http://localhost:4200",
                                "http://localhost:4200/",
                                "https://gym-manager-v1.azurewebsites.net",
                                "https://gym-manager-v1.azurewebsites.net/",
                                "https://gymmanager.com.co",
                                "https://gymmanager.com.co/",
                                "http://127.0.0.1",
                                "http://127.0.0.1:80")
                            .AllowAnyHeader()
                            .AllowCredentials()
                            .AllowAnyMethod();
                    });
            });

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var environmentName = string.IsNullOrEmpty(environment) ? "Prod" : environment;
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = $"Million API ({environmentName} Mode)",
                    Version = "v1",
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                });
                c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                        },
                        new string[] { }
                    },
                    });
            });
            return services;
        }

        public static IServiceCollection AddMiscellaneous(this IServiceCollection services)
        {
            return services;
        }
    }
}
