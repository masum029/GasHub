using GasHub.Models;
using GasHub.Services.Abstractions;
using GasHub.Services.Implemettions.Base;

namespace GasHub.Services.Implemettions
{
    public class UserClientServices : ClientRepository<User>, IUserClientServices
    {

        public UserClientServices(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            // if we Extaind our Uncommon Services . Then Emplement Here ........
        }
    }
}
