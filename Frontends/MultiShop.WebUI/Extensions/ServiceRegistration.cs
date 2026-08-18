using MultiShop.WebUI.RabbitMQ;

namespace MultiShop.WebUI.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddWebUIServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthServices(configuration);
            services.AddCatalogClientServices(configuration);
            services.AddOrderClientServices(configuration);
            services.AddCargoClientServices(configuration);
            services.AddDiscountClientServices(configuration);
            services.AddCommentClientServices(configuration);
            services.AddMessageClientServices(configuration);
            services.AddBasketClientServices(configuration);
            services.AddPaymentClientServices(configuration);
            services.AddRapidApiClientServices(configuration);
            services.AddStatisticClientServices(configuration);

            services.AddScoped<RabbitMQPublisher>();
        }
    }
}
