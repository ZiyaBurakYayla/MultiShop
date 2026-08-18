using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.Payment.Services;

namespace MultiShop.Payment.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddPaymentServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.Authority = configuration["IdentityServerUrl"];
                opt.Audience = "ResourcePayment";
                opt.RequireHttpsMetadata = false;
            });

            services.AddScoped<IPaymentService, PaymentService>();
        }
    }
}
