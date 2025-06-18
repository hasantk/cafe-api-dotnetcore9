using AutoMapper;
using KafeAPI.Application.Dtos.CafeInfoDtos;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Services.Abstract;
using KafeAPI.Domain.Entities;

namespace KafeAPI.Application.Services.Concrete
{
    public class CafeInfoServices : ICafeInfoServices
    {
        private readonly IGenericRepository<CafeInfo> _cafeInfoRepository;
        private readonly IMapper _mapper;
        public CafeInfoServices(IGenericRepository<CafeInfo> cafeInfoRepository, IMapper mapper)
        {
            _cafeInfoRepository = cafeInfoRepository;
            _mapper = mapper;
        }

        public Task<ResponseDto<object>> AddCafeInfoDto(CreateCafeInfoDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<object>> DeleteCafeInfoDto(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto<List<ResultCafeInfoDto>>> GetAllCafeInfos()
        {
            try
            {
                var cafeInfo = await _cafeInfoRepository.GetAllAsync();
                if (cafeInfo == null || !cafeInfo.Any())
                {
                    return new ResponseDto<List<ResultCafeInfoDto>> { Success=false, Data=null,ErrorCode=ErrorCodes.NotFound,Message="Kafe Bilgisi Bulunamadı."}
                }
                var result = _mapper.Map<List<ResultCafeInfoDto>>(cafeInfo);
                return new ResponseDto<List<ResultCafeInfoDto>> { Success = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultCafeInfoDto>> { Success = false, Data=null, ErrorCode = ErrorCodes.Exception, Message="Bir Hata Oluştu." };
            }
        }

        public Task<ResponseDto<DetailCafeInfoDto>> GetByIdCafeInfo(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<object>> UpdateCafeInfoDto(UpdateCafeInfoDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
