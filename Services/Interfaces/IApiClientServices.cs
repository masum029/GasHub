namespace GasHub.Services.Interfaces
{
    public interface IApiClientServices
    {
        Task<string> GetAllCompanies();
        Task<string> GetResponseFromApi(string endPoint, string jsonString);
        Task<T> GetAsyns<T>(string endPoint);
        Task<T> PostAsync<T>(string endPoint, object data);
        Task<T> PutAsync<T>(string endPoint, object data);
        
        Task<bool> DeleteAsync(string endPoint, object data);
        
        

    }
}
