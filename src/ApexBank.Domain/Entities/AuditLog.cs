using System;

namespace ApexBank.Domain.Entities
{
    public class AuditLog : BaseEntity
    {
        public string EntityName { get; set; } = string.Empty;     // "User", "Account", "Transaction"
        public string EntityId { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;         // Created, Updated, Deleted, Login, Logout
        public string? OldValues { get; set; }                     // JSON snapshot
        public string? NewValues { get; set; }                     // JSON snapshot
        public string? UserId { get; set; }
        public string? UserEmail { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string? FailureReason { get; set; }
    }
}
