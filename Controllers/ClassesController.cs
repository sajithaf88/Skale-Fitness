using Microsoft.AspNetCore.Mvc;
using SkaleFitnessMVC.Data;
using SkaleFitnessMVC.Models;

namespace SkaleFitnessMVC.Controllers
{
    public class ClassesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClassesController(ApplicationDbContext context)
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

        // ===================== INDEX =====================
        public IActionResult Index()
        {
            if (!IsAdminOrTrainer())
                return RedirectToAction("Index", "Home");

            return View(_context.FitnessClasses.ToList());
        }

        // ===================== CREATE =====================
        public IActionResult Create()
        {
            if (!IsAdminOrTrainer())
                return RedirectToAction("Index", "Home");

            ViewBag.Trainers = _context.Trainers.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(FitnessClass fitnessClass)
        {
            if (!IsAdminOrTrainer())
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                _context.FitnessClasses.Add(fitnessClass);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Trainers = _context.Trainers.ToList();
            return View(fitnessClass);
        }

        // ===================== EDIT =====================
        public IActionResult Edit(int id)
        {
            if (!IsAdminOrTrainer())
                return RedirectToAction("Index", "Home");

            var fitnessClass = _context.FitnessClasses.Find(id);
            if (fitnessClass == null)
                return NotFound();

            ViewBag.Trainers = _context.Trainers.ToList();
            return View(fitnessClass);
        }

        [HttpPost]
        public IActionResult Edit(FitnessClass fitnessClass)
        {
            if (!IsAdminOrTrainer())
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                _context.FitnessClasses.Update(fitnessClass);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Trainers = _context.Trainers.ToList();
            return View(fitnessClass);
        }

        // ===================== DETAILS =====================
        public IActionResult Details(int id)
        {
            if (!IsAdminOrTrainer())
                return RedirectToAction("Index", "Home");

            var fitnessClass = _context.FitnessClasses.Find(id);
            if (fitnessClass == null)
                return NotFound();

            return View(fitnessClass);
        }

        // ===================== DELETE =====================
        public IActionResult Delete(int id)
        {
            if (!IsAdminOrTrainer())
                return RedirectToAction("Index", "Home");

            var fitnessClass = _context.FitnessClasses.Find(id);
            if (fitnessClass == null)
                return NotFound();

            return View(fitnessClass);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!IsAdminOrTrainer())
                return RedirectToAction("Index", "Home");

            var fitnessClass = _context.FitnessClasses.Find(id);
            if (fitnessClass != null)
            {
                _context.FitnessClasses.Remove(fitnessClass);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
