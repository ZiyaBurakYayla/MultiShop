using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using MultiShop.Message.Dal.Context;
using MultiShop.Message.Services;
using MultiShop.Message.Services.StatisticServices;
using System.Reflection;

namespace MultiShop.Message.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddMessageServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.Authority = configuration["IdentityServerUrl"];
                opt.Audience = "ResourceMessage";
                opt.RequireHttpsMetadata = false;
            });

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MessageContext>(opt =>
            {
                opt.UseNpgsql(connectionString);
            });

            services.AddScoped<IUserMessageService, UserMessageService>();
            services.AddScoped<IStatisticService, StatisticService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();
        }
    }
}
