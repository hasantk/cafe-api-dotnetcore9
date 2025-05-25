using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Dtos.UserDtos;

namespace KafeAPI.Application.Services.Abstract
{
    public interface IUserServices
    {
        Task<ResponseDto<Object>> Register(RegisterDto dto);
    }
}
