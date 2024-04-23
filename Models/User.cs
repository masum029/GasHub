using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace GasHub.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string? UserImg { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DeactivatedDate { get; set; }
        public string? DeactiveBy { get; set; }
        public string? TIN { get; set; }
        public bool? IsBlocked { get; set; }

        // Navigation properties
        public virtual ICollection<DeliveryAddress>? DeliveryAddresses { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }

    }
    public class Order : BaseEntity
    {
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public bool IsHold { get; set; }
        public bool IsCancel { get; set; }
        public string ReturnProductId { get; set; }
        public bool IsPlaced { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsDispatched { get; set; }
        public bool IsReadyToDispatch { get; set; }
        public bool IsDelivered { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual Product Product { get; set; }

    }
    public class Product : BaseEntity
    {
        [ForeignKey("Company")]
        public Guid CompanyId { get; set; }
        public string? Name { get; set; }
        [ForeignKey("Size")]
        public Guid ProdSizeId { get; set; }
        [ForeignKey("Valve")]
        public Guid ProdValveId { get; set; }
        public string? ProdImage { get; set; }
        public bool IsActive { get; set; }

        // Navigation properties
        public virtual Company Company { get; set; }
        public virtual ProductSize Size { get; set; }
        public virtual Valve Valve { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ProdReturn> Returns { get; set; }

    }
    public class ProdReturn : BaseEntity
    {
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string ProdSizeId { get; set; }
        public string ProdValveId { get; set; }


        // Navigation property
        public virtual Product Product { get; set; }

    }
    public class Valve : BaseEntity
    {
        public string? Name { get; set; }
        public string? Unit { get; set; }
        public bool IsActive { get; set; }

    }
    public class ProductSize : BaseEntity
    {
        public decimal Size { get; set; }
        public string? Unit { get; set; }
        public bool IsActive { get; set; }
    }

    public class DeliveryAddress : BaseEntity
    {

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DeactivatedDate { get; set; }
        public string? DeactiveBy { get; set; }
        public bool? IsDefault { get; set; }

        // Navigation property
        public virtual User User { get; set; }

    }
}
