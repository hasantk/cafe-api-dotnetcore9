using KafeAPI.Application.Dtos.MenuItemDtos;
using KafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KafeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemsController : ControllerBase
    {
        private readonly IMenuItemServices _menuItemServices;

        public MenuItemsController(IMenuItemServices menuItemServices)
        {
            _menuItemServices = menuItemServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMenuItems() 
        {
            var result = await _menuItemServices.GetAllMenuItems();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdMenuItem(int id) 
        {
            var result = await _menuItemServices.GetByIdMenuItem(id);
            return Ok(result);
        }

       [HttpPost]
       public async Task<IActionResult> AddMenuItem(CreateMenuItemDto dto) 
        {
            await _menuItemServices.AddMenuItem(dto);
            return Ok("Menu Item Eklendi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMenuItem(UpdateMenuItemDto dto) 
        {
            await _menuItemServices.UpdateMenuItem(dto);
            return Ok("Menu Item Güncellendi.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMenuItem(int id) 
        {
            await _menuItemServices.DeleteMenuItem(id);
            return Ok("Menu Item Silindi.");
        }
    }
}
