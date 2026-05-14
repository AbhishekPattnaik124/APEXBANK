using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApexBank.Application.DTOs;

namespace ApexBank.Application.Interfaces
{
    public interface ILoanService
    {
        Task<LoanResponseDto> ApplyAsync(Guid userId, LoanApplicationDto dto);
        Task<LoanResponseDto?> GetByIdAsync(Guid loanId);
        Task<IEnumerable<LoanResponseDto>> GetUserLoansAsync(Guid userId);
        Task<IEnumerable<LoanResponseDto>> GetAllLoansAsync(); // Admin
        Task<bool> ApproveAsync(Guid loanId, string approvedByUserId);
        Task<bool> RejectAsync(Guid loanId, string reason);
        Task<EmiCalculationDto> CalculateEmiAsync(decimal principal, decimal annualRate, int termMonths);
    }

    public interface ICardService
    {
        Task<CardResponseDto> IssueCardAsync(Guid accountId, string cardType, string cardNetwork);
        Task<IEnumerable<CardResponseDto>> GetAccountCardsAsync(Guid accountId);
        Task<bool> FreezeCardAsync(Guid cardId);
        Task<bool> UnfreezeCardAsync(Guid cardId);
        Task<bool> UpdateLimitsAsync(Guid cardId, decimal dailyLimit, decimal monthlyLimit);
        Task<bool> BlockCardAsync(Guid cardId);
    }

    public interface IDashboardService
    {
        Task<CustomerDashboardDto> GetCustomerDashboardAsync(Guid userId);
        Task<AdminDashboardDto> GetAdminDashboardAsync();
    }

    public interface INotificationService
    {
        Task<IEnumerable<NotificationDto>> GetUserNotificationsAsync(Guid userId);
        Task<int> GetUnreadCountAsync(Guid userId);
        Task<bool> MarkAsReadAsync(Guid notificationId);
        Task MarkAllAsReadAsync(Guid userId);
        Task SendAsync(Guid userId, string title, string message, string type = "Info", string? referenceId = null, string? referenceType = null);
    }
}
