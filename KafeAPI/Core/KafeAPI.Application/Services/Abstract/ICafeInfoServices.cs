using KafeAPI.Application.Dtos.CafeInfoDtos;
using KafeAPI.Application.Dtos.ResponseDtos;

namespace KafeAPI.Application.Services.Abstract
{
    public interface ICafeInfoServices
    {
        Task<ResponseDto<List<ResultCafeInfoDto>>> GetAllCafeInfos();
        Task<ResponseDto<DetailCafeInfoDto>> GetByIdCafeInfo(int id);
        Task<ResponseDto<Object>> AddCafeInfoDto(CreateCafeInfoDto dto);
        Task<ResponseDto<Object>> UpdateCafeInfoDto(UpdateCafeInfoDto dto);
        Task<ResponseDto<Object>> DeleteCafeInfoDto(int id);
    }
}
