﻿using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync(); // Return IEnumerable<TransactionDto>
        Task<TransactionDto> GetTransactionByIdAsync(int transactionId); // Return a single TransactionDto
       // Task<IEnumerable<TransactionDto>> GetTransactionsByStudentIdAsync(int studentId); // Transactions by Student ID
       // Task<IEnumerable<TransactionDto>> GetTransactionsByUserIdAsync(int userId); // Transactions by User ID
        Task<int> CreateTransactionAsync(TransactionDto transactionDto); // Return the new Transaction ID
        Task<bool> UpdateTransactionAsync(TransactionDto transactionDto); // Return true if update is successful
        Task<bool> DeleteTransactionAsync(int transactionId); // Return true if delete is successful
    }
}