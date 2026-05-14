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

        private const int MaxFailedAttempts = 5;
        private const int LockoutMinutes = 15;

        public IdentityService(IApplicationDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDto> LoginAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return new AuthResponseDto { Success = false, Message = "Invalid email or password." };

            // Lockout Check
            if (user.IsLockedOut && user.LockoutEndAt > DateTime.UtcNow)
            {
                var remaining = (int)(user.LockoutEndAt!.Value - DateTime.UtcNow).TotalMinutes + 1;
                return new AuthResponseDto { Success = false, Message = $"Account locked. Try again in {remaining} minute(s)." };
            }

            // Reset lockout if expired
            if (user.IsLockedOut && user.LockoutEndAt <= DateTime.UtcNow)
            {
                user.IsLockedOut = false;
                user.FailedLoginAttempts = 0;
            }

            // BCrypt Password Verify
            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

            if (!isValid)
            {
                user.FailedLoginAttempts++;
                if (user.FailedLoginAttempts >= MaxFailedAttempts)
                {
                    user.IsLockedOut = true;
                    user.LockoutEndAt = DateTime.UtcNow.AddMinutes(LockoutMinutes);
                    await _context.SaveChangesAsync();
                    return new AuthResponseDto { Success = false, Message = $"Too many failed attempts. Account locked for {LockoutMinutes} minutes." };
                }
                await _context.SaveChangesAsync();
                return new AuthResponseDto { Success = false, Message = $"Invalid email or password. {MaxFailedAttempts - user.FailedLoginAttempts} attempt(s) remaining." };
            }

            if (!user.IsActive)
                return new AuthResponseDto { Success = false, Message = "Account is deactivated. Contact support." };

            // Successful login — reset counters
            user.FailedLoginAttempts = 0;
            user.IsLockedOut = false;
            user.LastLoginAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            var token = _jwtService.GenerateToken(user);

            return new AuthResponseDto
            {
                Success = true,
                Token = token,
                Email = user.Email,
                Role = user.Role,
                Message = $"Welcome back, {user.FirstName}!"
            };
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            var existingUser = await _context.Users.AnyAsync(u => u.Email == request.Email);
            if (existingUser)
                return new AuthResponseDto { Success = false, Message = "Email is already registered." };

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                PhoneNumber = request.PhoneNumber,
                Role = "Customer",
                KycStatus = "Pending",
                IsActive = true
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Auto-create savings account
            var accountNumber = "APEX" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString()[^10..];
            var account = new Account
            {
                UserId = user.Id,
                AccountNumber = accountNumber,
                Balance = 0,
                AccountType = "Savings",
                InterestRate = 4.0m,
                Status = "Active"
            };
            _context.Accounts.Add(account);

            // Welcome notification
            var notification = new Notification
            {
                UserId = user.Id,
                Title = "Welcome to ApexBank! 🎉",
                Message = $"Hi {user.FirstName}! Your savings account {accountNumber} is ready. Complete KYC to unlock all features.",
                Type = "Success",
                ActionUrl = "/dashboard/profile"
            };
            _context.Notifications.Add(notification);

            await _context.SaveChangesAsync();

            var token = _jwtService.GenerateToken(user);

            return new AuthResponseDto
            {
                Success = true,
                Token = token,
                Email = user.Email,
                Role = user.Role,
                Message = "Registration successful! Welcome to ApexBank."
            };
        }
    }
}
