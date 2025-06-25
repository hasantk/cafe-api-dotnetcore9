using KafeAPI.Application.Dtos.CafeInfoDtos;
using KafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KafeAPI.API.Controllers
{
    [Route("api/cafeinfos")]
    [ApiController]
    public class CafeInfosController : BaseController
    {
        private readonly ICafeInfoServices _cafeInfoServices;

        public CafeInfosController(ICafeInfoServices cafeInfoServices)
        {
            _cafeInfoServices = cafeInfoServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCafeInfos() 
        {
            var result = await _cafeInfoServices.GetAllCafeInfos();
            return CreateResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCafeInfo(int id) 
        {
            var result = await _cafeInfoServices.GetByIdCafeInfo(id);
            return CreateResponse(result);
        }

        [Authorize(Roles ="admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCafeInfo(CreateCafeInfoDto dto) 
        {
            var result = await _cafeInfoServices.AddCafeInfo(dto);
            return CreateResponse(result);
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateCafeInfo(UpdateCafeInfoDto dto)
        {
            var result = await _cafeInfoServices.UpdateCafeInfo(dto);
            return CreateResponse(result);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCafeInfo(int id)
        {
            var result = await _cafeInfoServices.DeleteCafeInfo(id);
            return CreateResponse(result);
        }
    }
}
