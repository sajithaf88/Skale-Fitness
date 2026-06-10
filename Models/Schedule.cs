using System.ComponentModel.DataAnnotations;
namespace SkaleFitnessMVC.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public int FitnessClassId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}