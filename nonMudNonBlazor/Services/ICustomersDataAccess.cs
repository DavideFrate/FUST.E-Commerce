using nonMudNonBlazor.Models;

namespace nonMudNonBlazor.Services
{
    public interface ICustomersDataAccess
    {
        void AddCustomer(int id, string CustomerName, string FirstName, string LastName, string Phone, string Address, string City, string State, string? PostalCode, string Country, decimal? CreditLimit, int? SalesRepId);
        void DeleteCustomer(int id);
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<List<Customer>> GetCustomersAsync();
    }
}