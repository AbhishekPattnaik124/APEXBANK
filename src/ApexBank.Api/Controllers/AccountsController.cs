using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApexBank.Application.Interfaces;

namespace ApexBank.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyAccounts()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty);
            var accounts = await _accountService.GetUserAccountsAsync(userId);
            return Ok(accounts);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] string type)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty);
            var account = await _accountService.CreateAccountAsync(userId, type);
            return Ok(account);
        }

        [HttpPatch("{id}/status")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] bool isFrozen)
        {
            var result = await _accountService.UpdateAccountStatusAsync(id, isFrozen);
            if (!result) return NotFound();
            return Ok();
        }
    }
}
