using System;

namespace ApexBank.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public Guid SourceAccountId { get; set; }
        public virtual Account SourceAccount { get; set; } = null!;
        public Guid? DestinationAccountId { get; set; }
        public virtual Account? DestinationAccount { get; set; }
        
        public decimal Amount { get; set; }
        public string TransactionType { get; set; } = string.Empty; // Transfer, Deposit, Withdrawal
        public string Status { get; set; } = "Completed"; // Pending, Completed, Failed
        public string Description { get; set; } = string.Empty;
        public string ReferenceNumber { get; set; } = string.Empty;
    }
}
