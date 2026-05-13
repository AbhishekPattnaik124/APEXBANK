using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApexBank.Application.DTOs;
using ApexBank.Application.Interfaces;
using ApexBank.Domain.Entities;

namespace ApexBank.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IApplicationDbContext _context;

        public AccountService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AccountDto> GetAccountByIdAsync(Guid id)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);

            if (account == null) return null!;

            return MapToDto(account);
        }

        public async Task<IEnumerable<AccountDto>> GetUserAccountsAsync(Guid userId)
        {
            return await _context.Accounts
                .Where(a => a.UserId == userId && !a.IsDeleted)
                .Select(a => MapToDto(a))
                .ToListAsync();
        }

        public async Task<AccountDto> CreateAccountAsync(Guid userId, string accountType)
        {
            var account = new Account
            {
                UserId = userId,
                AccountType = accountType,
                AccountNumber = GenerateAccountNumber(),
                Balance = 0,
                IsFrozen = false
            };

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return MapToDto(account);
        }

        public async Task<bool> UpdateAccountStatusAsync(Guid id, bool isFrozen)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null) return false;

            account.IsFrozen = isFrozen;
            account.UpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            return true;
        }

        private string GenerateAccountNumber()
        {
            var random = new Random();
            return "APEX" + random.Next(10000000, 99999999).ToString();
        }

        private static AccountDto MapToDto(Account account)
        {
            return new AccountDto
            {
                Id = account.Id,
                AccountNumber = account.AccountNumber,
                Balance = account.Balance,
                AccountType = account.AccountType,
                Currency = account.Currency,
                IsFrozen = account.IsFrozen
            };
        }
    }
}
