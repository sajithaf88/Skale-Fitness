using System.ComponentModel.DataAnnotations;

namespace SkaleFitnessMVC.Models
{
    public class FitnessClass
    {
        [Key]
        public int FitnessClassId { get; set; }

        public string ClassName { get; set; }
        public int TrainerId { get; set; }
    }
}
