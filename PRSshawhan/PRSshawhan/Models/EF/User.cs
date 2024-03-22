using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PRSshawhan.Models.EF;

namespace PRSshawhan.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        [Required]
        [StringLength(10)]
        public string Password { get; set; }

        [Required]
        [StringLength (20)]
        public string Firstname { get; set; }

        [Required]
        [StringLength(20)]
        public string Lastname { get; set; }

        [StringLength(12)]
        public string? Phone { get; set; }

        [StringLength(75)]
        public string? Email { get; set; }

        [Required]
        public bool Reviewer { get; set; }

        [Required]
        public bool Admin { get; set; }

        public List<Request>? Requests { get; set; }
    }
}
