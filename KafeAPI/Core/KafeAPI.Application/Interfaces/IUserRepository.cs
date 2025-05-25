using KafeAPI.Application.Dtos.UserDtos;
using Microsoft.AspNetCore.Identity;

namespace KafeAPI.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<SignInResult> LoginAsync(LoginDto dto);
        Task<IdentityResult> RegisterAsync(RegisterDto dto);
        Task LogoutAsync();
    }
}
