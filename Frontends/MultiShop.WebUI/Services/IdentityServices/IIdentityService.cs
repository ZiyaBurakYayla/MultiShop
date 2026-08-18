using MultiShop.DtoLayer.IdentityDtos.LoginDtos;

namespace MultiShop.WebUI.Services.IdentityServices
{
    public interface IIdentityService
    {
        Task<string> SignIn(SignInDto signInDto);
        Task<bool> GetRefreshToken();
    }
}
