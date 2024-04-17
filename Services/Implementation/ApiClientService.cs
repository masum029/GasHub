using GasHub.Services.Interfaces;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;

namespace GasHub.Services.Implementation
{
    public class ApiClientService : IApiClientServices
    {
        private readonly HttpClient _httpClient;
        public ApiClientService(HttpClient httpClient)
        {
            HttpClientHandler handler = new HttpClientHandler();
            httpClient = new HttpClient(handler);
            // Configure the handler to accept all certificates
            handler.ServerCertificateCustomValidationCallback = ValidateCertificate;

            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri("https://localhost:7128/api/");
            //_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


        }
        private static bool ValidateCertificate(HttpRequestMessage request, X509Certificate2 certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // Accept all certificates, even if there are errors
            return true;
        }
        public async Task<string> GetResponseFromApi(string endPoint, string jsonString)
        {
            // Create an HttpClientHandler
            HttpClientHandler handler = new HttpClientHandler();

            // Configure the handler to accept all certificates
            handler.ServerCertificateCustomValidationCallback = ValidateCertificate;

            // Create HttpClient with the custom handler
            HttpClient client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:7128/api/");

            // Make your API request using the HttpClient instance
            HttpResponseMessage response = await client.GetAsync(endPoint);

            //HttpResponseMessage response = await _httpClient.GetAsync(endPoint);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            else
            {
                throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
            }

        }
        public async Task<string> GetAllCompanies()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("Company/getAllCompany");

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            else
            {
                throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
            }
        }

        public async Task<T> GetAsyns<T>(string endPoint)
        {
            try
            {

                HttpResponseMessage response = await _httpClient.GetAsync(endPoint);
                response.EnsureSuccessStatusCode();

                // Read the response content as a string
                string responseContent = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON string using System.Text.Json
                return JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true // Ignore case when matching property names
                });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<T> PostAsync<T>(string endPoint, object data)
        {
            try
            {
                //HttpResponseMessage response = await _httpClient.PostAsJsonAsync(endPoint, data);

                //response.EnsureSuccessStatusCode();

                //string content = await response.Content.ReadAsStringAsync();

                //T result= JsonSerializer.Deserialize<T>(content);
                //return result;


                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(endPoint, data);
                //response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                T result = JsonSerializer.Deserialize<T>(content);
                return result;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<T> PutAsync<T>(string endPoint, object data)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync(endPoint, data);
                response.EnsureSuccessStatusCode();

                string content= await response.Content.ReadAsStringAsync();

                T result=JsonSerializer.Deserialize<T>(content);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<bool> DeleteAsync(string endPoint, object data)
        {
            // Serialize the object to a query string or any format suitable for your API
            string queryString = ConvertDataToQueryString(data);

            // Combine the endpoint with the query string
            string requestUri = $"{endPoint}?{queryString}";

            // Send the DELETE request
            HttpResponseMessage response = await _httpClient.DeleteAsync(requestUri);

            // Return true if the response indicates success (2xx status code)
            return response.IsSuccessStatusCode;
        }

        // Method to convert object data to a query string (or any suitable format)
        private string ConvertDataToQueryString(object data)
        {
            // Implement logic to convert the object to a query string
            // For example, you could use reflection or a library like System.Text.Json or Newtonsoft.Json
            // Here's a simple example using System.Text.Json:
            return System.Text.Json.JsonSerializer.Serialize(data);
        }

        
    }
}
