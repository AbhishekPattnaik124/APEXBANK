using System;

namespace ApexBank.Application.DTOs
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public string AccountType { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public bool IsFrozen { get; set; }
    }
}
