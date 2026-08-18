using IdentityServer4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiShop.IdentityServer.Models;
using System.Threading.Tasks;

namespace MultiShop.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            var userRole = await _userManager.AddToRoleAsync(user, role);
            return Ok(userRole);             
        }
    }
}
