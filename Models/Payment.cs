using System.ComponentModel.DataAnnotations;

namespace SkaleFitnessMVC.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        public int MemberId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Method { get; set; }
    }
}
