using AutoMapper;
using FluentValidation;
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
        private readonly IValidator<CreateCafeInfoDto> _createCafeInfoValidator;
        private readonly IValidator<UpdateCafeInfoDto> _updateCafeInfoValidator;
        public CafeInfoServices(IGenericRepository<CafeInfo> cafeInfoRepository, IMapper mapper, IValidator<CreateCafeInfoDto> createCafeInfoValidator, IValidator<UpdateCafeInfoDto> updateCafeInfoValidator)
        {
            _cafeInfoRepository = cafeInfoRepository;
            _mapper = mapper;
            _createCafeInfoValidator = createCafeInfoValidator;
            _updateCafeInfoValidator = updateCafeInfoValidator;
        }

        public async Task<ResponseDto<object>> AddCafeInfo(CreateCafeInfoDto dto)
        {
            try
            {
                var validate = await _createCafeInfoValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object> { Success = false, Data = null, ErrorCode = ErrorCodes.ValidationError, Message = validate.Errors.Select(x => x.ErrorMessage).FirstOrDefault() };

                }
                var cafeInfo = _mapper.Map<CafeInfo>(dto);
                await _cafeInfoRepository.AddAsync(cafeInfo);
                return new ResponseDto<object> {Success=true, Data = null, Message="Kafe Bilgisi Başarılı Bir Şekilde Kaydedilmiştir." };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success=false,Data=null,ErrorCode=ErrorCodes.Exception, Message="Bir Hata Oluştu."};
            }
        }

        public async Task<ResponseDto<object>> DeleteCafeInfo(int id)
        {
            try
            {
                var cafeInfo = await _cafeInfoRepository.GetByIdAsync(id);
                if (cafeInfo == null) 
                    return new ResponseDto<object> { Success = false, Data = null, ErrorCode = ErrorCodes.NotFound, Message = "Kafe Bilgisi Bulunamadı." };

                await _cafeInfoRepository.DeleteAsync(cafeInfo);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Kafe Bilgisi Başarılı Bir Şekilde Silindi." };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, ErrorCode = ErrorCodes.Exception, Message = "Bir Hata Oluştu." };
            }
        }

        public async Task<ResponseDto<List<ResultCafeInfoDto>>> GetAllCafeInfos()
        {
            try
            {
                var cafeInfo = await _cafeInfoRepository.GetAllAsync();
                if (cafeInfo == null || !cafeInfo.Any())
                {
                    return new ResponseDto<List<ResultCafeInfoDto>> { Success = false, Data = null, ErrorCode = ErrorCodes.NotFound, Message = "Kafe Bilgisi Bulunamadı." };
                }
                var result = _mapper.Map<List<ResultCafeInfoDto>>(cafeInfo);
                return new ResponseDto<List<ResultCafeInfoDto>> { Success = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultCafeInfoDto>> { Success = false, Data=null, ErrorCode = ErrorCodes.Exception, Message="Bir Hata Oluştu." };
            }
        }

        public async Task<ResponseDto<DetailCafeInfoDto>> GetByIdCafeInfo(int id)
        {
            try
            {
                var cafeInfo = await _cafeInfoRepository.GetByIdAsync(id);
                if (cafeInfo == null)
                {
                    return new ResponseDto<DetailCafeInfoDto> { Success = false, Data = null, ErrorCode = ErrorCodes.NotFound, Message = "Kafe Bilgisi Bulunamadı." };
                }
                var result = _mapper.Map<DetailCafeInfoDto>(cafeInfo);
                return new ResponseDto<DetailCafeInfoDto> { Success=true,Data = result };
            }
            catch (Exception ex)
            {
                return new ResponseDto<DetailCafeInfoDto> { Success = false, Data = null, ErrorCode = ErrorCodes.Exception, Message = "Bir Hata Oluştu" };
            }
        }

        public async Task<ResponseDto<object>> UpdateCafeInfo(UpdateCafeInfoDto dto)
        {
            try
            {
                var validate = await _updateCafeInfoValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object> { Success = false, Data = null, ErrorCode = ErrorCodes.ValidationError, Message = validate.Errors.Select(x => x.ErrorMessage).FirstOrDefault() };

                }
                var cafeInfo = await _cafeInfoRepository.GetByIdAsync(dto.Id);
                if (cafeInfo == null) 
                {
                    return new ResponseDto<object> { Success = false, Data = null, ErrorCode = ErrorCodes.NotFound, Message = "Kafe Bilgisi Bulunamadı." };
                }

                var result = _mapper.Map(dto, cafeInfo);
                await _cafeInfoRepository.UpdateAsync(result);
                return new ResponseDto<object> {Success=true, Data=null,Message="Kafe Bilgisi Güncellendi."};
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, ErrorCode = ErrorCodes.Exception, Message = "Bir Hata Oluştu." };
            }
        }
    }
}
