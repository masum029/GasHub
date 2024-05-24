namespace GasHub.Models.ViewModels
{
    public class ProductViewModel
    {
        public List<Product> ProductList { get; set; }
        public List<Company> companiList { get; set; }
        public List<Valve> ValveList { get; set; }
        public List<ProductSize> productSizeList { get; set; }
    }
}
