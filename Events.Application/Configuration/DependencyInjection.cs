using Events.Application.Configuration.Cloudinary;
using Events.Application.DTO.Category;
using Events.Application.DTO.Event;
using Events.Application.DTO.EventParticipant;
using Events.Application.DTO.Page;
using Events.Application.DTO.Participant;
using Events.Application.DTO.Token;
using Events.Application.Interfaces.UseCase.Category;
using Events.Application.Interfaces.UseCase.Cloudinary;
using Events.Application.Interfaces.UseCase.Event;
using Events.Application.Interfaces.UseCase.EventParticipant;
using Events.Application.Interfaces.UseCase.Hash;
using Events.Application.Interfaces.UseCase.Participant;
using Events.Application.Interfaces.UseCase.Token;
using Events.Application.Mapper;
using Events.Application.UseCases.CategoryUseCase.Delete;
using Events.Application.UseCases.CategoryUseCase.Get;
using Events.Application.UseCases.CategoryUseCase.Insert;
using Events.Application.UseCases.CategoryUseCase.Update;
using Events.Application.UseCases.CloudinaryUseCase.Upload;
using Events.Application.UseCases.EventParticipantUseCase.Delete;
using Events.Application.UseCases.EventParticipantUseCase.Get;
using Events.Application.UseCases.EventParticipantUseCase.Insert;
using Events.Application.UseCases.EventParticipantUseCase.Update;
using Events.Application.UseCases.EventUseCase.Delete;
using Events.Application.UseCases.EventUseCase.Get;
using Events.Application.UseCases.EventUseCase.Insert;
using Events.Application.UseCases.EventUseCase.Update;
using Events.Application.UseCases.Hash.HashPassword;
using Events.Application.UseCases.HashUseCase.Verify;
using Events.Application.UseCases.ParticipantUseCase.Delete;
using Events.Application.UseCases.ParticipantUseCase.Get;
using Events.Application.UseCases.ParticipantUseCase.Insert;
using Events.Application.UseCases.ParticipantUseCase.Login;
using Events.Application.UseCases.ParticipantUseCase.Update;
using Events.Application.UseCases.TokenUseCase.Generate;
using Events.Application.UseCases.TokenUseCase.Validation;
using Events.Application.Validation.Category;
using Events.Application.Validation.Event;
using Events.Application.Validation.EventParticipant;
using Events.Application.Validation.Page;
using Events.Application.Validation.Participant;
using Events.Application.Validation.Token;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace Events.Application.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, CloudinarySettings cloudinarySettings)
        {
            services.AddSingleton(cloudinarySettings);

            services.AddAutoMapper(typeof(EventProfile), 
                                   typeof(CategoryProfile),
                                   typeof(EventParticipantProfile),
                                   typeof(ParticipantProfile));

            services.AddScoped<IDeleteCategoryUseCase, DeleteCategoryUseCase>();
            services.AddScoped<IDeleteEventUseCase, DeleteEventUseCase>();
            services.AddScoped<IDeleteEventParticipantUseCase, DeleteEventParticipantUseCase>();
            services.AddScoped<IDeleteParticipantUseCase, DeleteParticipantUseCase>();
            services.AddScoped<IDeleteRefreshTokenParticipantUseCase, DeleteRefreshTokenParticipantUseCase>();

            services.AddScoped<IInsertCategoryUseCase, InsertCategoryUseCase>();
            services.AddScoped<IInsertEventUseCase, InsertEventUseCase>();
            services.AddScoped<IInsertEventParticipantUseCase, InsertEventParticipantUseCase>();
            services.AddScoped<IInsertParticipantUseCase, InsertParticipantUseCase>();  
            
            services.AddScoped<IUpdateCategoryUseCase, UpdateCategoryUseCase>();
            services.AddScoped<IUpdateEventUseCase, UpdateEventUseCase>();
            services.AddScoped<IUpdateEventParticipantUseCase, UpdateEventParticipantUseCase>();
            services.AddScoped<IUpdateParticipantUseCase, UpdateParticipantUseCase>();

            services.AddScoped<IUpdateImageEventUseCase, UpdateImageEventUseCase>();

            services.AddScoped<IGetAllCategoryUseCase, GetAllCategoryUseCase>();
            services.AddScoped<IGetAllEventUseCase, GetAllEventUseCase>();
            services.AddScoped<IGetAllEventParticipantUseCase, GetAllEventParticipantUseCase>();
            services.AddScoped<IGetAllParticipantUseCase, GetAllParticipantUseCase>();

            services.AddScoped<IGetByIdParticipantUseCase, GetByIdParticipantUseCase>();
            services.AddScoped<IGetByEmailParticipantUseCase, GetByEmailParticipantUseCase>();
            services.AddScoped<IGetByIdEventParticipantUseCase, GetByIdEventParticipantUseCase>();
            services.AddScoped<IGetByIdEventUseCase, GetByIdEventUseCase>();
            services.AddScoped<IGetByIdCategoryUseCase, GetByIdCategoryUseCase>();

            services.AddScoped<IGetByCategoryEventUseCase, GetByCategoryEventUseCase>();
            services.AddScoped<IGetByLocationEventUseCase, GetByLocationEventUseCase>();
            services.AddScoped<IGetByNameEventUseCase, GetByNameEventUseCase>();
            services.AddScoped<IGetByDateEventUseCase, GetByDateEventUseCase>();

            services.AddScoped<IGetByRefreshTokenParticipantUseCase, GetByRefreshTokenParticipantUseCase>();

            services.AddScoped<ILoginParticipantUseCase, LoginParticipantUseCase>();

            services.AddScoped<IUploadImageCloudinaryUseCase, UploadImageCloudinaryUseCase>();

            services.AddScoped<IHashPasswordUseCase, HashPasswordUseCase>();
            services.AddScoped<IVerifyPasswordUseCase, VerifyPasswordUseCase>();

            services.AddScoped<ITokenGenerateUseCase, TokenGenerateUseCase>();
            services.AddScoped<ITokenInvalidUseCase, TokenInvalidUseCase>();

            services.AddScoped<IValidator<EventDTO>, EventValidator>();
            services.AddScoped<IValidator<CategoryDTO>, CategoryValidator>();
            services.AddScoped<IValidator<ParticipantAuthDTO>, ParticipantAuthValidator>();
            services.AddScoped<IValidator<ParticipantDTO>, ParticipantValidator>();
            services.AddScoped<IValidator<CreateParticipantDTO>, CreateParticipantValidator>();
            services.AddScoped<IValidator<UpdateParticipantDTO>, UpdateParticipantValidator>();
            services.AddScoped<IValidator<UpdateEventDTO>, UpdateEventValidator>();
            services.AddScoped<IValidator<CreateCategoryDTO>, CreateCategoryValidator>();
            services.AddScoped<IValidator<PageDTO>, PageValidator>();
            services.AddScoped<IValidator<TokenDTO>, TokenValidator>();
            services.AddScoped<IValidator<EventParticipantDTO>, EventParticipantValidator>();

            services.AddMemoryCache();

            services.AddControllers()
                .AddJsonOptions(options => 
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            return services;
        }
    }
}
