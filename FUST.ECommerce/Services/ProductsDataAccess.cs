using FUST.ECommerce.Models;
using MySqlConnector;

namespace FUST.ECommerce.Services
{
    public class ProductsDataAccess : IProductsDataAccess
    {
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            try
            {
                 var products = new List<Product>();

                using (var connection = new MySqlConnection("DefaultConnection"))
                {
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand("SELECT * FROM products", connection))
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            products.Add(new Product
                            {
                                Id = reader.GetInt32("productID"),
                                Name = reader.GetString("name"),
                                Price = reader.GetDecimal("price"),
                                CategoryID = reader.GetInt32("categoryID")
                            });
                        }
                    }
                }

                return products;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving products", ex);
            }
        }

        public void AddProduct(Product product)
        {
            try
            {
                using (var connection = new MySqlConnection("DefaultConnection"))
                {
                    connection.Open();
                    using (var command = new MySqlCommand("INSERT INTO products (name, price, categoryID) VALUES (@name, @price, @categoryID)", connection))
                    {
                        command.Parameters.AddWithValue("@name", product.Name);
                        command.Parameters.AddWithValue("@price", product.Price);
                        command.Parameters.AddWithValue("@categoryID", product.CategoryID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding a product", ex);
            }
        }



        public void UpdateProduct(Product product)
        {
            try
            {
                using (var connection = new MySqlConnection("DefaultConnection"))
                {
                    connection.Open();
                    using (var command = new MySqlCommand("UPDATE products SET name = @name, price = @price, categoryID = @categoryID"))
                    {
                        command.Parameters.AddWithValue("@name", product.Name);
                        command.Parameters.AddWithValue("@price", product.Price);
                        command.Parameters.AddWithValue("@categoryID", product.CategoryID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating a product", ex);
            }
        }
    }
}