using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Application.Interfaces;
using Dapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data
{
    public class BookRepository : IBookRepository
    {
        private readonly string _connectionString;

        public BookRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "GetAll");

                return await connection.QueryAsync<Book>(
                    "usp_ManageBooks",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "GetById");
                parameters.Add("@BookId", bookId);

                return await connection.QueryFirstOrDefaultAsync<Book>(
                    "usp_ManageBooks",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> AddBookAsync(Book book)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "Create");
                parameters.Add("@Title", book.Title);
                parameters.Add("@AuthorId", book.AuthorId);
                parameters.Add("@Genre", book.Genre);
                parameters.Add("@ISBN", book.ISBN);
                parameters.Add("@Quantity", book.Quantity);

                // Retrieve the new Book ID
                return await connection.ExecuteScalarAsync<int>(
                    "usp_ManageBooks",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<bool> UpdateBookAsync(Book book)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "Update");
                parameters.Add("@BookId", book.BookId);
                parameters.Add("@Title", book.Title);
                parameters.Add("@AuthorId", book.AuthorId);
                parameters.Add("@Genre", book.Genre);
                parameters.Add("@ISBN", book.ISBN);
                parameters.Add("@Quantity", book.Quantity);

                var rowsAffected = await connection.ExecuteAsync(
                    "usp_ManageBooks",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return rowsAffected > 0; // True if update was successful
            }
        }

        public async Task<bool> DeleteBookAsync(int bookId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Flag", "Delete");
                    parameters.Add("@BookId", bookId);

                    var rowsAffected = await connection.ExecuteAsync(
                        "usp_ManageBooks",
                        parameters,
                        commandType: CommandType.StoredProcedure);

                    return rowsAffected > 0; // True if deletion was successful
                }
                catch (SqlException ex)
                {
                    if (ex.Message.Contains("Book not found"))
                        return false; // Handle not found case

                    throw; // Re-throw other SQL exceptions
                }
            }
        }
    }
}
