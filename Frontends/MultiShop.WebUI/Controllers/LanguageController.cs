using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Controllers
{
    public class LanguageController : Controller
    {
        // Secilen dili 1 yillik cookie olarak yazar ve gelinen sayfaya geri doner
        public IActionResult SetCulture(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = "/";

            return LocalRedirect(returnUrl);
        }
    }
}
