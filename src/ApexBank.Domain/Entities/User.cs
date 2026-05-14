using System;
using System.Collections.Generic;

namespace ApexBank.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Role { get; set; } = "Customer"; // Admin, Employee, Customer
        public bool IsActive { get; set; } = true;

        // Extended Profile
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; } = "India";
        public string? PostalCode { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? NationalId { get; set; } // Aadhar / Passport
        public string? ProfileImageUrl { get; set; }

        // KYC & Compliance
        public string KycStatus { get; set; } = "Pending"; // Pending, Verified, Rejected
        public DateTime? KycVerifiedAt { get; set; }

        // Security & Audit
        public DateTime? LastLoginAt { get; set; }
        public int FailedLoginAttempts { get; set; } = 0;
        public bool IsLockedOut { get; set; } = false;
        public DateTime? LockoutEndAt { get; set; }

        // Relationships
        public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
        public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
