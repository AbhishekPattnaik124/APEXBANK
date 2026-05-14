using System;

namespace ApexBank.Domain.Entities
{
    public class Notification : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Type { get; set; } = "Info"; // Info, Success, Warning, Alert, Transaction
        public string? ActionUrl { get; set; }
        public bool IsRead { get; set; } = false;
        public DateTime? ReadAt { get; set; }
        public string? ReferenceId { get; set; } // e.g. TransactionId, LoanId
        public string? ReferenceType { get; set; } // "Transaction", "Loan", "Card"
    }
}
