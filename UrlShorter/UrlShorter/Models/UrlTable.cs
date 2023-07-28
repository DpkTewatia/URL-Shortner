using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrlShorter.Models
{
    public class UrlTable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string OriginalUrl { get; set; }

        [Required]
        public string ShortUrl { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public UserTable User { get; set; }

    }
}
