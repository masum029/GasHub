namespace GasHub.Models
{
    public class Company
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Contactperson { get; set; }
        public string? ContactPerNum { get; set; }
        public string? ContactNumber { get; set; }
        public string? CreatedBy { get; set; } = "Masum";
        public string? UpdatedBy { get; set; } = "Masum";
        public bool? IsActive { get; set; } = true;
        public DateTime? DeactivatedDate { get; set; }= DateTime.Now;
        public string? DeactiveBy { get; set; }
        public string? BIN { get; set; }
    }
}
