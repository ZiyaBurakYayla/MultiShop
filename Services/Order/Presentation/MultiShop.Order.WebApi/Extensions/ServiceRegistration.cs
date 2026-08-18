using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.Order.Application.Features.CQRS.Handlers.AdressHandlers;
using MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Application.RabbitMQ;
using MultiShop.Order.Persistence.Context;
using MultiShop.Order.Persistence.Repository;
using MultiShop.Order.WebApi.Services.StatisticServices;

namespace MultiShop.Order.WebApi.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddOrderWebApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.Authority = configuration["IdentityServerUrl"];
                opt.Audience = "ResourceOrder";
                opt.RequireHttpsMetadata = false;
            });

            services.AddDbContext<OrderContext>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IOrderingRepository), typeof(OrderingRepository));
            services.AddScoped(typeof(IOrderDetailRepository), typeof(OrderDetailRepository));
            services.AddScoped<IStatisticService, StatisticService>();

            services.AddScoped<GetAddressQueryHandler>();
            services.AddScoped<GetAddressByIdQueryHandler>();
            services.AddScoped<CreateAddressCommandHandler>();
            services.AddScoped<UpdateAddressCommandHandler>();
            services.AddScoped<RemoveAddressCommandHandler>();

            services.AddScoped<GetOrderDetailQueryHandler>();
            services.AddScoped<GetOrderDetailByIdQueryHandler>();
            services.AddScoped<CreateOrderDetailCommandHandler>();
            services.AddScoped<UpdateOrderDetailCommandHandler>();
            services.AddScoped<RemoveOrderDetailCommandHandler>();

            services.AddScoped<RabbitMQPublisher>();
        }
    }
}
