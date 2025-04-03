using FUST.ECommerce.Components.Account.Pages.Manage;
using FUST.ECommerce.Models;
using MySqlConnector;

namespace FUST.ECommerce.Services
{

    public class UsersDataAccess : IUsersDataAccess
    {
        private readonly string _connectionString;
        public UsersDataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            try
            {
                var users = new List<User>();
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new MySqlCommand("SELECT * FROM users", connection))
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            users.Add(new User
                            {
                                Id = reader.GetInt32("userID"),
                                FirstName = reader.IsDBNull(reader.GetOrdinal("firstName")) ? null : reader.GetString("firstName"),
                                LastName = reader.IsDBNull(reader.GetOrdinal("lastName")) ? null : reader.GetString("lastName"),
                                EMail = reader.GetString("eMail"),
                                Password = reader.GetString("password"),
                                IsAdmin = reader.GetBoolean("isAdmin")
                            });
                        }
                    }
                }
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving users", ex);
            }
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            try
            {
                var user = new User();
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new MySqlCommand("SELECT * FROM users WHERE eMail = @email", connection))
                    {
                        command.Parameters.AddWithValue("@email", email);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                user.Id = reader.GetInt32("userID");
                                user.FirstName = reader.IsDBNull(reader.GetOrdinal("firstName")) ? null : reader.GetString("firstName");
                                user.LastName = reader.IsDBNull(reader.GetOrdinal("lastName")) ? null : reader.GetString("lastName");
                                user.EMail = reader.GetString("eMail");
                                user.Password = reader.GetString("password");
                                user.IsAdmin = reader.GetBoolean("isAdmin");
                            }
                        }
                    }
                }
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving users", ex);
            }
        }
        public void ModifyUser(User user)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand("UPDATE users SET firstName = @name, lastName = @lastName email = @email, password = @password, isAdmin = @isAdmin WHERE userID = @userID", connection))
                    {
                        command.Parameters.AddWithValue("@firstName", user.FirstName);
                        command.Parameters.AddWithValue("@lastName", user.LastName);
                        command.Parameters.AddWithValue("@email", user.EMail);
                        command.Parameters.AddWithValue("@password", user.Password);
                        command.Parameters.AddWithValue("@isAdmin", user.IsAdmin);
                        command.Parameters.AddWithValue("@userID", user.Id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while modifying a user", ex);
            }
        }

        public void ResetPassword(string password, int? id = null, string? email = null)
        {
            try
            {
                if (id is null)
                {
                    using (var connection = new MySqlConnection(_connectionString))
                    {
                        connection.Open();
                        using (var command = new MySqlCommand("UPDATE users SET password = @password, WHERE eMail = @email", connection))
                        {
                            command.Parameters.AddWithValue("@email", email);
                            command.Parameters.AddWithValue("@password", password);
                            command.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    using (var connection = new MySqlConnection(_connectionString))
                    {
                        connection.Open();
                        using (var command = new MySqlCommand("UPDATE users SET password = @password, WHERE userID = @id", connection))
                        {
                            command.Parameters.AddWithValue("@id", id);
                            command.Parameters.AddWithValue("@password", password);
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while resetting a password", ex);
            }
        }

        public void AddUser(string email, string password, string? name = null, string? surname = null, bool? isadmin = null)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand("INSERT INTO users(firstName, lastName, email, password, isAdmin) VALUES(@firstName, @lastName, @email, @password, @isAdmin)", connection))
                    {
                        command.Parameters.AddWithValue("@firstName", name);
                        command.Parameters.AddWithValue("@lastName", surname);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@password", password);
                        command.Parameters.AddWithValue("@isAdmin", isadmin ?? false);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding a user", ex);
            }
        }
    }
}