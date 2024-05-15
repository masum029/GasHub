namespace GasHub.Services.Abstractions.Base
{
    public interface IClientRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id, string EndPoint);
        Task<List<T>> GetAllAsync(string EndPoint);
        Task<bool> AddAsync(T model, string EndPoint);
        Task<bool> UpdateAsync(Guid id, T model, string EndPoint);
        Task<bool> DeleteAsync(Guid id, string EndPoint);
        Task<(bool Success, string ErrorMessage , string token)> LoginAsync(T model, string EndPoint);
    }
}
