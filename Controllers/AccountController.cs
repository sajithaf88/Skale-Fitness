using Microsoft.AspNetCore.Mvc;
using SkaleFitnessMVC.Data;
using SkaleFitnessMVC.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace SkaleFitnessMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ================= LOGIN =================

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var username = user.Username?.Trim();
            var password = user.Password?.Trim();

            var loggedUser = _context.Users
                .FirstOrDefault(u => u.Username == username && u.Password == password);

            if (loggedUser != null)
            {
                HttpContext.Session.SetString("Username", loggedUser.Username);
                HttpContext.Session.SetString("Role", loggedUser.Role);

                // Redirect everyone to Home page
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid Username or Password";
            return View();
        }
        // ================= REGISTER =================

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            // Check if username already exists
            var existingUser = _context.Users
                .FirstOrDefault(u => u.Username == user.Username);

            if (existingUser != null)
            {
                ViewBag.Error = "Username already exists. Please choose another.";
                return View();
            }

            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }

            return View(user);
        }
        // ================= LOGOUT =================

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}