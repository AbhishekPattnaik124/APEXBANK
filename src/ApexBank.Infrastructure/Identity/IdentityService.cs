using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApexBank.Application.DTOs;
using ApexBank.Application.Interfaces;
using ApexBank.Domain.Entities;

namespace ApexBank.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IApplicationDbContext _context;
        private readonly IJwtService _jwtService;

        public IdentityService(IApplicationDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDto> LoginAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null || user.PasswordHash != password) // In real app, use BCrypt.Verify
            {
                return new AuthResponseDto { Success = false, Message = "Invalid credentials" };
            }

            var token = _jwtService.GenerateToken(user);

            return new AuthResponseDto
            {
                Success = true,
                Token = token,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            var existingUser = await _context.Users.AnyAsync(u => u.Email == request.Email);
            if (existingUser)
            {
                return new AuthResponseDto { Success = false, Message = "Email already registered" };
            }

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = request.Password, // In real app, use BCrypt.Hash
                PhoneNumber = request.PhoneNumber,
                Role = "Customer"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = _jwtService.GenerateToken(user);

            return new AuthResponseDto
            {
                Success = true,
                Token = token,
                Email = user.Email,
                Role = user.Role
            };
        }
    }
}
