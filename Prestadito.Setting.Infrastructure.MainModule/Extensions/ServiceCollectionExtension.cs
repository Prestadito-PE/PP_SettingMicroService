using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Prestadito.Setting.Infrastructure.Data.Context;
using Prestadito.Setting.Infrastructure.Data.Interface;
using Prestadito.Setting.Infrastructure.Data.Settings;

namespace Prestadito.Setting.Infrastructure.MainModule.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddMongoDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SecurityDBSettings>(configuration.GetSection(nameof(SecurityDBSettings)));
            services.AddSingleton<ISecurityDBSettings>(sp => sp.GetRequiredService<IOptions<SecurityDBSettings>>().Value);
            services.AddSingleton<MongoContext>();

            return services;
        }
    }
}
