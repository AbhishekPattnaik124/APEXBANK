using System;
using System.Collections.Generic;

namespace ApexBank.Domain.Entities
{
    public class Account : BaseEntity
    {
        public string AccountNumber { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public string AccountType { get; set; } = "Savings"; // Savings, Current, Business
        public string Currency { get; set; } = "INR";
        public string Status { get; set; } = "Active"; // Active, Suspended, Closed
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;
        public bool IsFrozen { get; set; } = false;

        // Financial Metadata
        public decimal CreditLimit { get; set; } = 0;
        public decimal InterestRate { get; set; } = 3.5m;
        public DateTime OpenedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ClosedAt { get; set; }
        public string BranchCode { get; set; } = "APEX001";
        public string IfscCode { get; set; } = "APEX0001234";

        // Relationships
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
        public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
    }
}
