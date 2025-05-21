using KafeAPI.Application.Dtos.AuthDtos;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Helpers;
using KafeAPI.Application.Services.Abstract;

namespace KafeAPI.Application.Services.Concrete
{
    public class AuthServices : IAuthServices
    {
        private readonly TokenHelpers _tokenHelpers;

        public AuthServices(TokenHelpers tokenHelpers)
        {
            _tokenHelpers = tokenHelpers;
        }

        public async Task<ResponseDto<object>> GenerateToken(TokenDto dto)
        {
			try
			{
                var chechuser= dto.Email == "admin@admin.com" ? true: false;
                if (chechuser) 
                {
                    string token = _tokenHelpers.GenerateToken(dto);
                    return new ResponseDto<object> { Success = true, Data = token };
                }
                return new ResponseDto<object> { Success = false, Data = null, Message = "Kullanıcı Bulunamadı.", ErrorCode = ErrorCodes.Unauthorized };
			}
			catch (Exception ex)
			{
                return new ResponseDto<object> { Success=false, Data=null, Message="Bir Hata Oluştu.",ErrorCode=ErrorCodes.Exception};
			}
        }
    }
}
