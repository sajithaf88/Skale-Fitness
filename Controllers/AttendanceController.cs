using Microsoft.AspNetCore.Mvc;
using SkaleFitnessMVC.Data;
using SkaleFitnessMVC.Models;

namespace SkaleFitnessMVC.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttendanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔐 ADMIN + TRAINER CHECK
        private bool IsAdminOrTrainer()
        {
            var role = HttpContext.Session.GetString("Role");
            return HttpContext.Session.GetString("Username") != null
                   && (role == "Admin" || role == "Trainer");
        }

        // INDEX
        public IActionResult Index()
        {
            if (!IsAdminOrTrainer())
                return RedirectToAction("Index", "Home");

            return View(_context.Attendances.ToList());
        }

        // CREATE
        public IActionResult Create()
        {
            if (!IsAdminOrTrainer())
                return RedirectToAction("Index", "Home");

            ViewBag.Members = _context.Members.ToList();
            ViewBag.Classes = _context.FitnessClasses.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Attendance attendance)
        {
            if (!IsAdminOrTrainer())
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                _context.Attendances.Add(attendance);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Members = _context.Members.ToList();
            ViewBag.Classes = _context.FitnessClasses.ToList();
            return View(attendance);
        }

        // EDIT
        public IActionResult Edit(int id)
        {
            if (!IsAdminOrTrainer())
                return RedirectToAction("Index", "Home");

            var attendance = _context.Attendances.Find(id);
            if (attendance == null) return NotFound();

            ViewBag.Members = _context.Members.ToList();
            ViewBag.Classes = _context.FitnessClasses.ToList();
            return View(attendance);
        }

        [HttpPost]
        public IActionResult Edit(Attendance attendance)
        {
            if (!IsAdminOrTrainer())
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                _context.Attendances.Update(attendance);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Members = _context.Members.ToList();
            ViewBag.Classes = _context.FitnessClasses.ToList();
            return View(attendance);
        }

        // DETAILS
        public IActionResult Details(int id)
        {
            if (!IsAdminOrTrainer())
                return RedirectToAction("Index", "Home");

            var attendance = _context.Attendances.Find(id);
            if (attendance == null) return NotFound();

            return View(attendance);
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            if (!IsAdminOrTrainer())
                return RedirectToAction("Index", "Home");

            var attendance = _context.Attendances.Find(id);
            if (attendance == null) return NotFound();

            return View(attendance);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!IsAdminOrTrainer())
                return RedirectToAction("Index", "Home");

            var attendance = _context.Attendances.Find(id);
            if (attendance != null)
            {
                _context.Attendances.Remove(attendance);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
