using Application.Interfaces;
using Presentation.CQRS.CommandHandler.TransactionCommand;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.CommandHandlers
{
    public class TransactionCommandHandler :
        IRequestHandler<CreateTransactionCommand, int>,    // Return new Transaction ID
        IRequestHandler<UpdateTransactionCommand, bool>,   // Return true if update is successful
        IRequestHandler<DeleteTransactionCommand, bool>    // Return true if delete is successful
    {
        private readonly ITransactionService _transactionService;

        public TransactionCommandHandler(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // Handle CreateTransactionCommand - returns new Transaction ID
        public async Task<int> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _transactionService.CreateTransactionAsync(request.TransactionDto);
            }
            catch
            {
                return 0; // Return 0 to indicate failure
            }
        }

        // Handle UpdateTransactionCommand - returns true if update is successful
        public async Task<bool> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _transactionService.UpdateTransactionAsync(request.TransactionDto);
            }
            catch
            {
                return false; // Return false if update fails
            }
        }

        // Handle DeleteTransactionCommand - returns true if delete is successful
        public async Task<bool> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            return await _transactionService.DeleteTransactionAsync(request.TransactionId);
        }
    }
}
