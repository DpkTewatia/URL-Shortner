using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace UrlShorter.Models.ViewModels
{
    public class UrlTableViewModel
    {
        public int Id { get; set; }

      
        public string OriginalUrl { get; set; }

        
        public string ShortUrl { get; set; }

        
        public DateTime CreationDate { get; set; }

       
        public int? UserId  { get; set; }
        public bool AllowDelete { get; set; }
    }
}
