using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRSshawhan.Models
{
    [Table("Request")]
    public class Request
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Justification { get; set; }
        
        [Required]
        public DateTime DateNeeded { get; set; }

        [StringLength(25)]
        public string DeliveryMode { get; set; } = "Pickup";

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "NEW";

        [Required]
        public decimal Total { get; set; } = 0m;
        
        [Required]
        public DateTime SubmittedDate { get; set; } = DateTime.Now;

        [StringLength(100)]
        public string? ReasonForRejection { get; set; }

        public User? User { get; set; }
        public List<LineItem> LineItems { get; set; }
    }
}
