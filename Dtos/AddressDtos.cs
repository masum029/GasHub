using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GasHub.Dtos
{
    public class AddressDtos
    {
        [Required]
        public Guid UserId { get; set; }
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
