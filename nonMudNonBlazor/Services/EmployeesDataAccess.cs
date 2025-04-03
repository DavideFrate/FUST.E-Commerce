using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver.Core.Configuration;
using MySqlConnector;
using nonMudNonBlazor.Models;

namespace nonMudNonBlazor.Services
{
    public class EmployeesDataAccess : IEmployeesDataAccess
    {
        private readonly string _connectionString;
        public EmployeesDataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //GET Employees
        public async Task<List<Employee>> GetEmployeesAsync()
        {
            try
            {
                var employees = new List<Employee>();
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand("SELECT * FROM employees", connection))
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            employees.Add(new Employee
                            {
                                Id = reader.GetInt32("employeeNumber"),
                                FirstName = reader.GetString("firstName"),
                                LastName = reader.GetString("lastName"),
                                Extention = reader.GetString("extention"),
                                Email = reader.GetString("email"),
                                OfficeCode = reader.GetString("officeCode"),
                                ReportsTo = reader["reportsTo"] as Int32 ?,
                                JobTitle = reader.GetString("jobTitle")
                            });
                        }
                    }
                }
                return employees;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving employees", ex);
            }
        }

        //GET Employee by Id
        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            try
            {
                var employee = new Employee();
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand($"SELECT * FROM employees WHERE employeeNumber = {id}", connection))
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            employee = new Employee
                            {
                                Id = reader.GetInt32("employeeNumber"),
                                FirstName = reader.GetString("firstName"),
                                LastName = reader.GetString("lastName"),
                                Extention = reader.GetString("extention"),
                                Email = reader.GetString("email"),
                                OfficeCode = reader.GetString("officeCode"),
                                JobTitle = reader.GetString("jobTitle"),
                                ReportsTo = reader["reportsTo"] as Int32?
                            };
                        }
                    }
                }
                return employee;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving employee by ID", ex);
            }
        }

        //POST employee
        public void AddEmployee(int Id,
            string FirstName,
            string LastName,
            string Extention,
            string Email,
            string OfficeCode,
            string? JobTitle,
            int? ReportsTo)
        {
            try
            {
                var employee = new Employee();
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand("INSERT INTO employees (employeeNumber, firstName, lastName, extention, email, officeCode, reportsTo, jobTitle) VALUES (@employeeNumber, @firstName, @lastName, @extention, @email, @officeCode, @reportsTo, @jobTitle)", connection))
                    {
                        command.Parameters.AddWithValue("@employeeNumber", Id);
                        command.Parameters.AddWithValue("@firstName", FirstName);
                        command.Parameters.AddWithValue("@lastName", LastName);
                        command.Parameters.AddWithValue("@extention", Extention);
                        command.Parameters.AddWithValue("@email", Email);
                        command.Parameters.AddWithValue("@officeCode", OfficeCode);
                        command.Parameters.AddWithValue("@jobTitle", JobTitle);
                        command.Parameters.AddWithValue("@reportsTo", ReportsTo);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding a employee", ex);
            }
        }

        //DELETE employee
        public void DeleteEmployee(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand($"DELETE FROM employees WHERE employeeNumber = {id}", connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting a employee", ex);
            }
        }
    }
}
