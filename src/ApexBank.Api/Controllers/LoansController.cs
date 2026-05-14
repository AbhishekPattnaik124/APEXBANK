using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApexBank.Application.DTOs;
using ApexBank.Application.Interfaces;

namespace ApexBank.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;
        public LoansController(ILoanService loanService) => _loanService = loanService;

        private Guid GetUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        /// <summary>Apply for a new loan.</summary>
        [HttpPost("apply")]
        public async Task<IActionResult> Apply([FromBody] LoanApplicationDto dto)
        {
            var result = await _loanService.ApplyAsync(GetUserId(), dto);
            return Ok(result);
        }

        /// <summary>Get current user's loans.</summary>
        [HttpGet("my")]
        public async Task<IActionResult> MyLoans()
        {
            var loans = await _loanService.GetUserLoansAsync(GetUserId());
            return Ok(loans);
        }

        /// <summary>Get a specific loan by ID.</summary>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var loan = await _loanService.GetByIdAsync(id);
            return loan == null ? NotFound() : Ok(loan);
        }

        /// <summary>Calculate EMI without applying.</summary>
        [HttpGet("calculate-emi")]
        [AllowAnonymous]
        public async Task<IActionResult> CalculateEmi(
            [FromQuery] decimal principal,
            [FromQuery] decimal rate = 10.5m,
            [FromQuery] int months = 12)
        {
            var result = await _loanService.CalculateEmiAsync(principal, rate, months);
            return Ok(result);
        }

        /// <summary>Admin: Get all loans.</summary>
        [HttpGet("all")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> GetAll()
        {
            var loans = await _loanService.GetAllLoansAsync();
            return Ok(loans);
        }

        /// <summary>Admin: Approve a pending loan.</summary>
        [HttpPost("{id:guid}/approve")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Approve(Guid id)
        {
            var result = await _loanService.ApproveAsync(id, User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            return result ? Ok(new { message = "Loan approved successfully." }) : BadRequest(new { message = "Cannot approve this loan." });
        }

        /// <summary>Admin: Reject a pending loan.</summary>
        [HttpPost("{id:guid}/reject")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Reject(Guid id, [FromBody] RejectLoanRequest req)
        {
            var result = await _loanService.RejectAsync(id, req.Reason);
            return result ? Ok(new { message = "Loan rejected." }) : BadRequest(new { message = "Cannot reject this loan." });
        }
    }

    public class RejectLoanRequest
    {
        public string Reason { get; set; } = string.Empty;
    }
}
