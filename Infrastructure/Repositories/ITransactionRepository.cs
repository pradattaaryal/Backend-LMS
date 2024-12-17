using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITransactionRepository
{
 
    Task<int> AddTransactionAsync(Transaction transaction);

 
    Task<bool> UpdateTransactionAsync(Transaction transaction);
 
    Task<bool> DeleteTransactionAsync(int transactionId);

 
    Task<Transaction> GetTransactionByIdAsync(int transactionId);
 
    Task<IEnumerable<Transaction>> GetAllTransactionsAsync();

    Task<IEnumerable<TransactionDto>> GetTransactionsByBookNameAsync(string bookName);


    
}
