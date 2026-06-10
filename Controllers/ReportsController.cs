using Microsoft.AspNetCore.Mvc;
using SkaleFitnessMVC.Data;

namespace SkaleFitnessMVC.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔐 ADMIN CHECK
        private bool IsAdmin()
        {
            return HttpContext.Session.GetString("Username") != null
                && HttpContext.Session.GetString("Role") == "Admin";
        }

        // REPORTS HOME
        public IActionResult Index()
        {
            if (!IsAdmin())
                return RedirectToAction("Index", "Home");

            return View();
        }

        // MEMBERS REPORT
        public IActionResult MembersReport()
        {
            if (!IsAdmin())
                return RedirectToAction("Index", "Home");

            var members = _context.Members.ToList();
            return View(members);
        }

        // PAYMENTS REPORT
        public IActionResult PaymentsReport()
        {
            if (!IsAdmin())
                return RedirectToAction("Index", "Home");

            var payments = _context.Payments.ToList();
            ViewBag.TotalRevenue = payments.Sum(p => p.Amount);
            return View(payments);
        }

        // ATTENDANCE REPORT
        public IActionResult AttendanceReport()
        {
            if (!IsAdmin())
                return RedirectToAction("Index", "Home");

            var attendance = _context.Attendances.ToList();
            return View(attendance);
        }
    }
}
