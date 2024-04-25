namespace GasHub.Models
{
    public class Trader : BaseModel
    {
        public Guid CompanyId { get; set; }
        public string? Name { get; set; }
        public string? Contactperson { get; set; }
        public string? ContactPerNum { get; set; }
        public string? ContactNumber { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DeactivatedDate { get; set; }
        public string? DeactiveBy { get; set; }
        public string? BIN { get; set; }

        // Navigation properties
        //public virtual ICollection<Stock>? Stocks { get; set; }
    }
}
