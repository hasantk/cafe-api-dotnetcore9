using FluentValidation;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Dtos.UserDtos;
using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Services.Abstract;

namespace KafeAPI.Application.Services.Concrete
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<RegisterDto> _registerValidator;

        public UserServices(IUserRepository userRepository, IValidator<RegisterDto> registerValidator)
        {
            _userRepository = userRepository;
            _registerValidator = registerValidator;
        }

        public async Task<ResponseDto<object>> Register(RegisterDto dto)
        {
            try
            {
                var validate = await _registerValidator.ValidateAsync(dto);
                if (!validate.IsValid) 
                {
                    return new ResponseDto<object> 
                    {
                        Success = true,
                        Data = null,
                        Message = validate.Errors.FirstOrDefault().ErrorMessage,
                        ErrorCode = ErrorCodes.ValidationError
                    };
                }

                var result = await _userRepository.RegisterAsync(dto);

                if (result.Succeeded)
                {
                    return new ResponseDto<object> { Success=true,Data=null, Message="Kayit İşlemi Başarılı Bir Şekilde Gerçekleşmiştir."};
                }
                else
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = result.Errors.FirstOrDefault().Description };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Hata Oluştu." };
            }
        }
    }
}
