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

        public async Task<ResponseDto<object>> GenerateToken(string email)
        {
			try
			{
                string token = _tokenHelpers.GenerateToken(email);
                return new ResponseDto<object> { Success = true, Data = token };
			}
			catch (Exception ex)
			{
                return new ResponseDto<object> { Success=false, Data=null, Message="Bir Hata Oluştu.",ErrorCode=ErrorCodes.Exception};
			}
        }
    }
}
