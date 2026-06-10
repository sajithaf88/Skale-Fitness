using Microsoft.AspNetCore.Mvc;
using SkaleFitnessMVC.Data;
using SkaleFitnessMVC.Models;

namespace SkaleFitnessMVC.Controllers
{
    public class TrainersController : Controller
    {
        private readonly ApplicationDbContext _context;
    public TrainersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔐 ADMIN OR TRAINER CHECK
        private bool IsAdminOrTrainer()
        {
            if (HttpContext.Session.GetString("Username") == null)
                return false;

            var role = HttpContext.Session.GetString("Role");
            return role == "Admin" || role == "Trainer";
        }

        // LIST
        public IActionResult Index()
        {
            if (!IsAdminOrTrainer())
                return RedirectToAction("Login", "Account");

            return View(_context.Trainers.ToList());
        }

        // DETAILS
        public IActionResult Details(int id)
        {
            if (!IsAdminOrTrainer())
                return RedirectToAction("Login", "Account");

            var trainer = _context.Trainers.Find(id);
            if (trainer == null) return NotFound();

            return View(trainer);
        }

        // CREATE
        public IActionResult Create()
        {
            if (!IsAdminOrTrainer())
                return RedirectToAction("Login", "Account");

            return View();
        }

        [HttpPost]
        public IActionResult Create(Trainer trainer, IFormFile ImageFile)
        {
            if (!IsAdminOrTrainer())
                return RedirectToAction("Login", "Account");

            if (ImageFile != null && ImageFile.Length > 0)
            {
                string folder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/images/trainers"
                );

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                string path = Path.Combine(folder, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    ImageFile.CopyTo(stream);
                }

                trainer.ImagePath = fileName;
            }

            trainer.Status ??= "Active";

            _context.Trainers.Add(trainer);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // EDIT
        public IActionResult Edit(int id)
        {
            if (!IsAdminOrTrainer())
                return RedirectToAction("Login", "Account");

            var trainer = _context.Trainers.Find(id);
            if (trainer == null) return NotFound();

            return View(trainer);
        }

        [HttpPost]
        public IActionResult Edit(Trainer trainer, IFormFile ImageFile)
        {
            if (!IsAdminOrTrainer())
                return RedirectToAction("Login", "Account");

            var existing = _context.Trainers.Find(trainer.TrainerId);
            if (existing == null) return NotFound();

            if (ImageFile != null && ImageFile.Length > 0)
            {
                string folder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/images/trainers"
                );

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                string path = Path.Combine(folder, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    ImageFile.CopyTo(stream);
                }

                existing.ImagePath = fileName;
            }

            existing.Name = trainer.Name;
            existing.Specialization = trainer.Specialization;
            existing.Phone = trainer.Phone;
            existing.Status = trainer.Status ?? "Active";

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            if (!IsAdminOrTrainer())
                return RedirectToAction("Login", "Account");

            var trainer = _context.Trainers.Find(id);
            if (trainer == null) return NotFound();

            return View(trainer);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!IsAdminOrTrainer())
                return RedirectToAction("Login", "Account");

            var trainer = _context.Trainers.Find(id);

            if (trainer != null)
            {
                _context.Trainers.Remove(trainer);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }

}
