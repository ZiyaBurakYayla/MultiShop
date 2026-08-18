using MultiShop.SignalRRealTimeApi.Handlers;
using MultiShop.SignalRRealTimeApi.Services.Cargos.SignalRCargoServices;
using MultiShop.SignalRRealTimeApi.Services.Catalogs.SignalRBrandServices;
using MultiShop.SignalRRealTimeApi.Services.Catalogs.SignalRCategoryServices;
using MultiShop.SignalRRealTimeApi.Services.Catalogs.SignalRFeatureServices;
using MultiShop.SignalRRealTimeApi.Services.Catalogs.SignalRProductImageServices;
using MultiShop.SignalRRealTimeApi.Services.Catalogs.SignalRProductServices;
using MultiShop.SignalRRealTimeApi.Services.Catalogs.SignalRSpecialOfferServices;
using MultiShop.SignalRRealTimeApi.Services.Comments.SignalRCommentServices;
using MultiShop.SignalRRealTimeApi.Services.Discounts.SignalRDiscountServices;
using MultiShop.SignalRRealTimeApi.Services.Messages.SignalRMessageServices;
using MultiShop.SignalRRealTimeApi.Services.Orders.SignalROrderServices;
using MultiShop.SignalRRealTimeApi.Services.TokenServices;
using MultiShop.SignalRRealTimeApi.Services.Users.SignalRUserServices;
using MultiShop.SignalRRealTimeApi.Settings;

namespace MultiShop.SignalRRealTimeApi.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddSignalRRealTimeApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            var allowedOrigins = configuration
                .GetSection("Cors:AllowedOrigins")
                .Get<string[]>() ?? Array.Empty<string>();

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.WithOrigins(allowedOrigins)
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            services.AddSignalR();

            services.Configure<ServiceApiSettings>(configuration.GetSection("ServiceApiSettings"));
            services.Configure<ClientSettings>(configuration.GetSection("ClientSettings"));

            services.AddAccessTokenManagement();
            services.AddScoped<ClientCredentialTokenHandler>();
            services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();

            var values = configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

            services.AddHttpClient<ISignalRCommentService, SignalRCommentService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Comment.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<ISignalRMessageService, SignalRMessageService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Message.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<ISignalRBrandService, SignalRBrandService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<ISignalRProductService, SignalRProductService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<ISignalRCategoryService, SignalRCategoryService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<ISignalRSpecialOfferService, SignalRSpecialOfferService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<ISignalRProductImageService, SignalRProductImageService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<ISignalRFeatureService, SignalRFeatureService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Catalog.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<ISignalROrderService, SignalROrderService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Order.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<ISignalRCargoService, SignalRCargoService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Cargo.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<ISignalRDiscountService, SignalRDiscountService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Discount.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<ISignalRUserService, SignalRUserService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Identity.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();
        }
    }
}
