using System.ComponentModel.DataAnnotations;

namespace SkaleFitnessMVC.Models
{
    public class Attendance
    {
        [Key]
        public int AttendanceId { get; set; }

        public int MemberId { get; set; }
        public int FitnessClassId { get; set; }

        public DateTime AttendanceDate { get; set; }
        public string Status { get; set; }   // Present / Absent
    }
}
