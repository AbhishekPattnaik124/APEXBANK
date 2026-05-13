using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApexBank.Application.DTOs;

namespace ApexBank.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<bool> TransferAsync(Guid sourceAccountId, Guid destinationAccountId, decimal amount, string description);
        Task<bool> DepositAsync(Guid accountId, decimal amount);
        Task<bool> WithdrawAsync(Guid accountId, decimal amount);
        Task<IEnumerable<TransactionDto>> GetAccountTransactionsAsync(Guid accountId);
    }
}
