using Microsoft.AspNetCore.Mvc;
using SkaleFitnessMVC.Data;
using SkaleFitnessMVC.Models;

namespace SkaleFitnessMVC.Controllers
{
    public class MembersController : Controller
    {
        private readonly ApplicationDbContext _context;


    public MembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔐 ADMIN OR USER CHECK
        private bool IsAdminOrUser()
        {
            if (HttpContext.Session.GetString("Username") == null)
                return false;

            var role = HttpContext.Session.GetString("Role");
            return role == "Admin" || role == "User";
        }

        // LIST
        public IActionResult Index()
        {
            if (!IsAdminOrUser())
                return RedirectToAction("Login", "Account");

            return View(_context.Members.ToList());
        }

        // DETAILS
        public IActionResult Details(int id)
        {
            if (!IsAdminOrUser())
                return RedirectToAction("Login", "Account");

            var member = _context.Members.Find(id);
            if (member == null) return NotFound();

            return View(member);
        }

        // CREATE
        public IActionResult Create()
        {
            if (!IsAdminOrUser())
                return RedirectToAction("Login", "Account");

            return View();
        }

        [HttpPost]
        public IActionResult Create(Member member)
        {
            if (!IsAdminOrUser())
                return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                _context.Members.Add(member);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(member);
        }

        // EDIT
        public IActionResult Edit(int id)
        {
            if (!IsAdminOrUser())
                return RedirectToAction("Login", "Account");

            var member = _context.Members.Find(id);
            if (member == null) return NotFound();

            return View(member);
        }

        [HttpPost]
        public IActionResult Edit(Member member)
        {
            if (!IsAdminOrUser())
                return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                _context.Members.Update(member);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(member);
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            if (!IsAdminOrUser())
                return RedirectToAction("Login", "Account");

            var member = _context.Members.Find(id);
            if (member == null) return NotFound();

            return View(member);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!IsAdminOrUser())
                return RedirectToAction("Login", "Account");

            var member = _context.Members.Find(id);

            if (member != null)
            {
                _context.Members.Remove(member);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }


}
