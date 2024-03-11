using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRSshawhan.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int VendorId { get; set; }

        [Required]
        [StringLength(50)]
        public string PartNumber { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [StringLength(255)]
        public string Unit { get; set; }

        [StringLength(255)]
        public string? PhotoPath { get; set; }

        // Navigation Property - not in sql
        public Vendor? Vendor { get; set; } 
    }
}
