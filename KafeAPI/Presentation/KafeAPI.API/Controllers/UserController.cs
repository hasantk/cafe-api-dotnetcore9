using KafeAPI.Application.Dtos.UserDtos;
using KafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KafeAPI.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        //[Authorize(Roles = "admin")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto) 
        {
            var result = await _userServices.Register(dto);
            return CreateResponse(result);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("createrole")]
        public async Task<IActionResult> CreateRole(string roleName) 
        {
            var result = await _userServices.CreateRole(roleName);
            return CreateResponse(result);
        }

        //[Authorize(Roles = "admin")]
        [HttpPost("addrole")]
        public async Task<IActionResult> AddRoleToUser(string email,string roleName) 
        {
            var result = await _userServices.AddToRole(email,roleName);
            return CreateResponse(result);
        }
    }
}
