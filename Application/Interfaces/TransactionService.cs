using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repository;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync()
        {
            var transactionEntities = await _repository.GetAllTransactionsAsync();
            return _mapper.Map<IEnumerable<TransactionDto>>(transactionEntities);
        }

        public async Task<TransactionDto> GetTransactionByIdAsync(int transactionId)
        {
            var transactionEntity = await _repository.GetTransactionByIdAsync(transactionId);
            return _mapper.Map<TransactionDto>(transactionEntity);
        }

     /*   public async Task<IEnumerable<TransactionDto>> GetTransactionsByStudentIdAsync(int studentId)
        {
            var transactions = await _repository.GetTransactionsByStudentIdAsync(studentId);
            return _mapper.Map<IEnumerable<TransactionDto>>(transactions);
        }

        public async Task<IEnumerable<TransactionDto>> GetTransactionsByUserIdAsync(int userId)
        {
            var transactions = await _repository.GetTransactionsByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<TransactionDto>>(transactions);
        }
     */
        public async Task<int> CreateTransactionAsync(TransactionDto transactionDto)
        {
            var transactionEntity = _mapper.Map<Transaction>(transactionDto);
            return await _repository.AddTransactionAsync(transactionEntity); // Return new Transaction ID
        }

        public async Task<bool> UpdateTransactionAsync(TransactionDto transactionDto)
        {
            var transactionEntity = _mapper.Map<Transaction>(transactionDto);
            return await _repository.UpdateTransactionAsync(transactionEntity); // Return true if update is successful
        }

        public async Task<bool> DeleteTransactionAsync(int transactionId)
        {
            return await _repository.DeleteTransactionAsync(transactionId); // Return true if delete is successful
        }
    }
}
