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
    public class TransactionService : ITransactionService
    {
        private readonly IApplicationDbContext _context;

        public TransactionService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> TransferAsync(Guid sourceAccountId, Guid destinationAccountId, decimal amount, string description)
        {
            if (amount <= 0) return false;

            var source = await _context.Accounts.FindAsync(sourceAccountId);
            var dest = await _context.Accounts.FindAsync(destinationAccountId);

            if (source == null || dest == null) return false;
            if (source.IsFrozen || dest.IsFrozen) return false;
            if (source.Balance < amount) return false;

            // Atomic Transaction
            source.Balance -= amount;
            dest.Balance += amount;

            var transaction = new Transaction
            {
                SourceAccountId = sourceAccountId,
                DestinationAccountId = destinationAccountId,
                Amount = amount,
                TransactionType = "Transfer",
                Description = description,
                ReferenceNumber = "TRX" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper()
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DepositAsync(Guid accountId, decimal amount)
        {
            if (amount <= 0) return false;

            var account = await _context.Accounts.FindAsync(accountId);
            if (account == null || account.IsFrozen) return false;

            account.Balance += amount;

            var transaction = new Transaction
            {
                SourceAccountId = accountId,
                Amount = amount,
                TransactionType = "Deposit",
                Description = "Cash Deposit",
                ReferenceNumber = "DEP" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper()
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> WithdrawAsync(Guid accountId, decimal amount)
        {
            if (amount <= 0) return false;

            var account = await _context.Accounts.FindAsync(accountId);
            if (account == null || account.IsFrozen || account.Balance < amount) return false;

            account.Balance -= amount;

            var transaction = new Transaction
            {
                SourceAccountId = accountId,
                Amount = amount,
                TransactionType = "Withdrawal",
                Description = "Cash Withdrawal",
                ReferenceNumber = "WTH" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper()
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<TransactionDto>> GetAccountTransactionsAsync(Guid accountId)
        {
            return await _context.Transactions
                .Where(t => t.SourceAccountId == accountId || t.DestinationAccountId == accountId)
                .OrderByDescending(t => t.CreatedAt)
                .Select(t => new TransactionDto
                {
                    Id = t.Id,
                    SourceAccountId = t.SourceAccountId,
                    DestinationAccountId = t.DestinationAccountId,
                    Amount = t.Amount,
                    TransactionType = t.TransactionType,
                    Status = t.Status,
                    Description = t.Description,
                    CreatedAt = t.CreatedAt,
                    ReferenceNumber = t.ReferenceNumber
                })
                .ToListAsync();
        }
    }
}
