using Events.Application.Configuration.Cloudinary;
using Events.Application.DTO.Category;
using Events.Application.DTO.Event;
using Events.Application.DTO.Participant;
using Events.Application.Mapper;
using Events.Application.UseCases.CacheUseCase.ImageCache;
using Events.Application.UseCases.CacheUseCase.ImageCache.Interfaces;
using Events.Application.UseCases.CategoryUseCase.Delete;
using Events.Application.UseCases.CategoryUseCase.Delete.Interfaces;
using Events.Application.UseCases.CategoryUseCase.Get;
using Events.Application.UseCases.CategoryUseCase.Get.Interfaces;
using Events.Application.UseCases.CategoryUseCase.Insert;
using Events.Application.UseCases.CategoryUseCase.Insert.Interfaces;
using Events.Application.UseCases.CategoryUseCase.Update;
using Events.Application.UseCases.CategoryUseCase.Update.Interfaces;
using Events.Application.UseCases.CloudinaryUseCase.Upload;
using Events.Application.UseCases.CloudinaryUseCase.Upload.Interfaces;
using Events.Application.UseCases.EventParticipantUseCase.Delete;
using Events.Application.UseCases.EventParticipantUseCase.Delete.Interfaces;
using Events.Application.UseCases.EventParticipantUseCase.Get;
using Events.Application.UseCases.EventParticipantUseCase.Get.Interfaces;
using Events.Application.UseCases.EventParticipantUseCase.Insert;
using Events.Application.UseCases.EventParticipantUseCase.Insert.Interfaces;
using Events.Application.UseCases.EventParticipantUseCase.Update;
using Events.Application.UseCases.EventParticipantUseCase.Update.Interfaces;
using Events.Application.UseCases.EventUseCase.Delete;
using Events.Application.UseCases.EventUseCase.Delete.Interfaces;
using Events.Application.UseCases.EventUseCase.Get;
using Events.Application.UseCases.EventUseCase.Get.Interfaces;
using Events.Application.UseCases.EventUseCase.Insert;
using Events.Application.UseCases.EventUseCase.Insert.Interfaces;
using Events.Application.UseCases.EventUseCase.Update;
using Events.Application.UseCases.EventUseCase.Update.Interfaces;
using Events.Application.UseCases.Hash.HashPassword;
using Events.Application.UseCases.HashUseCase.Hash.Interfaces;
using Events.Application.UseCases.HashUseCase.Verify;
using Events.Application.UseCases.HashUseCase.Verify.Interfaces;
using Events.Application.UseCases.ParticipantUseCase.Delete;
using Events.Application.UseCases.ParticipantUseCase.Delete.Interfaces;
using Events.Application.UseCases.ParticipantUseCase.Get;
using Events.Application.UseCases.ParticipantUseCase.Get.Interfaces;
using Events.Application.UseCases.ParticipantUseCase.Insert;
using Events.Application.UseCases.ParticipantUseCase.Insert.Interfaces;
using Events.Application.UseCases.ParticipantUseCase.Login;
using Events.Application.UseCases.ParticipantUseCase.Login.Interfaces;
using Events.Application.UseCases.ParticipantUseCase.Update;
using Events.Application.UseCases.ParticipantUseCase.Update.Interfaces;
using Events.Application.UseCases.TokenUseCase.Generate;
using Events.Application.UseCases.TokenUseCase.Generate.Interfaces;
using Events.Application.Validation.Category;
using Events.Application.Validation.Event;
using Events.Application.Validation.Participant;
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

            services.AddScoped<ICacheImageUseCase, CacheImageUseCase>();

            services.AddScoped<IValidator<EventDTO>, EventValidator>();
            services.AddScoped<IValidator<CategoryDTO>, CategoryValidator>();
            services.AddScoped<IValidator<ParticipantAuthDTO>, ParticipantAuthValidator>();
            services.AddScoped<IValidator<ParticipantDTO>, ParticipantValidator>();
            services.AddScoped<IValidator<CreateParticipantDTO>, CreateParticipantValidator>();
            services.AddScoped<IValidator<UpdateParticipantDTO>, UpdateParticipantValidator>();
            services.AddScoped<IValidator<UpdateEventDTO>, UpdateEventValidator>();
            services.AddScoped<IValidator<CreateCategoryDTO>, CreateCategoryValidator>();

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
