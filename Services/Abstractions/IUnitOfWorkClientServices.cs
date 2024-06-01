namespace GasHub.Services.Abstractions
{
    public interface IUnitOfWorkClientServices
    {
        ICompanyClientServices companyClientServices { get; }
        IDeliveryAddressClientServices deliveryAddressClientServices { get; }
        IOrderClientServices orderClientServices { get; }
        IProdReturnClientServices prodReturnClientServices { get; }
        IProductClientServices productClientServices { get; }
        IProductSizeClientServices productSizeClientServices { get; }
        IRetailerClientServices retailerClientServices { get; }
        IStockClientServices stockClientServices { get; }
        ITraderClientServices traderClientServices { get; }
        IUserClientServices userClientServices { get; }
        IValveClientServices valveClientServices { get; }
        IRegisterUserClientServices registerUserClientServices { get; }
        ILoginUserClientServices loginUserClientServices { get; }
        IProductDiscunClientServices productDiscunClientServices { get; }
    }
}
