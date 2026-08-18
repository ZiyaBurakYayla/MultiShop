using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Models;
using System.Diagnostics;

namespace MultiShop.WebUI.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            if (statusCode == 404)
            {
                return View("NotFound");
            }

            var model = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            return View("Error", model);
        }
    }
}
