using FUST.ECommerce.Models;

namespace FUST.ECommerce.Services
{
    public interface IUsersDataAccess
    {
        Task<IEnumerable<User>> GetUsersAsync();
        void ModifyUser(User user);
        void ResetPassword(string password);
    }
}