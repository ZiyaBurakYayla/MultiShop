using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.Concrete;
using MultiShop.WebUI.Services.IdentityServices;
using MultiShop.WebUI.Services.IdentityServices.LoginServices;
using MultiShop.WebUI.Services.IdentityServices.RegisterServices;
using MultiShop.WebUI.Services.IdentityServices.RoleServices;
using MultiShop.WebUI.Services.IdentityServices.UserServices;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Services.UserIdentityServices;
using MultiShop.WebUI.Settings;

namespace MultiShop.WebUI.Extensions
{
    public static class AuthServiceRegistration
    {
        public static void AddAuthServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddCookie(JwtBearerDefaults.AuthenticationScheme,
                options =>
            {
                options.LoginPath = "/Login/Index/";
                options.AccessDeniedPath = "/Pages/AccessDenied/";
                options.LogoutPath = "/Login/LogOut/";
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.Cookie.Name = "MultiShopJwt";
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie
                (CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = "/Login/Index/";
                options.Cookie.Name = "MultiShopCookie";
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
                options.SlidingExpiration = true;
            });

            services.AddAccessTokenManagement();

            services.AddHttpContextAccessor();

            services.AddScoped<ILoginService, LoginService>();
            services.AddHttpClient<IIdentityService, IdentityService>();

            services.AddHttpClient();

            services.Configure<ClientSettings>(configuration.GetSection("ClientSettings"));
            services.Configure<ServiceApiSettings>(configuration.GetSection("ServiceApiSettings"));

            services.AddScoped<ResourceOwnerPasswordTokenHandler>();
            services.AddScoped<ClientCredentialTokenHandler>();
            services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();

            var values = configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

            services.AddHttpClient<IUserService, UserService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Identity.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IUserIdentityService, UserIdentityService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Identity.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IRoleService, RoleService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Identity.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IRegisterService, RegisterService>(opt =>
            {
                opt.BaseAddress = new Uri($"{values.OcelotUrl}/{values.Identity.Path}");
            });
        }
    }
}
