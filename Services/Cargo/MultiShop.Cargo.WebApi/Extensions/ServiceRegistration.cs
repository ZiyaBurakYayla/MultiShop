using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.BusinessLayer.Concrete;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Concrete;
using MultiShop.Cargo.DataAccessLayer.EntityFramework;
using MultiShop.Cargo.WebApi.LoginServices;
using MultiShop.Cargo.WebApi.Services.StatisticServices;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

namespace MultiShop.Cargo.WebApi.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddCargoServices(this IServiceCollection services, IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.Authority = configuration["IdentityServerUrl"];
                opt.Audience = "ResourceCargo";
                opt.RequireHttpsMetadata = false;
            });

            services.AddDbContext<CargoContext>();

            services.AddScoped<ICargoCompanyDal, EfCargoCompanyDal>();
            services.AddScoped<ICargoCompanyService, CargoCompanyManager>();

            services.AddScoped<ICargoCustomerDal, EfCargoCustomerDal>();
            services.AddScoped<ICargoCustomerService, CargoCustomerManager>();

            services.AddScoped<ICargoDetailDal, EfCargoDetailDal>();
            services.AddScoped<ICargoDetailService, CargoDetailManager>();

            services.AddScoped<ICargoOperationDal, EfCargoOperationDal>();
            services.AddScoped<ICargoOperationService, CargoOperationManager>();

            services.AddScoped<IStatisticService, StatisticService>();

            services.AddHttpContextAccessor();
            services.AddScoped<ILoginService, LoginService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssembly(typeof(ICargoCompanyService).Assembly);
            services.AddFluentValidationAutoValidation();
        }
    }
}
