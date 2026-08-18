using MultiShop.DtoLayer.IdentityDtos.UserDtos;
using MultiShop.WebUI.Models;

namespace MultiShop.WebUI.Services.IdentityServices.UserServices
{
    public interface IUserService
    {
        Task<UserDetailViewModel> GetUserInfo();
        Task<List<ResultUserDto>> GetAllUserListAsync();
        Task<string> UpdateUserAsync(UpdateUserDto updateUserDto);
    }
}
