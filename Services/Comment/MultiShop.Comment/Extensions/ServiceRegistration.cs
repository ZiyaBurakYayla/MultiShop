using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.Comment.Context;
using MultiShop.Comment.LoginServices;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

namespace MultiShop.Comment.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddCommentServices(this IServiceCollection services, IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.Authority = configuration["IdentityServerUrl"];
                opt.Audience = "ResourceComment";
                opt.RequireHttpsMetadata = false;
            });
            services.AddDbContext<CommentContext>();

            services.AddHttpContextAccessor();
            services.AddScoped<ILoginService, LoginService>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();
        }
    }
}
