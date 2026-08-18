namespace MultiShop.WebUI.Services.IdentityServices.RoleServices
{
    public interface IRoleService
    {
        Task AssignRoleAsync(string userId, string role);
    }
}
