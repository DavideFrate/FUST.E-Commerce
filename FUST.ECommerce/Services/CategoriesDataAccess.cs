using FUST.ECommerce.Models;
using MySqlConnector;

namespace FUST.ECommerce.Services
{
    public class CategoriesDataAccess : ICategoriesDataAccess
    {
        public async Task<IEnumerable<Category>> GetCategories()
        {
            try
            {
                var categories = new List<Category>();

                using (var connection = new MySqlConnection("DefaultConnection"))
                {
                    await connection.OpenAsync();
                    using (var command = new MySqlCommand("SELECT * FROM categories", connection))
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            categories.Add(new Category
                            {
                                Id = reader.GetInt32("categoryID"),
                                Name = reader.GetString("name")
                            });
                        }
                    }
                }

                return categories;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving categories", ex);
            }
        }

        public void AddCategory(Category category)
        {
            try
            {
                using (var connection = new MySqlConnection("DefaultConnection"))
                {
                    connection.Open();
                    using (var command = new MySqlCommand("INSERT INTO categories (name) VALUES (@name)", connection))
                    {
                        command.Parameters.AddWithValue("@name", category.Name);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding a category", ex);
            }
        }

        public void ModifyCategory(Category category)
        {
            try
            {
                using (var connection = new MySqlConnection("DefaultConnection"))
                {
                    connection.Open();
                    using (var command = new MySqlCommand("UPDATE categories SET name = @name WHERE categoryID = @categoryID", connection))
                    {
                        command.Parameters.AddWithValue("@name", category.Name);
                        command.Parameters.AddWithValue("@categoryID", category.Id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while modifying a category", ex);
            }
        }
    }
}
