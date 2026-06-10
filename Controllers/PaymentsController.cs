using Microsoft.AspNetCore.Mvc;
using SkaleFitnessMVC.Data;
using SkaleFitnessMVC.Models;

namespace SkaleFitnessMVC.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔐 ADMIN CHECK
        private bool IsAdmin()
        {
            return HttpContext.Session.GetString("Username") != null
                && HttpContext.Session.GetString("Role") == "Admin";
        }

        // INDEX
        public IActionResult Index()
        {
            if (!IsAdmin())
                return RedirectToAction("Index", "Home");

            return View(_context.Payments.ToList());
        }

        // CREATE
        public IActionResult Create()
        {
            if (!IsAdmin())
                return RedirectToAction("Index", "Home");

            ViewBag.Members = _context.Members.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Payment payment)
        {
            if (!IsAdmin())
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                _context.Payments.Add(payment);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Members = _context.Members.ToList();
            return View(payment);
        }

        // EDIT
        public IActionResult Edit(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Index", "Home");

            var payment = _context.Payments.Find(id);
            if (payment == null) return NotFound();

            ViewBag.Members = _context.Members.ToList();
            return View(payment);
        }

        [HttpPost]
        public IActionResult Edit(Payment payment)
        {
            if (!IsAdmin())
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                _context.Payments.Update(payment);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Members = _context.Members.ToList();
            return View(payment);
        }

        // DETAILS
        public IActionResult Details(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Index", "Home");

            var payment = _context.Payments.Find(id);
            if (payment == null) return NotFound();

            return View(payment);
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Index", "Home");

            var payment = _context.Payments.Find(id);
            if (payment == null) return NotFound();

            return View(payment);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Index", "Home");

            var payment = _context.Payments.Find(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
