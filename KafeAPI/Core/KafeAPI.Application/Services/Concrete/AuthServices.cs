using KafeAPI.Application.Dtos.AuthDtos;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Dtos.UserDtos;
using KafeAPI.Application.Helpers;
using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Services.Abstract;

namespace KafeAPI.Application.Services.Concrete
{
    public class AuthServices : IAuthServices
    {
        private readonly TokenHelpers _tokenHelpers;
        private readonly IUserRepository _userRepository;

        public AuthServices(TokenHelpers tokenHelpers, IUserRepository userRepository)
        {
            _tokenHelpers = tokenHelpers;
            _userRepository = userRepository;
        }

        public async Task<ResponseDto<object>> GenerateToken(LoginDto dto)
        {
			try
			{
                //var checkuser= dto.Email == "admin@admin.com" ? true: false;
                var checkuser = await _userRepository.CheckUser(dto.Email);
                if (checkuser.Id != null) 
                {
                    var user = await _userRepository.CheckUserWithPassword(dto);
                    if (user.Succeeded)
                    {
                        var tokendto = new TokenDto 
                        {
                            Email = dto.Email,
                            Id = checkuser.Id,
                            Role = "Admin"
                        };
                        string token = _tokenHelpers.GenerateToken(tokendto);
                        return new ResponseDto<object> { Success = true, Data = token };
                    }
                    return new ResponseDto<object> { Success = false, Data = null, Message = "Kullanıcı Bulunamadı.", ErrorCode = ErrorCodes.Unauthorized };
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
