using Domain.Entities;
using MediatR;

namespace Presentation.CQRS.CommandHandler.TransactionCommand
{
    public class CreateTransactionCommand : IRequest<int>
    {
        public TransactionDto TransactionDto { get; set; }
    }

    // Command to Update a Transaction
    public class UpdateTransactionCommand : IRequest<bool>
    {
        public TransactionDto TransactionDto { get; set; }
    }

    // Command to Delete a Transaction
    public class DeleteTransactionCommand : IRequest<bool>
    {
        public int TransactionId { get; set; }
    }
}
