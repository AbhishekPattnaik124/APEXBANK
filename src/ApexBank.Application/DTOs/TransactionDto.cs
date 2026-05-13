using System;

namespace ApexBank.Application.DTOs
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public Guid SourceAccountId { get; set; }
        public Guid? DestinationAccountId { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string ReferenceNumber { get; set; } = string.Empty;
    }
}
