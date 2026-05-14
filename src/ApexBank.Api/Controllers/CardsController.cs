using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApexBank.Application.Interfaces;

namespace ApexBank.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;
        public CardsController(ICardService cardService) => _cardService = cardService;

        /// <summary>Get all cards for a given account.</summary>
        [HttpGet("account/{accountId:guid}")]
        public async Task<IActionResult> GetByAccount(Guid accountId)
        {
            var cards = await _cardService.GetAccountCardsAsync(accountId);
            return Ok(cards);
        }

        /// <summary>Issue a new card for an account.</summary>
        [HttpPost("issue")]
        public async Task<IActionResult> Issue([FromBody] IssueCardRequest req)
        {
            var card = await _cardService.IssueCardAsync(req.AccountId, req.CardType, req.CardNetwork);
            return Ok(card);
        }

        /// <summary>Freeze a card temporarily.</summary>
        [HttpPost("{id:guid}/freeze")]
        public async Task<IActionResult> Freeze(Guid id)
        {
            var result = await _cardService.FreezeCardAsync(id);
            return result ? Ok(new { message = "Card frozen successfully." }) : NotFound();
        }

        /// <summary>Unfreeze a previously frozen card.</summary>
        [HttpPost("{id:guid}/unfreeze")]
        public async Task<IActionResult> Unfreeze(Guid id)
        {
            var result = await _cardService.UnfreezeCardAsync(id);
            return result ? Ok(new { message = "Card unfrozen successfully." }) : NotFound();
        }

        /// <summary>Update daily and monthly spending limits.</summary>
        [HttpPut("{id:guid}/limits")]
        public async Task<IActionResult> UpdateLimits(Guid id, [FromBody] UpdateLimitsRequest req)
        {
            var result = await _cardService.UpdateLimitsAsync(id, req.DailyLimit, req.MonthlyLimit);
            return result ? Ok(new { message = "Limits updated." }) : NotFound();
        }

        /// <summary>Permanently block and deactivate a card.</summary>
        [HttpPost("{id:guid}/block")]
        public async Task<IActionResult> Block(Guid id)
        {
            var result = await _cardService.BlockCardAsync(id);
            return result ? Ok(new { message = "Card permanently blocked." }) : NotFound();
        }
    }

    public class IssueCardRequest
    {
        public Guid AccountId { get; set; }
        public string CardType { get; set; } = "Debit";
        public string CardNetwork { get; set; } = "Visa";
    }

    public class UpdateLimitsRequest
    {
        public decimal DailyLimit { get; set; }
        public decimal MonthlyLimit { get; set; }
    }
}
