using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ApexBank.Application.Interfaces;
using ApexBank.Infrastructure.Persistence;
using ApexBank.Infrastructure.Identity;


namespace ApexBank.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            Console.WriteLine($"[DB DEBUG] Connection string found: {!string.IsNullOrEmpty(connectionString)}");

            if (!string.IsNullOrEmpty(connectionString) && connectionString.StartsWith("mysql://"))
            {
                Console.WriteLine("[DB DEBUG] Detected mysql:// URL format. Parsing...");
                try 
                {
                    var uri = new Uri(connectionString);
                    var userInfo = uri.UserInfo.Split(':');
                    var user = userInfo[0];
                    var password = userInfo.Length > 1 ? userInfo[1] : "";
                    var host = uri.Host;
                    var port = uri.Port > 0 ? uri.Port : 3306;
                    var database = uri.AbsolutePath.TrimStart('/');
                    
                    connectionString = $"Server={host};Port={port};Database={database};Uid={user};Pwd={password};SslMode=Required;CharSet=utf8mb4;";
                    Console.WriteLine($"[DB DEBUG] Successfully parsed URL to Server={host};Database={database}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[DB ERROR] Failed to parse mysql:// URL: {ex.Message}");
                }
            }

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("❌ Connection string 'DefaultConnection' is null or empty! Please check your Render Environment Variables.");
            }

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    connectionString,
                    new MySqlServerVersion(new Version(8, 0, 31)),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));


            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IIdentityService, IdentityService>();

            return services;
        }

    }
}
