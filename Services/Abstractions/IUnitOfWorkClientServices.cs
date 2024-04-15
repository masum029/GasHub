namespace GasHub.Services.Abstractions
{
    public interface IUnitOfWorkClientServices
    {
        ICompanyClientServices companyClientServices { get; }
        IDeliveryAddressClientServices deliveryAddressClientServices { get; }
    }
}
