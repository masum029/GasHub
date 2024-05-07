using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace GasHub.Models
{
    public class Company : BaseModel
    {
        
        public string? Name { get; set; }
        public string? Contactperson { get; set; }
        public string? ContactPerNum { get; set; }
        public string? ContactNumber { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DeactivatedDate { get; set; }
        public string? DeactiveBy { get; set; }

        public string? BIN { get; set; }
        // Navigation properties
        public virtual ICollection<Product>? Products { get; set; }
        public virtual ICollection<Trader>? Traders { get; set; }
    }
}
