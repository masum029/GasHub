using System.ComponentModel;

namespace GasHub.Models
{
    public class User : BaseModel
    {
        [DisplayName("User Name")]
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserImg { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DeactivatedDate { get; set; }
        public string? DeactiveBy { get; set; }
        public string? TIN { get; set; }
        public bool? IsBlocked { get; set; }

        // Navigation properties
        //public virtual ICollection<DeliveryAddress>? DeliveryAddresses { get; set; }
        //public virtual ICollection<Order>? Orders { get; set; }
    }
}
