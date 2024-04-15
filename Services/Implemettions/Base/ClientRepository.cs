using GasHub.Services.Abstractions.Base;
using Newtonsoft.Json;
using System.Text;

namespace GasHub.Services.Implemettions.Base
{
    public class ClientRepository<T> : IClientRepository<T> where T : class
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ClientRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> AddAsync(T model, string EndPoint)
        {
            var httpClient = _httpClientFactory.CreateClient("GasHubClient");
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(EndPoint, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new HttpRequestException($"Failed to add resource. Status code: {response.StatusCode}");
            }
        }

        public async Task<bool> DeleteAsync(Guid id, string EndPoint)
        {
            var httpClient = _httpClientFactory.CreateClient("GasHubClient");
            var response = await httpClient.DeleteAsync($"{EndPoint}/{id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new HttpRequestException($"Failed to delete resource. Status code: {response.StatusCode}");
            }
        }

        public async Task<List<T>> GetAllAsync(string EndPoint)
        {
            var httpClient = _httpClientFactory.CreateClient("GasHubClient");
            var response = await httpClient.GetAsync(EndPoint);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<T>>(jsonString);
            }
            else
            {
                throw new HttpRequestException($"Failed to retrieve resources. Status code: {response.StatusCode}");
            }
        }

        public async Task<T> GetByIdAsync(Guid id, string EndPoint)
        {
            var httpClient = _httpClientFactory.CreateClient("GasHubClient");
            var response = await httpClient.GetAsync($"{EndPoint}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            else
            {
                throw new HttpRequestException($"Failed to retrieve resource. Status code: {response.StatusCode}");
            }
        }

        public async Task<bool> UpdateAsync(Guid id, T model, string EndPoint)
        {
            var httpClient = _httpClientFactory.CreateClient("GasHubClient");
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"{EndPoint}/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new HttpRequestException($"Failed to update resource. Status code: {response.StatusCode}");
            }
        }
    }
}
