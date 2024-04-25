using System.ComponentModel.DataAnnotations.Schema;

namespace GasHub.Models
{
    public class Stock : BaseModel
    {
       
        public Guid ProductId { get; set; }


        public Guid TraderId { get; set; }

        public int Quantity { get; set; }
        public bool IsQC { get; set; }
        public bool IsActive { get; set; }

        // Navigation properties
        //public virtual Product? Product { get; set; }
        //public virtual Trader? Trader { get; set; }
    }
}
