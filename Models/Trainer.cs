using System.ComponentModel.DataAnnotations;

namespace SkaleFitnessMVC.Models
{
    public class Trainer
    {
        [Key]
        public int TrainerId { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Specialization { get; set; } = null!;

        [Required]
        public string Phone { get; set; } = null!;

        [Required]
        public string Status { get; set; } = null!;

        public string? ImagePath { get; set; }
    }
}