using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class UserDto
    {
        public int UserId { get; set; }
        [EmailAddress]
        [Required]
        public string? UserName { get; set; }
        [MaxLength(15)]
        [Required]
        public string? Password { get; set; }
        [MaxLength(20)]
        [Required]
        public string? FirstName { get; set; }
        [MaxLength(20)]
        public string? LastName { get; set; }
    }
}
