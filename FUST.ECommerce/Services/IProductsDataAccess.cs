using FUST.ECommerce.Models;

namespace FUST.ECommerce.Services
{
    public interface IProductsDataAccess
    {
        void AddProduct(Product product);
        Task<IEnumerable<Product>> GetProductsAsync();
        void UpdateProduct(Product product);
    }
}