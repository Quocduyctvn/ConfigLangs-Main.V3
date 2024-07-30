using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace lscCommon.configLang.commandApplication.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register application services
        /// </summary>
        /// <param name="services"></param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register mediator
            services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()); });
            return services;
        }
    }
}
