using Microsoft.AspNetCore.Mvc;
using SkaleFitnessMVC.Data;

namespace SkaleFitnessMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;


    public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ================= HOME PAGE =================
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Username") == null)
                return RedirectToAction("Login", "Account");

            return View();
        }

        // ================= DASHBOARD =================
        public IActionResult Dashboard()
        {
            // Check Login
            if (HttpContext.Session.GetString("Username") == null)
                return RedirectToAction("Login", "Account");

            // ---------- Dashboard Cards ----------
            ViewBag.MemberCount = _context.Members.Count();
            ViewBag.TrainerCount = _context.Trainers.Count();
            ViewBag.ClassCount = _context.FitnessClasses.Count();
            ViewBag.Revenue = _context.Payments.Sum(p => (decimal?)p.Amount) ?? 0;

            // ---------- Charts ----------
            ViewBag.NewMembers = new int[] { 2, 4, 3, 5, 1 };

            ViewBag.MaleCount = _context.Members.Count(m => m.Gender == "Male");
            ViewBag.FemaleCount = _context.Members.Count(m => m.Gender == "Female");

            // ---------- Recent Members ----------
            ViewBag.RecentMembers = _context.Members
                .OrderByDescending(m => m.MemberId)
                .Take(5)
                .ToList();

            // ---------- Monthly Revenue (Sample Data) ----------
            ViewBag.MonthlyRevenue = new int[] { 20000, 30000, 25000, 40000, 35000, 50000 };

            return View();
        }
    }


}
