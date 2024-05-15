namespace GasHub.Services.Abstractions
{
    public interface ITokenService
    {
        void SaveToken(string token);
        string ? GetToken();
        void ClearToken();
    }
}
