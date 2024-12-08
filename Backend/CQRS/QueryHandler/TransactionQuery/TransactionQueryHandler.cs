using Application.Interfaces;
using Presentation.CQRS.QueryHandler.TransactionQuery;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.CQRS.QueryHandlers
{
    public class TransactionQueryHandler :
        IRequestHandler<GetAllTransactionsQuery, IEnumerable<TransactionDto>>,  // Return all transactions
        IRequestHandler<GetTransactionByIdQuery, TransactionDto>           // Return a single transaction by ID
       // IRequestHandler<GetTransactionsByStudentIdQuery, IEnumerable<TransactionDto>>, // Return transactions by Student ID
      //  IRequestHandler<GetTransactionsByUserIdQuery, IEnumerable<TransactionDto>>     // Return transactions by User ID
    {
        private readonly ITransactionService _transactionService;

        public TransactionQueryHandler(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // Handle GetAllTransactionsQuery - returns all transactions
        public async Task<IEnumerable<TransactionDto>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            return await _transactionService.GetAllTransactionsAsync();
        }

        // Handle GetTransactionByIdQuery - returns a single transaction by ID
        public async Task<TransactionDto> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            return await _transactionService.GetTransactionByIdAsync(request.TransactionId);
        }

        // Handle GetTransactionsByStudentIdQuery - returns transactions by Student ID
       /* public async Task<IEnumerable<TransactionDto>> Handle(GetTransactionsByStudentIdQuery request, CancellationToken cancellationToken)
        {
            return await _transactionService.GetTransactionsByStudentIdAsync(request.StudentId);
        }

        // Handle GetTransactionsByUserIdQuery - returns transactions by User ID
     public async Task<IEnumerable<TransactionDto>> Handle(GetTransactionsByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _transactionService.GetTransactionsByUserIdAsync(request.UserId);
        }*/
    }
}
