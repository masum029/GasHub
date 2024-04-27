namespace GasHub.Models.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel( List<Company> companiList, List<Valve> valveList, List<ProductSize> productSizeList)
        {
            Product = new Product();
            this.companiList = companiList;
            ValveList = valveList;
            this.productSizeList = productSizeList;
        }
        public ProductViewModel()
        {
            Product = new Product();
            this.companiList = new List<Company>();
            ValveList = new List<Valve>();
            this.productSizeList = new List<ProductSize>();
        }

        public Product Product { get; set; }
        public List<Company> companiList { get; set; }
        public List<Valve> ValveList { get; set; }
        public List<ProductSize> productSizeList { get; set; }
    }
}
