﻿using GasHub.Models;
using GasHub.Services.Abstractions;
using GasHub.Services.Implemettions.Base;

namespace GasHub.Services.Implemettions
{
    public class ValveClientServices : ClientRepository<Valve>, IValveClientServices
    {

        public ValveClientServices(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            // if we Extaind our Uncommon Services . Then Emplement Here ........
        }
    }
}
