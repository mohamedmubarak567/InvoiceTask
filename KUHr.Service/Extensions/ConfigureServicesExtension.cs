
using KUHr.Data;
using KUHr.DataAccess;
using KUHr.DataAccess.Repository;
using KUHr.Service.AutoMapper;
using KUHr.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System.Reflection;


namespace KUHr.Service.Extensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ServicesRegisterConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.DatabaseConfig(configuration);
            services.AddAutoMapper(typeof(KUHrProfile));
            services.ServicesConfig();
            return services;
        }
        private static void DatabaseConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("InvoiceConnection");

            services.AddDbContext<KUHrContext>
                (options => options.UseSqlServer(connectionString, x => x.UseNetTopologySuite()).EnableSensitiveDataLogging());
        
            services.AddScoped<DbContext, KUHrContext>();
        }

        private static void ServicesConfig(this IServiceCollection services)
        {
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
            var assemblyToScan = Assembly.GetAssembly(typeof(InvoiceService));
            services.RegisterAssemblyPublicNonGenericClasses(assemblyToScan)
                .Where(c => c.Name.EndsWith("Service"))
              .AsPublicImplementedInterfaces();
           
        }
    }
}
