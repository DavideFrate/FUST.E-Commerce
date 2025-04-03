using MySqlConnector;
using nonMudNonBlazor.Models;

namespace nonMudNonBlazor.Services
{
    public class CustomersDataAccess : ICustomersDataAccess
    {
        private readonly string _connectionString;

        public CustomersDataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        //GET Customers
        public async Task<List<Customer>> GetCustomersAsync()
        {
            try
            {
                var customers = new List<Customer>();
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (MySqlCommand command = new("SELECT * FROM customers", connection))
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            customers.Add(new Customer
                            {
                                Id = reader.GetInt32("customerNumber"),
                                CustomerName = reader.GetString("customerName"),
                                FirstName = reader.GetString("contactFirstName"),
                                LastName = reader.GetString("contactLastName"),
                                Phone = reader.GetString("phone"),
                                Address = reader.GetString("addressLine1"),
                                City = reader.GetString("city"),
                                State = reader["state"] as string,
                                PostalCode = reader["postalCode"] as string,
                                Country = reader.GetString("country"),
                                SalesRepId = reader["salesRepEmployeeNumber"] as Int32? ?? 0,
                                CreditLimit = reader["creditLimit"] as decimal? ?? 0
                            });
                        }
                    }
                }
                return customers;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving customers", ex);
            }
        }

        //GET Customer by Id
        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            try
            {
                var customer = new Customer();
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand($"SELECT * FROM customers WHERE customerNumber = {id}", connection))
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            customer = new Customer
                            {
                                Id = reader.GetInt32("customerNumber"),
                                CustomerName = reader.GetString("customerName"),
                                FirstName = reader.GetString("contactFirstName"),
                                LastName = reader.GetString("contactLastName"),
                                Phone = reader.GetString("phone"),
                                Address = reader.GetString("addressLine1"),
                                City = reader.GetString("city"),
                                State = reader["state"] as string,
                                PostalCode = reader["postalCode"] as string,
                                Country = reader.GetString("country"),
                                CreditLimit = reader["creditLimit"] as decimal? ?? 0,
                                SalesRepId = reader["salesRepEmployeeNumber"] as Int32? ?? 0
                            };
                        }
                    }
                    
                }
                return customer;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving customer by ID", ex);
            }
        }

        //POST Customer
        public void AddCustomer(int id, string CustomerName, string FirstName, string LastName,
            string Phone, string Address, string City, string State, string? PostalCode,
            string Country, decimal? CreditLimit, int? SalesRepId)
        {
            try
            {
                var Customer = new Customer();
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand("INSERT INTO customers (customerNumber, customerName contactFirstName, contactLastName, phone, addressLine1, city, state, postalCode, country, creditLimit, salesRepEmployeeNumber) VALUES (@customerNumber, @customerName, @contactFirstName, @contactLastName, @phone, @addressLine1, @city, @state, @postalCode, @country, @creditLimit, @salesRepEmployeeNumber)", connection))
                    {
                        command.Parameters.AddWithValue("@customerNumber", id);
                        command.Parameters.AddWithValue("@customerName", CustomerName);
                        command.Parameters.AddWithValue("@contactFirstName", FirstName);
                        command.Parameters.AddWithValue("@contactLastName", LastName);
                        command.Parameters.AddWithValue("@phone", Phone);
                        command.Parameters.AddWithValue("@addressLine1", Address);
                        command.Parameters.AddWithValue("@city", City);
                        command.Parameters.AddWithValue("@state", State);
                        command.Parameters.AddWithValue("@postalCode", PostalCode);
                        command.Parameters.AddWithValue("@country", Country);
                        command.Parameters.AddWithValue("@creditLimit", CreditLimit);
                        command.Parameters.AddWithValue("@salesRepEmployeeNumber", SalesRepId);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding a customer", ex);
            }
        }

        //DELETE Customer
        public void DeleteCustomer(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand($"DELETE FROM customers WHERE customerNumber = {id}", connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting a customer", ex);
            }
        }
    }
}