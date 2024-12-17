using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Presentation.CQRS.QueryHandler.TransactionQuery;

public class TransactionQueryHandler :
    IRequestHandler<GetAllTransactionsQuery, IEnumerable<TransactionDto>>,
    IRequestHandler<GetTransactionByIdQuery, TransactionDto>,
 
    IRequestHandler<GetTransactionsByBookNameQuery, IEnumerable<TransactionDto>> // New handler
{
    private readonly ITransactionService _transactionService;

    public TransactionQueryHandler(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    public async Task<IEnumerable<TransactionDto>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
    {
        return await _transactionService.GetAllTransactionsAsync();
    }

    public async Task<TransactionDto> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        return await _transactionService.GetTransactionByIdAsync(request.TransactionId);
    }

    

    public async Task<IEnumerable<TransactionDto>> Handle(GetTransactionsByBookNameQuery request, CancellationToken cancellationToken)
    {
        return await _transactionService.GetTransactionsByBookNameAsync(request.BookName); // New call
    }
}
