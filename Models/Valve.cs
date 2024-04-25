namespace GasHub.Models
{
    public class Valve : BaseModel
    {
        public string? Name { get; set; }
        public string? Unit { get; set; }
        public bool IsActive { get; set; }
    }
}
