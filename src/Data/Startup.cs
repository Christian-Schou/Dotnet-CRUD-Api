using Data.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    public static class Startup
    {
        /// <summary>
        /// Register Database Context for service container (DI)
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            // Add Database Context for the application
            services.AddDbContext<ApplicationDbContext>();
            return services;
        }
    }
}
