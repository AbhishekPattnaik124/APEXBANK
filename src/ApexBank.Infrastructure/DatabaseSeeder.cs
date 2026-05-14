using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ApexBank.Domain.Entities;
using ApexBank.Infrastructure.Persistence;

namespace ApexBank.Infrastructure
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(IServiceProvider services, ILogger logger)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            try
            {
                await context.Database.MigrateAsync();
                logger.LogInformation("✅ Database migrations applied.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "❌ Migration failed.");
                return;
            }

            // ── Seed Admin ──────────────────────────────────────────────
            if (!await context.Users.AnyAsync(u => u.Email == "admin@apexbank.in"))
            {
                var admin = new User
                {
                    FirstName = "Apex",
                    LastName = "Admin",
                    Email = "admin@apexbank.in",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@12345"),
                    PhoneNumber = "9000000001",
                    Role = "Admin",
                    KycStatus = "Verified",
                    KycVerifiedAt = DateTime.UtcNow,
                    IsActive = true,
                    Country = "India",
                    City = "Mumbai"
                };
                context.Users.Add(admin);
                await context.SaveChangesAsync();
                logger.LogInformation("✅ Admin user seeded: admin@apexbank.in / Admin@12345");

                // Admin Account
                var adminAccount = new Account
                {
                    UserId = admin.Id,
                    AccountNumber = "APEX0000000001",
                    Balance = 1_000_000m,
                    AccountType = "Current",
                    Status = "Active",
                    BranchCode = "APEX001",
                    IfscCode = "APEX0001234"
                };
                context.Accounts.Add(adminAccount);
                await context.SaveChangesAsync();
            }

            // ── Seed Demo Customer ──────────────────────────────────────
            if (!await context.Users.AnyAsync(u => u.Email == "demo@apexbank.in"))
            {
                var customer = new User
                {
                    FirstName = "Abhishek",
                    LastName = "Pattnaik",
                    Email = "demo@apexbank.in",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Demo@12345"),
                    PhoneNumber = "9000000002",
                    Role = "Customer",
                    KycStatus = "Verified",
                    KycVerifiedAt = DateTime.UtcNow,
                    IsActive = true,
                    Country = "India",
                    City = "Bhubaneswar"
                };
                context.Users.Add(customer);
                await context.SaveChangesAsync();
                logger.LogInformation("✅ Demo customer seeded: demo@apexbank.in / Demo@12345");

                // Savings Account
                var savingsAccount = new Account
                {
                    UserId = customer.Id,
                    AccountNumber = "APEX0000000002",
                    Balance = 75_420.50m,
                    AccountType = "Savings",
                    InterestRate = 4.0m,
                    Status = "Active",
                    BranchCode = "APEX002",
                    IfscCode = "APEX0001234"
                };
                context.Accounts.Add(savingsAccount);
                await context.SaveChangesAsync();

                // Sample Transactions
                var sampleTransactions = new[]
                {
                    new Transaction { SourceAccountId = savingsAccount.Id, Amount = 50000, TransactionType = "Deposit",    Status = "Completed", Description = "Initial Deposit",       ReferenceNumber = "DEP" + Guid.NewGuid().ToString()[..8].ToUpper(), BalanceAfter = 50000,  Channel = "Branch" },
                    new Transaction { SourceAccountId = savingsAccount.Id, Amount = 30000, TransactionType = "Deposit",    Status = "Completed", Description = "Salary Credit",         ReferenceNumber = "DEP" + Guid.NewGuid().ToString()[..8].ToUpper(), BalanceAfter = 80000,  Channel = "Web"    },
                    new Transaction { SourceAccountId = savingsAccount.Id, Amount = 4579.50m, TransactionType = "Withdrawal", Status = "Completed", Description = "Online Shopping",    ReferenceNumber = "WTH" + Guid.NewGuid().ToString()[..8].ToUpper(), BalanceAfter = 75420.50m, Channel = "Mobile"},
                };
                context.Transactions.AddRange(sampleTransactions);

                // Sample Loan
                var loan = new Loan
                {
                    UserId = customer.Id,
                    LoanType = "Personal",
                    LoanNumber = "LN" + Guid.NewGuid().ToString()[..8].ToUpper(),
                    Principal = 200000,
                    InterestRate = 10.5m,
                    TermMonths = 24,
                    MonthlyEmi = 9261.40m,
                    TotalPayable = 222273.60m,
                    OutstandingBalance = 200000,
                    Status = "Active",
                    Purpose = "Home renovation",
                    ApprovedAt = DateTime.UtcNow.AddDays(-10),
                    StartDate = DateTime.UtcNow.AddDays(-10),
                    EndDate = DateTime.UtcNow.AddMonths(24).AddDays(-10)
                };
                context.Loans.Add(loan);

                // Demo Virtual Card
                var card = new Card
                {
                    AccountId = savingsAccount.Id,
                    CardType = "Debit",
                    CardNetwork = "Visa",
                    MaskedNumber = "**** **** **** 4242",
                    CardHolderName = "ABHISHEK PATTNAIK",
                    ExpiryMonth = "12",
                    ExpiryYear = "2029",
                    CardTokenId = Guid.NewGuid().ToString(),
                    IsActive = true,
                    DailyLimit = 50000,
                    MonthlyLimit = 200000,
                    ActivatedAt = DateTime.UtcNow
                };
                context.Cards.Add(card);

                // Welcome Notification
                var notification = new Notification
                {
                    UserId = customer.Id,
                    Title = "Welcome to ApexBank! 🎉",
                    Message = "Your account has been successfully created. Explore your dashboard to get started.",
                    Type = "Success",
                    ActionUrl = "/dashboard"
                };
                context.Notifications.Add(notification);

                await context.SaveChangesAsync();
                logger.LogInformation("✅ Demo data seeded successfully.");
            }
        }
    }
}
