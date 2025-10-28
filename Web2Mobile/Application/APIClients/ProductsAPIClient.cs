using Application.DTOs;
using Microsoft.Extensions.Options;

namespace Application.APIClients
{

    public class ProductsAPIClient : CoreThreeAPIClientBase
    {
        private readonly APIConfig _config;
        public ProductsAPIClient(HttpClient client, IOptions<APIConfig> config) : base(client)
        {
            this._config = config.Value;
            _client.DefaultRequestHeaders.Add("x-api-key", _config.APIKey);
            Authentication = new System.Net.Http.Headers.AuthenticationHeaderValue("x-api-key", _config.APIKey);
        }


        public async Task<List<ProductDTO>> GetProductsbyCategoriesID(string productCategoryID)
        {
            var results = await Get<List<ProductDTO>>($"{_config.BaseURL}/product/api/vendor/toxZgyNaGeW/productcategories/{productCategoryID}/products?");
            return results;
        }

        public async Task<List<ProductCategory>> GetProductsCategories()
        {
            var results = await Get<List<ProductCategory>>($"{_config.BaseURL}/product/api/vendor/toxZgyNaGeW/productcategories");
            return results;
        }
	}
}