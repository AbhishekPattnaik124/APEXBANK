using System.Threading.Tasks;
using ApexBank.Application.Interfaces;
using ApexBank.Domain.Entities;

namespace ApexBank.Application.Services
{
    public class FraudDetectionService : IFraudDetectionService
    {
        public Task<bool> IsTransactionSuspicious(Transaction transaction)
        {
            // Simple rule-based fraud detection
            bool isSuspicious = false;

            // Rule 1: High amount transfer
            if (transaction.Amount > 10000) 
            {
                isSuspicious = true;
            }

            // Rule 2: Suspicious descriptions
            if (transaction.Description.ToLower().Contains("crypto") || 
                transaction.Description.ToLower().Contains("unknown"))
            {
                isSuspicious = true;
            }

            return Task.FromResult(isSuspicious);
        }
    }
}
