using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BarayeAzadi.Domain.Entities
{
    public class Statement
    {
        [Key]
        public int StatementId { get; set; }
        public bool Show { get; set; } = true;
        public string Type { get; set; }
        public string Language { get; set; }
        public DateOnly? Created_Date { get; set; }
        public string Title { get; set; }
        public string? Brief { get; set; }
        [DataType(DataType.MultilineText)]
        public string? Text { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
        [Display(Name = "Image Url")]
        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile? Pdf { get; set; }
        [Display(Name = "Pdf Url")]
        public string? PdfUrl { get; set; }
        [NotMapped]
        public IFormFile? Media { get; set; }
        [Display(Name = "Media Url")]
        public string? MediaUrl { get; set; }
    }
}
