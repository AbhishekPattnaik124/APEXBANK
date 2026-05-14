using Microsoft.Extensions.DependencyInjection;
using ApexBank.Application.Interfaces;
using ApexBank.Application.Services;

namespace ApexBank.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Existing Services
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IFraudDetectionService, FraudDetectionService>();

            // Enterprise Services
            services.AddScoped<ILoanService, LoanService>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<INotificationService, NotificationService>();

            return services;
        }
    }
}
