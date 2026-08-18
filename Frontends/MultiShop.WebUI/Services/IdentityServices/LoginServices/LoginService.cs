using MultiShop.WebUI.Services.Concrete;
using System.Security.Claims;

namespace MultiShop.WebUI.Services.IdentityServices.LoginServices
{
    public class LoginService : ILoginService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public LoginService(IHttpContextAccessor context)
        {
            _contextAccessor = context;
        }

        public string GetUserId
        {
            get
            {
                var user = _contextAccessor.HttpContext.User;
                var claim = user.FindFirst("sub");
                if (claim == null)
                {
                    claim = user.FindFirst(ClaimTypes.NameIdentifier);
                }
                if (claim == null)
                {
                    return null;
                }
                return claim.Value;
            }
        }
    }
}
