using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;
using Infrastructure.Repositories;

namespace Infrastructure.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly string _connectionString;

        public AuthRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> CreateUserrepo(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "Create");
                string hashedPassword = Hash(user.Password);
                parameters.Add("@Username", user.Username);
                parameters.Add("@Password", hashedPassword);
                parameters.Add("@Email", user.Email);
                parameters.Add("@Role", user.Role);

                var result = await connection.ExecuteScalarAsync<int>(
                    "usp_ManageUsers",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return result > 0;
            }
        }

        public async Task<User> GetUserByNamerepo(string username)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "GetByName");
                parameters.Add("@Username", username);

                return await connection.QueryFirstOrDefaultAsync<User>(
                    "usp_ManageUsers",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<User> GetUserByIdrepo(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "GetById");
                parameters.Add("@UserId", userId);

                return await connection.QueryFirstOrDefaultAsync<User>(
                    "usp_ManageUsers",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersrepo()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "GetAll");

                return await connection.QueryAsync<User>(
                    "usp_ManageUsers",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<bool> UpdateUserrepo(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "Update");
                parameters.Add("@UserId", user.UserId);
                parameters.Add("@Username", user.Username);
                string hashedPassword = Hash(user.Password);
                parameters.Add("@Password", hashedPassword);
                parameters.Add("@Email", user.Email);
                parameters.Add("@Role", user.Role);

                var rowsAffected = await connection.ExecuteAsync(
                    "usp_ManageUsers",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return rowsAffected > 0;
            }
        }

        public async Task<bool> DeleteUserrepo(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Flag", "Delete");
                    parameters.Add("@UserId", userId);

                    var rowsAffected = await connection.ExecuteAsync(
                        "usp_ManageUsers",
                        parameters,
                        commandType: CommandType.StoredProcedure);

                    return rowsAffected > 0;
                }
                catch (SqlException ex)
                {
                    if (ex.Message.Contains("User not found"))
                        return false;

                    throw;
                }
            }
        }

        private static string Hash(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
