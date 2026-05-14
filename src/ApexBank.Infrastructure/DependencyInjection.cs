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

            // ── Smart URL Parsing for Render/Production ──────────────────
            if (!string.IsNullOrEmpty(connectionString) && connectionString.StartsWith("mysql://"))
            {
                var uri = new Uri(connectionString);
                var userInfo = uri.UserInfo.Split(':');
                var user = userInfo[0];
                var password = userInfo.Length > 1 ? userInfo[1] : "";
                var host = uri.Host;
                var port = uri.Port > 0 ? uri.Port : 3306;
                var database = uri.AbsolutePath.TrimStart('/');
                
                // Cloud DBs like Aiven usually require SSL
                connectionString = $"Server={host};Port={port};Database={database};Uid={user};Pwd={password};SslMode=Required;CharSet=utf8mb4;";
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
