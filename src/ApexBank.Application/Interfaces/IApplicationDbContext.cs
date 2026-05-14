using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApexBank.Domain.Entities;

namespace ApexBank.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        // Core
        DbSet<User> Users { get; }
        DbSet<Account> Accounts { get; }
        DbSet<Transaction> Transactions { get; }

        // Enterprise
        DbSet<Loan> Loans { get; }
        DbSet<Card> Cards { get; }
        DbSet<AuditLog> AuditLogs { get; }
        DbSet<Notification> Notifications { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
