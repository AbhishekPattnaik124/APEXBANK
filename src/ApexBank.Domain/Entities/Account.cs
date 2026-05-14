using System;
using System.Collections.Generic;

namespace ApexBank.Domain.Entities
{
    public class Account : BaseEntity
    {
        public string AccountNumber { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public string AccountType { get; set; } = "Savings"; // Savings, Current
        public string Currency { get; set; } = "USD";
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;
        public bool IsFrozen { get; set; } = false;

        // Relationships
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
