
using Newtonsoft.Json;
using Sqli_CRM_Web_Application.Connection;
using Sqli_CRM_Web_Application.Models;

namespace Sqli_CRM_Web_Application.Services
{
    public class ProductsService : IProductsService
    {
        private readonly Dynamics365Connection _connection;
        private readonly IConfiguration _configuration;
        private readonly string baseUrl;

        public ProductsService(Dynamics365Connection connection, IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = connection;
            baseUrl = $"{_configuration.GetValue<string>("DynamicsCrmSettings:Scope")}/api/data/v9.2";
        }

        // retieving all products

        async Task<List<Product>> IProductsService.GetAllProducts()
        {
            try
            {
                string accessToken = await _connection.GetAccessTokenAsync();

                var response = await _connection.CrmRequest(
                    HttpMethod.Get,
                    accessToken,
                    $"{baseUrl}/products");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to retrieve products. Status code: {response.StatusCode}");
                }

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(json);
                var op = result?.value?.ToObject<List<Product>>();

                // Handle the case where the data is not coming
                if (op == null)
                {
                    return new List<Product>();
                }

                return op;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the retrieval process
                Console.WriteLine($"Error retrieving products: {ex.Message}");
                return new List<Product>();
            }
        }


        async Task<Product> IProductsService.GetProductsByProductId(Guid productid)
        {
            try
            {
                string accessToken = await _connection.GetAccessTokenAsync();

                var response = await _connection.CrmRequest(
                    HttpMethod.Get,
                    accessToken,
                    $"{baseUrl}/products({productid})");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to retrieve product. Status code: {response.StatusCode}");
                }

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(json);
                var op = result?.ToObject<Product>();


                // Handling errors
                if (op == null)
                {
                    return new Product();
                }

                return op;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the retrieval process
                Console.WriteLine($"Error retrieving products: {ex.Message}");
                return new Product();
            }
        }

    }

}
