using System.ComponentModel.DataAnnotations;

namespace PRSshawhan.Models.DTOs
{
    // DTO to get data for the login method
    public record LoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
