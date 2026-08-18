using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Settings;

namespace MultiShop.WebUI.Extensions
{
    public static class BasketServiceRegistration
    {
        public static void AddBasketClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            var values = configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

            services.AddHttpClient<IBasketService, BasketService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Basket.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
        }
    }
}
