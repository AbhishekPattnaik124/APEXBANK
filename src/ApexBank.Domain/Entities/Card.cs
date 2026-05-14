using System;

namespace ApexBank.Domain.Entities
{
    public class Card : BaseEntity
    {
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; } = null!;

        public string CardType { get; set; } = "Debit"; // Debit, Credit, Prepaid
        public string CardNetwork { get; set; } = "Visa"; // Visa, Mastercard, RuPay
        public string MaskedNumber { get; set; } = string.Empty; // e.g. **** **** **** 4242
        public string CardHolderName { get; set; } = string.Empty;
        public string ExpiryMonth { get; set; } = string.Empty;
        public string ExpiryYear { get; set; } = string.Empty;

        // Security (never store full CVV/PAN in real apps)
        public string CardTokenId { get; set; } = string.Empty; // Vault token

        // Limits & Status
        public bool IsActive { get; set; } = true;
        public bool IsFrozen { get; set; } = false;
        public decimal DailyLimit { get; set; } = 50000;
        public decimal MonthlyLimit { get; set; } = 200000;
        public decimal UsedToday { get; set; } = 0;
        public DateTime? LastUsedAt { get; set; }

        // Virtual Card
        public bool IsVirtual { get; set; } = false;
        public DateTime? ActivatedAt { get; set; }
        public DateTime? BlockedAt { get; set; }
    }
}
