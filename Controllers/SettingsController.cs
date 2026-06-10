using Microsoft.AspNetCore.Mvc;

namespace SkaleFitnessMVC.Controllers
{
    public class SettingsController : Controller
    {
        // 🔐 ADMIN CHECK (same pattern everywhere)
        private bool IsAdmin()
        {
            return HttpContext.Session.GetString("Username") != null
                && HttpContext.Session.GetString("Role") == "Admin";
        }

        public IActionResult Index()
        {
            if (!IsAdmin())
                return RedirectToAction("Index", "Home");

            return View();
        }
    }
}
