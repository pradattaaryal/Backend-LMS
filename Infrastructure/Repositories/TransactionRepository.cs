using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly string _connectionString;

        public TransactionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "GetAll");

                return await connection.QueryAsync<Transaction>(
                    "usp_ManageTransactions",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }
        
        public async Task<Transaction> GetTransactionByIdAsync(int transactionId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "GetById");
                parameters.Add("@TransactionId", transactionId);

                return await connection.QueryFirstOrDefaultAsync<Transaction>(
                    "usp_ManageTransactions",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> AddTransactionAsync(Transaction transaction)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "Create");
                parameters.Add("@StudentId", transaction.StudentId);
                parameters.Add("@UserId", transaction.UserId);
                parameters.Add("@BookId", transaction.BookId);
                parameters.Add("@TransactionType", transaction.TransactionType);
                parameters.Add("@Date", transaction.Date);

                // Retrieve the new Transaction ID
                return await connection.ExecuteScalarAsync<int>(
                    "usp_ManageTransactions",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<bool> UpdateTransactionAsync(Transaction transaction)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "Update");
                parameters.Add("@TransactionId", transaction.TransactionId);
                parameters.Add("@StudentId", transaction.StudentId);
                parameters.Add("@UserId", transaction.UserId);
                parameters.Add("@BookId", transaction.BookId);
                parameters.Add("@TransactionType", transaction.TransactionType);
                parameters.Add("@Date", transaction.Date);

                var rowsAffected = await connection.ExecuteAsync(
                    "usp_ManageTransactions",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return rowsAffected > 0;
            }
        }

        public async Task<bool> DeleteTransactionAsync(int transactionId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "Delete");
                parameters.Add("@TransactionId", transactionId);

                var rowsAffected = await connection.ExecuteAsync(
                    "usp_ManageTransactions",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return rowsAffected > 0;
            }
        }

        public async Task<IEnumerable<TransactionDto>> GetTransactionsByBookNameAsync(string bookName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "ByBookName");
                parameters.Add("@BookName", bookName);  // Passing the BookName parameter

                // Call the new stored procedure for searching by BookName
                var transactions = await connection.QueryAsync<Transaction>(
                    "usp_ManageTransactions",  // Call the new procedure
                    parameters,
                    commandType: CommandType.StoredProcedure);

                // Mapping Transaction to TransactionDto
                return transactions.Select(transaction => new TransactionDto
                {
                    TransactionId = transaction.TransactionId,
                    StudentId = transaction.StudentId,
                    UserId = transaction.UserId,
                    BookId = transaction.BookId,
                    TransactionType = transaction.TransactionType,
                    Date = transaction.Date,
                    studentName = transaction.studentName,  // Ensure field matches stored procedure output
                    LibarianName = transaction.LibarianName,  // Ensure field matches stored procedure output
                    BookName = transaction.BookName          // Ensure field matches stored procedure output
                });
            }
        }




        /* public Task<IEnumerable<Transaction>> GetTransactionsByUserIdAsync(int userId)
         {
             throw new NotImplementedException();
         }*/
    }
}
