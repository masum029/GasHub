using System.ComponentModel.DataAnnotations.Schema;

namespace GasHub.Models
{
    public class Order : BaseModel
    {
       
        public Guid UserId { get; set; }
       
        public Guid ProductId { get; set; }
        public bool IsHold { get; set; }
        public bool IsCancel { get; set; }
        public Guid ReturnProductId { get; set; }
        public bool IsPlaced { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsDispatched { get; set; }
        public bool IsReadyToDispatch { get; set; }
        public bool IsDelivered { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual Product Product { get; set; }

    }
}
