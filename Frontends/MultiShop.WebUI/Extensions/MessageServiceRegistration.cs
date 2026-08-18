using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.MessageServices;
using MultiShop.WebUI.Settings;

namespace MultiShop.WebUI.Extensions
{
    public static class MessageServiceRegistration
    {
        public static void AddMessageClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            var values = configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

            services.AddHttpClient<IMessageService, MessageService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Message.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
        }
    }
}
