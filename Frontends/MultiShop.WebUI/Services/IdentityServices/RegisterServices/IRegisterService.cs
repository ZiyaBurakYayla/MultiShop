using MultiShop.DtoLayer.IdentityDtos.RegisterDtos;

namespace MultiShop.WebUI.Services.IdentityServices.RegisterServices
{
    public interface IRegisterService
    {
        Task<string> CreateUserAsync(CreateRegisterDto createRegisterDto);
    }
}
