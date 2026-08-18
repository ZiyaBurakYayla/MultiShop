using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.OrderServices.AddressServices;
using MultiShop.WebUI.Services.OrderServices.OrderingServices;
using MultiShop.WebUI.Services.OrderServices.OrderDetailServices;
using MultiShop.WebUI.Settings;

namespace MultiShop.WebUI.Extensions
{
    public static class OrderServiceRegistration
    {
        public static void AddOrderClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            var values = configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

            services.AddHttpClient<IOrderingService, OrderingService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Order.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IOrderDetailService, OrderDetailService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Order.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IAddressService, AddressService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Order.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
        }
    }
}
