using GasHub.Services.Abstractions;

namespace GasHub.Services.Implemettions
{
    public class UnitOfWorkClientServices : IUnitOfWorkClientServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ITokenService _tokenService;
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

        public IRegisterUserClientServices registerUserClientServices { get; private set; }

        public ILoginUserClientServices loginUserClientServices { get; private set; }

        public ITokenService tokenServices { get; private set; }

        public IProductDiscunClientServices productDiscunClientServices { get; private set; }

        public UnitOfWorkClientServices(IHttpClientFactory httpClientFactory, IHttpContextAccessor contextAccessor, ITokenService tokenService)
        {
            _httpClientFactory = httpClientFactory;
            _contextAccessor = contextAccessor;
            _tokenService = tokenService;
            companyClientServices = new CompanyClientServices(httpClientFactory, tokenService);
            deliveryAddressClientServices = new DeliveryAddressClientServices(httpClientFactory, tokenService);
            orderClientServices = new OrderClientServices(httpClientFactory, tokenService);
            productSizeClientServices = new ProductSizeClientServices(httpClientFactory,tokenService);
            productClientServices = new ProductClientServices(httpClientFactory, tokenService);
            prodReturnClientServices = new ProdReturnClientServices(httpClientFactory, tokenService);
            retailerClientServices = new RetailerClientServices(httpClientFactory, tokenService);
            stockClientServices = new StockClientServices(httpClientFactory, tokenService);
            traderClientServices = new TraderClientServices(httpClientFactory, tokenService);
            userClientServices = new UserClientServices(httpClientFactory, tokenService);
            valveClientServices = new ValveClientServices(httpClientFactory, tokenService);
            registerUserClientServices = new RegisterUserClientServices(httpClientFactory, tokenService);
            loginUserClientServices = new LoginUserClientServices(httpClientFactory, tokenService);
            productDiscunClientServices = new ProductDiscunClientServices(httpClientFactory, tokenService);
            
        }
    }
}
