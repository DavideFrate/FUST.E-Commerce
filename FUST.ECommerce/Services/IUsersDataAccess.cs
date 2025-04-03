using FUST.ECommerce.Models;

namespace FUST.ECommerce.Services
{
    public interface IUsersDataAccess
    {
        void AddUser(string email, string password, string? name = null, string? surname = null, bool? isadmin = null);
        Task<User> GetUserByEmailAsync(string email);
        Task<IEnumerable<User>> GetUsersAsync();
        void ModifyUser(User user);
        void ResetPassword(string password, int? id = null, string? email = null);
    }
}