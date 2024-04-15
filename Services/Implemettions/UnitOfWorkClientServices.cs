using GasHub.Services.Abstractions;

namespace GasHub.Services.Implemettions
{
    public class UnitOfWorkClientServices : IUnitOfWorkClientServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ICompanyClientServices companyClientServices { get; private set; }

        public IDeliveryAddressClientServices deliveryAddressClientServices { get; private set; }

        public UnitOfWorkClientServices(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            companyClientServices = new CompanyClientServices(httpClientFactory);
            deliveryAddressClientServices = new DeliveryAddressClientServices(httpClientFactory);
        }
    }
}
