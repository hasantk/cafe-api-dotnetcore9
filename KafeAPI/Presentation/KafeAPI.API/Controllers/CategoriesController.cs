using KafeAPI.Application.Dtos.CategoryDtos;
using KafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KafeAPI.API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryServices _categoryServices;

        public CategoriesController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _categoryServices.GetAllCategories();
            return CreateResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCategory(int id) 
        {
            var result = await _categoryServices.GetByIdCategory(id);
            return CreateResponse(result);
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> AddCategory(CreateCategoryDto dto) 
        {
            var result= await _categoryServices.AddCategory(dto);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto dto) 
        {
            var result = await _categoryServices.UpdateCategory(dto);
            return CreateResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id) 
        {
            var result = await _categoryServices.DeleteCategory(id);
            return CreateResponse(result);
        }

        [HttpGet("categorymenuitems")]
        public async Task<IActionResult> GetAllCategoriesWithMenuItems()
        {
            var result = await _categoryServices.GetCategoriesWithMenuItem();
            return CreateResponse(result);
        }
    }
}
