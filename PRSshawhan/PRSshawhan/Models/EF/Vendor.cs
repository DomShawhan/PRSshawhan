using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using PRSshawhan.Models.EF;

namespace PRSshawhan.Models
{
    [Table("Vendor")]
    public class Vendor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        [Required]
        [StringLength(255)]
        public string City { get; set; }

        [Required]
        [StringLength(2)]
        public string State { get; set; }

        [Required]
        [StringLength(5)]
        public string Zip { get; set; }

        [StringLength(12)]
        public string? Phone { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }

        // navigation property

        public List<Product>? Products { get; set; }
    }
}
