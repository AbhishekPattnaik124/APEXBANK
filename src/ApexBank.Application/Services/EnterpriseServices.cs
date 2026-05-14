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
    // ── Loan Service ───────────────────────────────────────────────────────
    public class LoanService : ILoanService
    {
        private readonly IApplicationDbContext _context;
        public LoanService(IApplicationDbContext context) => _context = context;

        public async Task<LoanResponseDto> ApplyAsync(Guid userId, LoanApplicationDto dto)
        {
            var emi = CalculateEmi(dto.Principal, dto.InterestRate, dto.TermMonths);
            var total = emi * dto.TermMonths;

            var loan = new Loan
            {
                UserId = userId,
                LoanType = dto.LoanType,
                LoanNumber = "LN" + Guid.NewGuid().ToString()[..8].ToUpper(),
                Principal = dto.Principal,
                InterestRate = dto.InterestRate,
                TermMonths = dto.TermMonths,
                MonthlyEmi = emi,
                TotalPayable = total,
                OutstandingBalance = dto.Principal,
                Purpose = dto.Purpose,
                Status = "Pending"
            };

            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();
            return MapLoan(loan);
        }

        public async Task<LoanResponseDto?> GetByIdAsync(Guid loanId)
        {
            var loan = await _context.Loans.FindAsync(loanId);
            return loan == null ? null : MapLoan(loan);
        }

        public async Task<IEnumerable<LoanResponseDto>> GetUserLoansAsync(Guid userId)
        {
            return await _context.Loans
                .Where(l => l.UserId == userId)
                .OrderByDescending(l => l.CreatedAt)
                .Select(l => MapLoan(l))
                .ToListAsync();
        }

        public async Task<IEnumerable<LoanResponseDto>> GetAllLoansAsync()
        {
            return await _context.Loans
                .OrderByDescending(l => l.CreatedAt)
                .Select(l => MapLoan(l))
                .ToListAsync();
        }

        public async Task<bool> ApproveAsync(Guid loanId, string approvedByUserId)
        {
            var loan = await _context.Loans.FindAsync(loanId);
            if (loan == null || loan.Status != "Pending") return false;

            loan.Status = "Active";
            loan.ApprovedAt = DateTime.UtcNow;
            loan.DisbursedAt = DateTime.UtcNow;
            loan.StartDate = DateTime.UtcNow;
            loan.EndDate = DateTime.UtcNow.AddMonths(loan.TermMonths);
            loan.ApprovedByUserId = approvedByUserId;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RejectAsync(Guid loanId, string reason)
        {
            var loan = await _context.Loans.FindAsync(loanId);
            if (loan == null || loan.Status != "Pending") return false;

            loan.Status = "Rejected";
            loan.RejectionReason = reason;
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<EmiCalculationDto> CalculateEmiAsync(decimal principal, decimal annualRate, int termMonths)
        {
            var emi = CalculateEmi(principal, annualRate, termMonths);
            var total = emi * termMonths;
            return Task.FromResult(new EmiCalculationDto
            {
                Principal = principal,
                AnnualRate = annualRate,
                TermMonths = termMonths,
                MonthlyEmi = Math.Round(emi, 2),
                TotalPayable = Math.Round(total, 2),
                TotalInterest = Math.Round(total - principal, 2)
            });
        }

        private static decimal CalculateEmi(decimal principal, decimal annualRate, int termMonths)
        {
            if (annualRate == 0) return Math.Round(principal / termMonths, 2);
            var monthlyRate = (double)(annualRate / 100 / 12);
            var n = termMonths;
            var p = (double)principal;
            var emi = p * monthlyRate * Math.Pow(1 + monthlyRate, n) / (Math.Pow(1 + monthlyRate, n) - 1);
            return Math.Round((decimal)emi, 2);
        }

        private static LoanResponseDto MapLoan(Loan l) => new()
        {
            Id = l.Id, LoanNumber = l.LoanNumber, LoanType = l.LoanType,
            Principal = l.Principal, InterestRate = l.InterestRate, TermMonths = l.TermMonths,
            MonthlyEmi = l.MonthlyEmi, TotalPayable = l.TotalPayable,
            OutstandingBalance = l.OutstandingBalance, AmountPaid = l.AmountPaid,
            Status = l.Status, Purpose = l.Purpose, ApprovedAt = l.ApprovedAt,
            StartDate = l.StartDate, EndDate = l.EndDate, CreatedAt = l.CreatedAt
        };
    }

    // ── Card Service ───────────────────────────────────────────────────────
    public class CardService : ICardService
    {
        private readonly IApplicationDbContext _context;
        public CardService(IApplicationDbContext context) => _context = context;

        public async Task<CardResponseDto> IssueCardAsync(Guid accountId, string cardType, string cardNetwork)
        {
            var account = await _context.Accounts.Include(a => a.User).FirstOrDefaultAsync(a => a.Id == accountId);
            if (account == null) throw new Exception("Account not found.");

            var last4 = new Random().Next(1000, 9999).ToString();
            var card = new Card
            {
                AccountId = accountId,
                CardType = cardType,
                CardNetwork = cardNetwork,
                MaskedNumber = $"**** **** **** {last4}",
                CardHolderName = $"{account.User.FirstName} {account.User.LastName}".ToUpper(),
                ExpiryMonth = DateTime.UtcNow.AddYears(3).Month.ToString("D2"),
                ExpiryYear = DateTime.UtcNow.AddYears(3).Year.ToString(),
                CardTokenId = Guid.NewGuid().ToString(),
                IsActive = true,
                DailyLimit = cardType == "Credit" ? 100000 : 50000,
                MonthlyLimit = cardType == "Credit" ? 500000 : 200000,
                ActivatedAt = DateTime.UtcNow
            };

            _context.Cards.Add(card);
            await _context.SaveChangesAsync();
            return MapCard(card);
        }

        public async Task<IEnumerable<CardResponseDto>> GetAccountCardsAsync(Guid accountId)
        {
            return await _context.Cards
                .Where(c => c.AccountId == accountId)
                .Select(c => MapCard(c))
                .ToListAsync();
        }

        public async Task<bool> FreezeCardAsync(Guid cardId)
        {
            var card = await _context.Cards.FindAsync(cardId);
            if (card == null) return false;
            card.IsFrozen = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UnfreezeCardAsync(Guid cardId)
        {
            var card = await _context.Cards.FindAsync(cardId);
            if (card == null) return false;
            card.IsFrozen = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateLimitsAsync(Guid cardId, decimal dailyLimit, decimal monthlyLimit)
        {
            var card = await _context.Cards.FindAsync(cardId);
            if (card == null) return false;
            card.DailyLimit = dailyLimit;
            card.MonthlyLimit = monthlyLimit;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> BlockCardAsync(Guid cardId)
        {
            var card = await _context.Cards.FindAsync(cardId);
            if (card == null) return false;
            card.IsActive = false;
            card.IsFrozen = true;
            card.BlockedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        private static CardResponseDto MapCard(Card c) => new()
        {
            Id = c.Id, CardType = c.CardType, CardNetwork = c.CardNetwork,
            MaskedNumber = c.MaskedNumber, CardHolderName = c.CardHolderName,
            ExpiryMonth = c.ExpiryMonth, ExpiryYear = c.ExpiryYear,
            IsActive = c.IsActive, IsFrozen = c.IsFrozen,
            DailyLimit = c.DailyLimit, MonthlyLimit = c.MonthlyLimit,
            UsedToday = c.UsedToday, LastUsedAt = c.LastUsedAt,
            IsVirtual = c.IsVirtual, ActivatedAt = c.ActivatedAt
        };
    }

    // ── Dashboard Service ──────────────────────────────────────────────────
    public class DashboardService : IDashboardService
    {
        private readonly IApplicationDbContext _context;
        public DashboardService(IApplicationDbContext context) => _context = context;

        public async Task<CustomerDashboardDto> GetCustomerDashboardAsync(Guid userId)
        {
            var user = await _context.Users.Include(u => u.Accounts).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) throw new Exception("User not found.");

            var accountIds = user.Accounts.Select(a => a.Id).ToList();
            var loans = await _context.Loans.Where(l => l.UserId == userId && l.Status == "Active").ToListAsync();
            var cards = await _context.Cards.Where(c => accountIds.Contains(c.AccountId) && c.IsActive).CountAsync();
            var unread = await _context.Notifications.CountAsync(n => n.UserId == userId && !n.IsRead);

            var recentTx = await _context.Transactions
                .Where(t => accountIds.Contains(t.SourceAccountId))
                .OrderByDescending(t => t.CreatedAt)
                .Take(5)
                .Select(t => new TransactionDto
                {
                    Id = t.Id, Amount = t.Amount, TransactionType = t.TransactionType,
                    Status = t.Status, Description = t.Description,
                    CreatedAt = t.CreatedAt, ReferenceNumber = t.ReferenceNumber,
                    SourceAccountId = t.SourceAccountId, DestinationAccountId = t.DestinationAccountId
                })
                .ToListAsync();

            return new CustomerDashboardDto
            {
                FullName = $"{user.FirstName} {user.LastName}",
                KycStatus = user.KycStatus,
                TotalBalance = user.Accounts.Sum(a => a.Balance),
                AccountCount = user.Accounts.Count,
                ActiveLoans = loans.Count,
                LoanOutstanding = loans.Sum(l => l.OutstandingBalance),
                ActiveCards = cards,
                UnreadNotifications = unread,
                RecentTransactions = recentTx,
                Accounts = user.Accounts.Select(a => new AccountSummaryDto
                {
                    Id = a.Id, AccountNumber = a.AccountNumber,
                    AccountType = a.AccountType, Balance = a.Balance,
                    Status = a.Status, Currency = a.Currency
                }).ToList()
            };
        }

        public async Task<AdminDashboardDto> GetAdminDashboardAsync()
        {
            var totalUsers = await _context.Users.CountAsync(u => u.Role == "Customer");
            var totalAccounts = await _context.Accounts.CountAsync();
            var totalTx = await _context.Transactions.CountAsync();
            var totalDeposits = await _context.Transactions.Where(t => t.TransactionType == "Deposit").SumAsync(t => t.Amount);
            var totalWithdrawals = await _context.Transactions.Where(t => t.TransactionType == "Withdrawal").SumAsync(t => t.Amount);
            var totalLoan = await _context.Loans.Where(l => l.Status == "Active").SumAsync(l => l.Principal);
            var pendingLoans = await _context.Loans.CountAsync(l => l.Status == "Pending");
            var pendingKyc = await _context.Users.CountAsync(u => u.KycStatus == "Pending");
            var activeCards = await _context.Cards.CountAsync(c => c.IsActive);

            var recentTx = await _context.Transactions
                .OrderByDescending(t => t.CreatedAt).Take(10)
                .Select(t => new TransactionDto
                {
                    Id = t.Id, Amount = t.Amount, TransactionType = t.TransactionType,
                    Status = t.Status, Description = t.Description,
                    CreatedAt = t.CreatedAt, ReferenceNumber = t.ReferenceNumber,
                    SourceAccountId = t.SourceAccountId, DestinationAccountId = t.DestinationAccountId
                }).ToListAsync();

            return new AdminDashboardDto
            {
                TotalUsers = totalUsers, TotalAccounts = totalAccounts,
                TotalDeposits = totalDeposits, TotalWithdrawals = totalWithdrawals,
                TotalTransactions = totalTx, TotalLoanDisbursed = totalLoan,
                PendingLoans = pendingLoans, PendingKyc = pendingKyc,
                ActiveCards = activeCards, RecentTransactions = recentTx
            };
        }
    }

    // ── Notification Service ───────────────────────────────────────────────
    public class NotificationService : INotificationService
    {
        private readonly IApplicationDbContext _context;
        public NotificationService(IApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<NotificationDto>> GetUserNotificationsAsync(Guid userId)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .Take(50)
                .Select(n => MapNotification(n))
                .ToListAsync();
        }

        public async Task<int> GetUnreadCountAsync(Guid userId)
        {
            return await _context.Notifications.CountAsync(n => n.UserId == userId && !n.IsRead);
        }

        public async Task<bool> MarkAsReadAsync(Guid notificationId)
        {
            var n = await _context.Notifications.FindAsync(notificationId);
            if (n == null) return false;
            n.IsRead = true;
            n.ReadAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task MarkAllAsReadAsync(Guid userId)
        {
            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId && !n.IsRead).ToListAsync();
            foreach (var n in notifications) { n.IsRead = true; n.ReadAt = DateTime.UtcNow; }
            await _context.SaveChangesAsync();
        }

        public async Task SendAsync(Guid userId, string title, string message, string type = "Info", string? referenceId = null, string? referenceType = null)
        {
            var notification = new Notification
            {
                UserId = userId, Title = title, Message = message,
                Type = type, ReferenceId = referenceId, ReferenceType = referenceType
            };
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        private static NotificationDto MapNotification(Notification n) => new()
        {
            Id = n.Id, Title = n.Title, Message = n.Message, Type = n.Type,
            ActionUrl = n.ActionUrl, IsRead = n.IsRead, ReadAt = n.ReadAt,
            ReferenceId = n.ReferenceId, ReferenceType = n.ReferenceType,
            CreatedAt = n.CreatedAt
        };
    }
}
