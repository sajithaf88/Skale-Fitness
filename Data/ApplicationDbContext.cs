using Microsoft.EntityFrameworkCore;
using SkaleFitnessMVC.Models;

namespace SkaleFitnessMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<FitnessClass> FitnessClasses { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}
