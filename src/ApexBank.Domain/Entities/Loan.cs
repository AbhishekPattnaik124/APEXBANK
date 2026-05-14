using System;

namespace ApexBank.Domain.Entities
{
    public class Loan : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public string LoanType { get; set; } = "Personal"; // Personal, Home, Auto, Education, Business
        public string LoanNumber { get; set; } = string.Empty;

        // Financials
        public decimal Principal { get; set; }
        public decimal InterestRate { get; set; } // Annual %
        public int TermMonths { get; set; }
        public decimal MonthlyEmi { get; set; }
        public decimal TotalPayable { get; set; }
        public decimal AmountPaid { get; set; } = 0;
        public decimal OutstandingBalance { get; set; }

        // Lifecycle
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected, Active, Closed, Defaulted
        public DateTime? ApprovedAt { get; set; }
        public DateTime? DisbursedAt { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? RejectionReason { get; set; }

        // Metadata
        public string Purpose { get; set; } = string.Empty;
        public string? ApprovedByUserId { get; set; }
    }
}
