using Domain.Entities;
using MediatR;

namespace Presentation.CQRS.QueryHandler.TransactionQuery
{
    public class GetAllTransactionsQuery : IRequest<IEnumerable<TransactionDto>>
    {
    
    }

    // Query to Get a Transaction by ID
    public class GetTransactionByIdQuery : IRequest<TransactionDto>
    {
        public int TransactionId { get; set; }
    }

    // Query to Get Transactions by Student ID
    public class GetTransactionsByStudentIdQuery : IRequest<IEnumerable<TransactionDto>>
    {
        public int StudentId { get; set; }
    }

    // Query to Get Transactions by User ID
    public class GetTransactionsByUserIdQuery : IRequest<IEnumerable<TransactionDto>>
    {
        public int UserId { get; set; }
    }
}
