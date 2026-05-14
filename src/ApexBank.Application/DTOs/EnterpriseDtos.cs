using System;
using System.Collections.Generic;

namespace ApexBank.Application.DTOs
{
    // ── Loan DTOs ──────────────────────────────────────────────────────────
    public class LoanApplicationDto
    {
        public string LoanType { get; set; } = "Personal";
        public decimal Principal { get; set; }
        public decimal InterestRate { get; set; } = 10.5m;
        public int TermMonths { get; set; } = 12;
        public string Purpose { get; set; } = string.Empty;
    }

    public class LoanResponseDto
    {
        public Guid Id { get; set; }
        public string LoanNumber { get; set; } = string.Empty;
        public string LoanType { get; set; } = string.Empty;
        public decimal Principal { get; set; }
        public decimal InterestRate { get; set; }
        public int TermMonths { get; set; }
        public decimal MonthlyEmi { get; set; }
        public decimal TotalPayable { get; set; }
        public decimal OutstandingBalance { get; set; }
        public decimal AmountPaid { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Purpose { get; set; } = string.Empty;
        public DateTime? ApprovedAt { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class EmiCalculationDto
    {
        public decimal Principal { get; set; }
        public decimal AnnualRate { get; set; }
        public int TermMonths { get; set; }
        public decimal MonthlyEmi { get; set; }
        public decimal TotalPayable { get; set; }
        public decimal TotalInterest { get; set; }
    }

    // ── Card DTOs ──────────────────────────────────────────────────────────
    public class CardResponseDto
    {
        public Guid Id { get; set; }
        public string CardType { get; set; } = string.Empty;
        public string CardNetwork { get; set; } = string.Empty;
        public string MaskedNumber { get; set; } = string.Empty;
        public string CardHolderName { get; set; } = string.Empty;
        public string ExpiryMonth { get; set; } = string.Empty;
        public string ExpiryYear { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool IsFrozen { get; set; }
        public decimal DailyLimit { get; set; }
        public decimal MonthlyLimit { get; set; }
        public decimal UsedToday { get; set; }
        public DateTime? LastUsedAt { get; set; }
        public bool IsVirtual { get; set; }
        public DateTime? ActivatedAt { get; set; }
    }

    // ── Notification DTO ───────────────────────────────────────────────────
    public class NotificationDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string? ActionUrl { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }
        public string? ReferenceId { get; set; }
        public string? ReferenceType { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    // ── Dashboard DTOs ─────────────────────────────────────────────────────
    public class CustomerDashboardDto
    {
        public string FullName { get; set; } = string.Empty;
        public string KycStatus { get; set; } = string.Empty;
        public decimal TotalBalance { get; set; }
        public int AccountCount { get; set; }
        public int ActiveLoans { get; set; }
        public decimal LoanOutstanding { get; set; }
        public int ActiveCards { get; set; }
        public int UnreadNotifications { get; set; }
        public List<TransactionDto> RecentTransactions { get; set; } = new();
        public List<AccountSummaryDto> Accounts { get; set; } = new();
    }

    public class AdminDashboardDto
    {
        public int TotalUsers { get; set; }
        public int TotalAccounts { get; set; }
        public decimal TotalDeposits { get; set; }
        public decimal TotalWithdrawals { get; set; }
        public int TotalTransactions { get; set; }
        public decimal TotalLoanDisbursed { get; set; }
        public int PendingLoans { get; set; }
        public int PendingKyc { get; set; }
        public int ActiveCards { get; set; }
        public List<TransactionDto> RecentTransactions { get; set; } = new();
    }

    public class AccountSummaryDto
    {
        public Guid Id { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
    }
}
