using Events.Domain.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Events.WebApi.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebAPI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                    {
                        options.TokenValidationParameters = new()
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("thHxx1uPmtZYc7LYY1fIbx4t2SPTNf7AeONVQJPNQb0B"))
                        };

                        options.Events = new JwtBearerEvents
                        {
                            OnMessageReceived = context =>
                            {
                                context.Token = context.Request.Cookies["access-token"];

                                return Task.CompletedTask;
                            }
                        };
                    });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy =>
                {
                    policy.RequireClaim("Role", Role.ADMIN.ToString());
                });

                options.AddPolicy("UserPolicy", policy =>
                {
                    policy.RequireClaim("Role", Role.USER.ToString());
                });

                options.AddPolicy("AdminOrUserPolicy", policy => 
                { 
                    policy.RequireAssertion(context => context.User.HasClaim(claim => claim.Type == "Role" && (claim.Value == Role.ADMIN.ToString() ||
                    claim.Value == Role.USER.ToString()))); 
                });

            });

            return services;
        }
    }
}
