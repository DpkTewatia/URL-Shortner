using System.ComponentModel.DataAnnotations;

namespace UrlShorter.Models
{
    public class UserTable
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int RoleId { get; set; }
    }
}
