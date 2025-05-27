using KafeAPI.Application.Dtos.AuthDtos;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Dtos.UserDtos;

namespace KafeAPI.Application.Services.Abstract
{
    public interface IAuthServices
    {
        Task<ResponseDto<Object>> GenerateToken(LoginDto dto); 
    }
}
