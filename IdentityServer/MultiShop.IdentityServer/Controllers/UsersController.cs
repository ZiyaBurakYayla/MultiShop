using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.IdentityServer.Dtos;
using MultiShop.IdentityServer.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace MultiShop.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            var userClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
            if (userClaim == null)
            {
                return null;
            }
            return await _userManager.FindByIdAsync(userClaim.Value);
        }

        private async Task<string> GetImageUrl(ApplicationUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var picture = claims.FirstOrDefault(x => x.Type == "picture");
            if (picture == null)
            {
                return "";
            }
            return picture.Value;
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            var user = await GetCurrentUser();
            if (user == null)
            {
                return NotFound();
            }
            return Ok(new
            {
                Id = user.Id,
                Username = user.UserName,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                ImageUrl = await GetImageUrl(user)
            });
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto updateUserDto)
        {
            var user = await GetCurrentUser();
            if (user == null)
            {
                return NotFound();
            }

            user.Name = updateUserDto.Name;
            user.Surname = updateUserDto.Surname;

            if (!string.IsNullOrWhiteSpace(updateUserDto.Email) && updateUserDto.Email != user.Email)
            {
                user.Email = updateUserDto.Email;
                user.NormalizedEmail = updateUserDto.Email.ToUpperInvariant();
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(string.Join(" ", result.Errors.Select(x => x.Description)));
            }

            var claims = await _userManager.GetClaimsAsync(user);
            var picture = claims.FirstOrDefault(x => x.Type == "picture");
            var newPicture = new Claim("picture", updateUserDto.ImageUrl ?? "");
            if (picture == null)
            {
                await _userManager.AddClaimAsync(user, newPicture);
            }
            else
            {
                await _userManager.ReplaceClaimAsync(user, picture, newPicture);
            }

            if (!string.IsNullOrWhiteSpace(updateUserDto.NewPassword))
            {
                if (string.IsNullOrWhiteSpace(updateUserDto.CurrentPassword))
                {
                    return BadRequest("Şifre değiştirmek için mevcut şifrenizi girmelisiniz.");
                }
                var passwordResult = await _userManager.ChangePasswordAsync(user, updateUserDto.CurrentPassword, updateUserDto.NewPassword);
                if (!passwordResult.Succeeded)
                {
                    return BadRequest(string.Join(" ", passwordResult.Errors.Select(x => x.Description)));
                }
            }

            return Ok();
        }

        [HttpGet("GetAllUserList")]
        public async Task<IActionResult> GetAllUserList()
        {
            var values = await _userManager.Users.ToListAsync();
            return Ok(values);
        }
    }
}
