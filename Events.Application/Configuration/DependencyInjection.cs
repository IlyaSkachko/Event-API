using Events.Application.DTO.Category;
using Events.Application.DTO.Event;
using Events.Application.DTO.Participant;
using Events.Application.Mapper;
using Events.Application.Services;
using Events.Application.Services.Interfaces;
using Events.Application.Validation.Category;
using Events.Application.Validation.Event;
using Events.Application.Validation.Participant;
using FluentValidation;
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

            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IParticipantService, ParticipantService>();
            services.AddScoped<IEventParticipantService, EventParticipantService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IHashService, HashService>();

            services.AddScoped<IValidator<EventDTO>, EventValidator>();
            services.AddScoped<IValidator<EventImageDTO>, EventImageValidator>();
            services.AddScoped<IValidator<CategoryDTO>, CategoryValidator>();
            services.AddScoped<IValidator<ParticipantAuthDTO>, ParticipantAuthValidator>();
            services.AddScoped<IValidator<ParticipantDTO>, ParticipantValidator>();
            services.AddScoped<IValidator<CreateParticipantDTO>, CreateParticipantValidator>();
            services.AddScoped<IValidator<UpdateParticipantDTO>, UpdateParticipantValidator>();

            return services;
        }
    }
}
