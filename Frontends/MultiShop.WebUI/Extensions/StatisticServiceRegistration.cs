using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.StatisticServices.CatalogStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.CommentStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.DiscountStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.MessageStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.UserStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.OrderStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.CargoStatisticServices;
using MultiShop.WebUI.Settings;

namespace MultiShop.WebUI.Extensions
{
    public static class StatisticServiceRegistration
    {
        public static void AddStatisticClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            var values = configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

            services.AddHttpClient<ICatalogStatisticService, CatalogStatisticService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IUserStatisticService, UserStatisticService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Identity.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<ICommentStatisticService, CommentStatisticService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Comment.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IDiscountStatisticService, DiscountStatisticService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Discount.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IMessageStatisticService, MessageStatisticService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Message.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IOrderStatisticService, OrderStatisticService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Order.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<ICargoStatisticService, CargoStatisticService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Cargo.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
        }
    }
}
