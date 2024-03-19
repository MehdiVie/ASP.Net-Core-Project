using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Range(3, 50)]
        [Display(Name = "Full Name")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
        public DateTime? Created_Date { get; set; }
        public IFormFile? AttachedFile { get; set; }
        [Display(Name = "AttachFile Url")]
        public string? AttachedFileUrl { get; set; }
    }
}
