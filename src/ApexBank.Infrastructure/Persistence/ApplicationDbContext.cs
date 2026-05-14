using System.Reflection;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ApexBank.Application.Interfaces;
using ApexBank.Domain.Entities;

namespace ApexBank.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Core Tables
        public DbSet<User> Users => Set<User>();
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<Transaction> Transactions => Set<Transaction>();

        // Enterprise Tables
        public DbSet<Loan> Loans => Set<Loan>();
        public DbSet<Card> Cards => Set<Card>();
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
        public DbSet<Notification> Notifications => Set<Notification>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ── Users ──────────────────────────────────────────────────
            builder.Entity<User>(e =>
            {
                e.HasIndex(u => u.Email).IsUnique();
                e.Property(u => u.Email).HasMaxLength(256).IsRequired();
                e.Property(u => u.FirstName).HasMaxLength(100);
                e.Property(u => u.LastName).HasMaxLength(100);
                e.Property(u => u.PhoneNumber).HasMaxLength(15);
                e.Property(u => u.KycStatus).HasMaxLength(20).HasDefaultValue("Pending");
                e.Property(u => u.Role).HasMaxLength(20).HasDefaultValue("Customer");
            });

            // ── Accounts ───────────────────────────────────────────────
            builder.Entity<Account>(e =>
            {
                e.HasIndex(a => a.AccountNumber).IsUnique();
                e.Property(a => a.AccountNumber).HasMaxLength(20).IsRequired();
                e.Property(a => a.Balance).HasPrecision(18, 2);
                e.Property(a => a.CreditLimit).HasPrecision(18, 2);
                e.Property(a => a.InterestRate).HasPrecision(5, 2);
                e.Property(a => a.AccountType).HasMaxLength(20);
                e.Property(a => a.Status).HasMaxLength(20).HasDefaultValue("Active");
                e.Property(a => a.Currency).HasMaxLength(5).HasDefaultValue("INR");
                e.HasOne(a => a.User)
                    .WithMany(u => u.Accounts)
                    .HasForeignKey(a => a.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ── Transactions ───────────────────────────────────────────
            builder.Entity<Transaction>(e =>
            {
                e.HasIndex(t => t.ReferenceNumber).IsUnique();
                e.Property(t => t.Amount).HasPrecision(18, 2);
                e.Property(t => t.Fee).HasPrecision(18, 2);
                e.Property(t => t.BalanceAfter).HasPrecision(18, 2);
                e.Property(t => t.ExchangeRate).HasPrecision(10, 4);
                e.Property(t => t.TransactionType).HasMaxLength(20);
                e.Property(t => t.Status).HasMaxLength(20).HasDefaultValue("Completed");
                e.Property(t => t.Channel).HasMaxLength(20).HasDefaultValue("Web");
                e.Property(t => t.Currency).HasMaxLength(5).HasDefaultValue("INR");
                e.HasOne(t => t.SourceAccount)
                    .WithMany(a => a.Transactions)
                    .HasForeignKey(t => t.SourceAccountId)
                    .OnDelete(DeleteBehavior.Restrict);
                e.HasOne(t => t.DestinationAccount)
                    .WithMany()
                    .HasForeignKey(t => t.DestinationAccountId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ── Loans ──────────────────────────────────────────────────
            builder.Entity<Loan>(e =>
            {
                e.HasIndex(l => l.LoanNumber).IsUnique();
                e.Property(l => l.Principal).HasPrecision(18, 2);
                e.Property(l => l.MonthlyEmi).HasPrecision(18, 2);
                e.Property(l => l.TotalPayable).HasPrecision(18, 2);
                e.Property(l => l.AmountPaid).HasPrecision(18, 2);
                e.Property(l => l.OutstandingBalance).HasPrecision(18, 2);
                e.Property(l => l.InterestRate).HasPrecision(5, 2);
                e.Property(l => l.LoanType).HasMaxLength(30);
                e.Property(l => l.Status).HasMaxLength(20).HasDefaultValue("Pending");
                e.HasOne(l => l.User)
                    .WithMany(u => u.Loans)
                    .HasForeignKey(l => l.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ── Cards ──────────────────────────────────────────────────
            builder.Entity<Card>(e =>
            {
                e.Property(c => c.DailyLimit).HasPrecision(18, 2);
                e.Property(c => c.MonthlyLimit).HasPrecision(18, 2);
                e.Property(c => c.UsedToday).HasPrecision(18, 2);
                e.Property(c => c.CardType).HasMaxLength(20);
                e.Property(c => c.CardNetwork).HasMaxLength(20);
                e.Property(c => c.MaskedNumber).HasMaxLength(25);
                e.HasOne(c => c.Account)
                    .WithMany(a => a.Cards)
                    .HasForeignKey(c => c.AccountId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ── Notifications ──────────────────────────────────────────
            builder.Entity<Notification>(e =>
            {
                e.Property(n => n.Title).HasMaxLength(200);
                e.Property(n => n.Type).HasMaxLength(20).HasDefaultValue("Info");
                e.HasOne(n => n.User)
                    .WithMany(u => u.Notifications)
                    .HasForeignKey(n => n.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ── AuditLogs ──────────────────────────────────────────────
            builder.Entity<AuditLog>(e =>
            {
                e.Property(a => a.EntityName).HasMaxLength(100);
                e.Property(a => a.Action).HasMaxLength(50);
                e.Property(a => a.IpAddress).HasMaxLength(50);
                e.HasIndex(a => a.EntityId);
                e.HasIndex(a => a.CreatedAt);
            });

            // MySQL UTF8MB4 charset for full Unicode/emoji support
            builder.HasCharSet("utf8mb4");
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Auto-set CreatedAt on new entities
            foreach (var entry in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added && e.Entity is BaseEntity))
            {
                ((BaseEntity)entry.Entity).CreatedAt = DateTime.UtcNow;
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
