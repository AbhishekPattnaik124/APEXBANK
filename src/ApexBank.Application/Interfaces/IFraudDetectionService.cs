using System.Threading.Tasks;
using ApexBank.Domain.Entities;

namespace ApexBank.Application.Interfaces
{
    public interface IFraudDetectionService
    {
        Task<bool> IsTransactionSuspicious(Transaction transaction);
    }
}
