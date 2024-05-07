using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace GasHub.Models
{
    public class Company : BaseModel
    {
        
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Contactperson is required")]
        public string? Contactperson { get; set; }

        [RegularExpression(@"^\d{11}$", ErrorMessage = "Contact person number should be 11 digits")]
        public string? ContactPerNum { get; set; }

        [RegularExpression(@"^\d{11}$", ErrorMessage = "Contact number should be 11 digits")]
        public string? ContactNumber { get; set; }
        public bool? IsActive { get; set; }
        [Required(ErrorMessage = "DeactivatedDate is required")]
        public DateTime? DeactivatedDate { get; set; }
        public string? DeactiveBy { get; set; }

        [RegularExpression(@"^\d{9}$", ErrorMessage = "BIN should be 9 digits")]
        public string? BIN { get; set; }
        // Navigation properties
        public virtual ICollection<Product>? Products { get; set; }
        public virtual ICollection<Trader>? Traders { get; set; }
    }
}
