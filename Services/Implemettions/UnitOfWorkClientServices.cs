using GasHub.Services.Abstractions;

namespace GasHub.Services.Implemettions
{
    public class UnitOfWorkClientServices : IUnitOfWorkClientServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ICompanyClientServices companyClientServices { get; private set; }

        public IDeliveryAddressClientServices deliveryAddressClientServices { get; private set; }

        public IOrderClientServices orderClientServices { get; private set; }

        public IProdReturnClientServices prodReturnClientServices { get; private set; }

        public IProductClientServices productClientServices { get; private set; }

        public IProductSizeClientServices productSizeClientServices { get; private set; }

        public IRetailerClientServices retailerClientServices { get; private set; }

        public IStockClientServices stockClientServices { get; private set; }

        public ITraderClientServices traderClientServices { get; private set; }

        public IUserClientServices userClientServices { get; private set; }

        public IValveClientServices valveClientServices { get; private set; }

        public UnitOfWorkClientServices(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            companyClientServices = new CompanyClientServices(httpClientFactory);
            deliveryAddressClientServices = new DeliveryAddressClientServices(httpClientFactory);
            orderClientServices = new OrderClientServices(httpClientFactory);
            productSizeClientServices = new ProductSizeClientServices(httpClientFactory);
            productClientServices = new ProductClientServices(httpClientFactory);
            prodReturnClientServices = new ProdReturnClientServices(httpClientFactory);
            retailerClientServices = new RetailerClientServices(httpClientFactory);
            stockClientServices = new StockClientServices(httpClientFactory);
            traderClientServices = new TraderClientServices(httpClientFactory);
            userClientServices = new UserClientServices(httpClientFactory);
            valveClientServices = new ValveClientServices(httpClientFactory);
        }
    }
}
