using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.Images.Services.StorageServices;
using MultiShop.Images.Settings;

namespace MultiShop.Images.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddImagesServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.Authority = configuration["IdentityServerUrl"];
                opt.Audience = "ResourceImage";
                opt.RequireHttpsMetadata = false;
            });

            services.Configure<GoogleCloudStorageSettings>(
                configuration.GetSection("GoogleCloudStorage"));

            services.AddSingleton<IStorageService, GoogleCloudStorageService>();
        }
    }
}
