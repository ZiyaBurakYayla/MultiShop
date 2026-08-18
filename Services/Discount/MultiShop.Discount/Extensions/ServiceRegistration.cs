using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.Discount.Context;
using MultiShop.Discount.Services;
using MultiShop.Discount.Services.StatisticServices;
using System.Reflection;

namespace MultiShop.Discount.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddDiscountServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.Authority = configuration["IdentityServerUrl"];
                opt.Audience = "ResourceDiscount";
                opt.RequireHttpsMetadata = false;
            });

            services.AddTransient<DapperContext>();
            services.AddTransient<IDiscountService, DiscountService>();
            services.AddTransient<IStatisticService, StatisticService>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();
        }
    }
}
