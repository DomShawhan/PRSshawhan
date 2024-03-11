using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRSshawhan.Models
{
    [Table("LineItem")]
    public class LineItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RequestId { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; } = 1;
        // navigation properties
        public Request? Request { get; set; }
        public Product? Product { get; set; }

    }
}
