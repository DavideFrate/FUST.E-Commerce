using FUST.ECommerce.Models;
using MySqlConnector;

namespace FUST.ECommerce.Services
{
    public class UsersDataAccess : IUsersDataAccess
    {
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            try
            {
                var users = new List<User>();
                using (var connection = new MySqlConnection("DefaultConnection"))
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
                                FirstName = reader.GetString("firstName"),
                                LastName = reader.GetString("LastName"),
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
        public void ModifyUser(User user)
        {
            try
            {
                using (var connection = new MySqlConnection("DefaultConnection"))
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

        public void ResetPassword(string password)
        {
            try
            {
                using (var connection = new MySqlConnection("DefaultConnection"))
                {
                    connection.Open();
                    using (var command = new MySqlCommand("UPDATE users SET password = @password", connection))
                    {
                        command.Parameters.AddWithValue("@password", password);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while resetting a password", ex);
            }
        }
    }
}