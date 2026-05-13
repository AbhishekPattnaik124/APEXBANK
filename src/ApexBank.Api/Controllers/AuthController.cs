using Microsoft.AspNetCore.Mvc;
using ApexBank.Application.DTOs;
using ApexBank.Application.Interfaces;

namespace ApexBank.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            var result = await _identityService.RegisterAsync(request);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var result = await _identityService.LoginAsync(loginRequest.Email, loginRequest.Password);
            if (!result.Success) return Unauthorized(result);
            return Ok(result);
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
