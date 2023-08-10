using Sqli_CRM_Web_Application.Models;

namespace Sqli_CRM_Web_Application.Services
{
    public interface IProductsService
    {

        // retrieve all products
        public Task<List<Product>> GetAllProducts();

        public Task<Product> GetProductsByProductId(Guid productid);
    }
}
