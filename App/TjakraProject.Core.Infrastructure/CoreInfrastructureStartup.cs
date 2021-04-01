using Microsoft.Extensions.DependencyInjection;
using TjakraProject.Core.Domain.Event;
using TjakraProject.Core.Infrastructure.Service;

namespace TjakraProject.Core.Infrastructure
{
    public static class CoreInfrastructureStartup
    {
        public static IServiceCollection AddCoreInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IDomainEventService, DomainEventService>();
            
            return services;
        }
    }
}