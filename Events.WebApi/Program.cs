using Events.Application.Configuration;
using Events.Domain.Cloudinary;
using Events.Infrastructure.Configuration;
using Events.WebApi.Middleware;

namespace Events.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var cloudinaryUrl = Environment.GetEnvironmentVariable("CLOUDINARY_URL");

            var cloudinarySettings = new CloudinarySettings { Url = cloudinaryUrl };

            builder.Services
                .AddInfrastructure(builder.Configuration)
                .AddApplication(cloudinarySettings);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ErrorMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
