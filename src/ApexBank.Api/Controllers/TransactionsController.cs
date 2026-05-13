using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApexBank.Application.Interfaces;

namespace ApexBank.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferRequest request)
        {
            var result = await _transactionService.TransferAsync(
                request.SourceAccountId, 
                request.DestinationAccountId, 
                request.Amount, 
                request.Description);
            
            if (!result) return BadRequest("Transfer failed. Please check balance and account status.");
            return Ok(new { Message = "Transfer successful" });
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetHistory(Guid accountId)
        {
            var transactions = await _transactionService.GetAccountTransactionsAsync(accountId);
            return Ok(transactions);
        }
    }

    public class TransferRequest
    {
        public Guid SourceAccountId { get; set; }
        public Guid DestinationAccountId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
