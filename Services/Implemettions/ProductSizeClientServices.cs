﻿using GasHub.Models;
using GasHub.Services.Abstractions;
using GasHub.Services.Implemettions.Base;

namespace GasHub.Services.Implemettions
{
    public class ProductSizeClientServices : ClientRepository<ProductSize>, IProductSizeClientServices
    {

        public ProductSizeClientServices(IHttpClientFactory httpClientFactory, ITokenService tokenService) : base(httpClientFactory, tokenService)
        {
            // if we Extaind our Uncommon Services . Then Emplement Here ........
        }
    }
}
