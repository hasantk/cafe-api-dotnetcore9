using KafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KafeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthServices _authServices;

        public AuthController(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        [HttpGet("generateToken")]
        public async Task<IActionResult> GenerateToken(string email) 
        {
            var result = await _authServices.GenerateToken(email);
            return CreateResponse(result);
        }
    }
}
