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
        [Required]
        [DisplayName("Contact Number")]
        public string ContactNumber { get; set; }
        [Required]
        [DisplayName("Division")]
        public string Division { get; set; }
        [Required]
        [DisplayName("District")]
        public string District { get; set; }
        [Required]
        [DisplayName("Sub-district (Upazilla)")]
        public string Subdistrict { get; set; }
        [Required]
        [DisplayName("Area (Nearest area)")]
        public string Area { get; set; }
        [Required]
        [DisplayName("House/Holding")]
        public string HouseHolding { get; set; }
        [Required]
        [DisplayName("Street Address")]
        public string StreetAddress { get; set; }
        [Required]
        [DisplayName("Postal Code")]
        public string postCode { get; set; }

    }
}
