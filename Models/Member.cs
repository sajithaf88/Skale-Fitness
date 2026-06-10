using System.ComponentModel.DataAnnotations;

namespace SkaleFitnessMVC.Models
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }

        public string Gender { get; set; }
        public DateTime JoinDate { get; set; }
        public string Status { get; set; }
    }
}
