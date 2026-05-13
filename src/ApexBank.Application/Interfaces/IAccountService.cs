using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApexBank.Application.DTOs;

namespace ApexBank.Application.Interfaces
{
    public interface IAccountService
    {
        Task<AccountDto> GetAccountByIdAsync(Guid id);
        Task<IEnumerable<AccountDto>> GetUserAccountsAsync(Guid userId);
        Task<AccountDto> CreateAccountAsync(Guid userId, string accountType);
        Task<bool> UpdateAccountStatusAsync(Guid id, bool isFrozen);
    }
}
