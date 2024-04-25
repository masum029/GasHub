namespace GasHub.Models
{
    public class ProductSize : BaseModel
    {
        public decimal Size { get; set; }
        public string? Unit { get; set; }
        public bool IsActive { get; set; }
    }
}
