using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.RapidApiServices.CatalogProductServices;
using MultiShop.WebUI.Settings;

namespace MultiShop.WebUI.Extensions
{
    public static class RapidApiServiceRegistration
    {
        public static void AddRapidApiClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            var values = configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

            services.AddHttpClient<IProductSearchService, ProductSearchService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.RapidApi.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();
        }
    }
}
