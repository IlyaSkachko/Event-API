using Microsoft.Extensions.DependencyInjection;


namespace Events.Domain.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            return services;
        }
    }  
}
