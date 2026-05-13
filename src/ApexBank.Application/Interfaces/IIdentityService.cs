using System.Threading.Tasks;
using ApexBank.Application.DTOs;

namespace ApexBank.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthResponseDto> LoginAsync(string email, string password);
        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request);
    }
}
