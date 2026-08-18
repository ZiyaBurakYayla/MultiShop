using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.CargoServices.CargoCompanyServices;
using MultiShop.WebUI.Services.CargoServices.CargoCustomerServices;
using MultiShop.WebUI.Services.CargoServices.CargoDetailServices;
using MultiShop.WebUI.Services.CargoServices.CargoOperationServices;
using MultiShop.WebUI.Settings;

namespace MultiShop.WebUI.Extensions
{
    public static class CargoServiceRegistration
    {
        public static void AddCargoClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            var values = configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

            services.AddHttpClient<ICargoCompanyService, CargoCompanyService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Cargo.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<ICargoCustomerService, CargoCustomerService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Cargo.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<ICargoDetailService, CargoDetailService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Cargo.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<ICargoOperationService, CargoOperationService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Cargo.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
        }
    }
}
