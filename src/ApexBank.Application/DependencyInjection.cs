using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ApexBank.Application.Interfaces;
using ApexBank.Application.Services;


namespace ApexBank.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IFraudDetectionService, FraudDetectionService>();

            return services;
        }


    }
}
