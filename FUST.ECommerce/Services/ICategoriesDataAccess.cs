using FUST.ECommerce.Models;

namespace FUST.ECommerce.Services
{
    public interface ICategoriesDataAccess
    {
        void AddCategory(Category category);
        Task<IEnumerable<Category>> GetCategories();
        void ModifyCategory(Category category);
    }
}