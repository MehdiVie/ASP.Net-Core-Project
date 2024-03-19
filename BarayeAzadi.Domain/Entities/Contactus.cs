using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarayeAzadi.Domain.Entities
{
    public class Contactus
    {
        [Key]
        public int ContactusId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 4)]
        [RegularExpression(@"^([a-zA-Z \.\&\'\-]+)$", ErrorMessage = "Please enter valid Name!")]
        [Display(Name = "Full Name")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$|^\+?\d{0,2}\-?\d{4,5}\-?\d{5,6}", 
            ErrorMessage = "Please enter valid Email!")]
        public string Email { get; set; }
        [StringLength(20, MinimumLength = 9)]
        [RegularExpression(@"^([0-9\.\&\'\-]+)$", 
            ErrorMessage = "Please enter valid Phone!ONLY NUMBERS! (DO NOT USE: +,empty sapce,alphabets)")]
        public string? Phone { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
        public DateTime? Created_Date { get; set; }
        [NotMapped]
        public IFormFile? AttachedFile { get; set; }

        [Display(Name = "AttachFile Url")]
        public string? AttachedFileUrl { get; set; }
        
    }
}
