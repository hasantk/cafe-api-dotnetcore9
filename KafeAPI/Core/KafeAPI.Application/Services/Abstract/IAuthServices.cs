using KafeAPI.Application.Dtos.ResponseDtos;

namespace KafeAPI.Application.Services.Abstract
{
    public interface IAuthServices
    {
        Task<ResponseDto<Object>> GenerateToken(string email); 
    }
}
