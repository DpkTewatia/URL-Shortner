using System.ComponentModel.DataAnnotations;
using UrlShorter.Enums;

namespace UrlShorter.Models
{
    public class UserTable
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public RoleIdEnum RoleId { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
