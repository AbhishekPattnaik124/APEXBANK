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
        
        // Relationships
        public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}
