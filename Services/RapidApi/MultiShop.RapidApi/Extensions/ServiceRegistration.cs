using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.RapidApi.Services.CatalogServices.CatalogProductServices;

namespace MultiShop.RapidApi.Extensions
{
    public static class services
    {
        public static void AddRapidApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.Authority = configuration["IdentityServerUrl"];
                opt.Audience = "ResourceRapidApi";
                opt.RequireHttpsMetadata = false;
            });

            services.AddHttpClient<IProductService, ProductService>();
            services.AddControllers();
        }
    }
}
