using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApexBank.Domain.Entities;

namespace ApexBank.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Account> Accounts { get; }
        DbSet<Transaction> Transactions { get; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
