using System.ComponentModel.DataAnnotations.Schema;

namespace GasHub.Models
{
    public class ProdReturn : BaseModel
    {
       
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string ProdSizeId { get; set; }
        public string ProdValveId { get; set; }


        // Navigation property
        //public virtual Product Product { get; set; }
    }
}
