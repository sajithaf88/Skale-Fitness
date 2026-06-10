using Microsoft.AspNetCore.Mvc;
using SkaleFitnessMVC.Data;
using SkaleFitnessMVC.Models;

namespace SkaleFitnessMVC.Controllers
{
    public class SchedulesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchedulesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Schedules
        public IActionResult Index()
        {
            var schedules = _context.Schedules.ToList();
            return View(schedules);
        }

        // GET: Schedules/Create
        public IActionResult Create()
        {
            ViewBag.Classes = _context.FitnessClasses.ToList();
            return View();
        }

        // POST: Schedules/Create
        [HttpPost]
        public IActionResult Create(Schedule schedule)
        {
            _context.Schedules.Add(schedule);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
