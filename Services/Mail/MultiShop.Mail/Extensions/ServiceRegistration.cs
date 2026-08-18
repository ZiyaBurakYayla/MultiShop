using MultiShop.Mail.RabbitMQ;
using MultiShop.Mail.Services;
using MultiShop.Mail.Settings;

namespace MultiShop.Mail.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddMailServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddScoped<IMailService, MailService>();
            services.AddHostedService<OrderCreatedMailConsumer>();
        }
    }
}
