using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly string _connectionString;

        public AuthorRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "GetAll");

                return await connection.QueryAsync<Author>(
                    "usp_ManageAuthors",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Author> GetAuthorByIdAsync(int authorId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "GetById");
                parameters.Add("@AuthorId", authorId);

                return await connection.QueryFirstOrDefaultAsync<Author>(
                    "usp_ManageAuthors",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> AddAuthorAsync(Author author)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "Create");
                parameters.Add("@Name", author.Name);
                parameters.Add("@Bio", author.Bio);
                // Add other Author properties as needed

                // Retrieve the new Author ID
                return await connection.ExecuteScalarAsync<int>(
                    "usp_ManageAuthors",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<bool> UpdateAuthorAsync(Author author)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "Update");
                parameters.Add("@AuthorId", author.AuthorId);
                parameters.Add("@Name", author.Name);
                parameters.Add("@Bio", author.Bio);
                // Add other Author properties as needed

                var rowsAffected = await connection.ExecuteAsync(
                    "usp_ManageAuthors",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return rowsAffected > 0;
            }
        }

        public async Task<bool> DeleteAuthorAsync(int authorId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Flag", "Delete");
                    parameters.Add("@AuthorId", authorId);

                    var rowsAffected = await connection.ExecuteAsync(
                        "usp_ManageAuthors",
                        parameters,
                        commandType: CommandType.StoredProcedure);

                   bool b= rowsAffected > 0;
                    return b; // True if the deletion was successful
                }
                catch (SqlException ex)
                {
                    if (ex.Message.Contains("Author not found"))
                        return false; // Handle not found case

                    throw; // Re-throw other SQL exceptions
                }
            }
        }

    }
}
