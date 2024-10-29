using Events.Application.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace Events.Application.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(EventProfile), 
                                   typeof(CategoryProfile),
                                   typeof(EventParticipantProfile),
                                   typeof(ParticipantProfile));

            return services;
        }
    }
}
