using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GasHub.Models
{
    public class DeliveryAddress : BaseModel
    {
        [Required(ErrorMessage ="mast select User ")]
        [DisplayName("User Name")]
        public Guid UserId { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DeactivatedDate { get; set; }
        public string? DeactiveBy { get; set; }
        public bool? IsDefault { get; set; }

    }
}
