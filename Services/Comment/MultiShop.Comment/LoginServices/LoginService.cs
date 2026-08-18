namespace MultiShop.Comment.LoginServices
{
    public class LoginService : ILoginService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public LoginService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string GetUserId
        {
            get
            {
                var claim = _contextAccessor.HttpContext?.User?.FindFirst("sub");
                if (claim == null)
                {
                    return null;
                }
                return claim.Value;
            }
        }
    }
}
