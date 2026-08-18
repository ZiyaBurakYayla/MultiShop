using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.DiscountServices;
using MultiShop.WebUI.Settings;

namespace MultiShop.WebUI.Extensions
{
    public static class DiscountServiceRegistration
    {
        public static void AddDiscountClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            var values = configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

            services.AddHttpClient<IDiscountService, DiscountService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Discount.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
        }
    }
}
