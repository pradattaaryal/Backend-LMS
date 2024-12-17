using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Commands;
using Application.Queries;
using System.Threading.Tasks;
using Presentation.CQRS.CommandHandler.TransactionCommand;
using Presentation.CQRS.QueryHandler.TransactionQuery;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransactions()
        {
            var transactions = await _mediator.Send(new GetAllTransactionsQuery());
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            var transaction = await _mediator.Send(new GetTransactionByIdQuery { TransactionId = id });
            if (transaction == null)
                return NotFound(new { message = "Transaction not found" });

            return Ok(transaction);
        }
        [HttpGet("student/{id}")]
        public async Task<IActionResult> GetTransactionsByStudentId(int id)
        {
            var transactionn = await _mediator.Send(new GetTransactionsByStudentIdQuery { StudentId = id });
            if (transactionn == null)
                return NotFound(new { message = "Transaction not found" });

            return Ok(transactionn);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction(TransactionDto transactionDto)
        {
            if (transactionDto == null)
                return BadRequest(new { message = "Invalid transaction data" });

            var newTransactionId = await _mediator.Send(new CreateTransactionCommand { TransactionDto = transactionDto });

            if (newTransactionId <= 0)
                return BadRequest(new { message = "Failed to create transaction" });

            return CreatedAtAction(nameof(GetTransactionById), new { id = newTransactionId }, transactionDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(int id, [FromBody] TransactionDto transactionDto)
        {
            if (id != transactionDto.TransactionId)
                return BadRequest(new { message = "Transaction ID mismatch" });

            var result = await _mediator.Send(new UpdateTransactionCommand { TransactionDto = transactionDto });

            if (!result)
                return NotFound(new { message = "Transaction not found or update failed" });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var result = await _mediator.Send(new DeleteTransactionCommand { TransactionId = id });

            if (!result)
                return NotFound(new { message = "Transaction not found or could not be deleted" });

            return NoContent();
        }
        [HttpGet("book")]
        public async Task<IActionResult> GetTransactionsByBookName([FromQuery] string bookName)
        {
            if (string.IsNullOrEmpty(bookName))
                return BadRequest(new { message = "Book name cannot be empty" });

            var transactions = await _mediator.Send(new GetTransactionsByBookNameQuery { BookName = bookName });

            if (transactions == null || !transactions.Any())
                return NotFound(new { message = "No transactions found for the specified book name" });

            return Ok(transactions);
        }

    }
} 
