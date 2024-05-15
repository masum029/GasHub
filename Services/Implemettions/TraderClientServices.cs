﻿using GasHub.Models;
using GasHub.Services.Abstractions;
using GasHub.Services.Implemettions.Base;

namespace GasHub.Services.Implemettions
{
    public class TraderClientServices : ClientRepository<Trader>, ITraderClientServices
    {

        public TraderClientServices(IHttpClientFactory httpClientFactory, ITokenService tokenService) : base(httpClientFactory,tokenService)
        {
            // if we Extaind our Uncommon Services . Then Emplement Here ........
        }
    }
}
