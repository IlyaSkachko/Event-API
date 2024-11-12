using Events.Application.Configuration;
using Events.Application.Configuration.Cloudinary;
using Events.Infrastructure.Configuration;
using Events.WebApi.Configuration;
using Events.WebApi.Middleware;

namespace Events.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var cloudinaryUrl = Environment.GetEnvironmentVariable("CLOUDINARY_URL");

            var cloudinarySettings = new CloudinarySettings { Url = cloudinaryUrl };

            builder.Services
                .AddInfrastructure(builder.Configuration)
                .AddApplication(cloudinarySettings)
                .AddWebAPI(builder.Configuration);

            builder.Services.AddMemoryCache();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ErrorMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
