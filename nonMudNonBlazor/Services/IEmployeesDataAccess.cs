using nonMudNonBlazor.Models;

namespace nonMudNonBlazor.Services
{
    public interface IEmployeesDataAccess
    {
        void AddEmployee(int Id, string FirstName, string LastName, string Extention, string Email, string OfficeCode, string? JobTitle, int? ReportsTo);
        void DeleteEmployee(int id);
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<List<Employee>> GetEmployeesAsync();
    }
}