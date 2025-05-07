using KafeAPI.Application.Dtos.MenuItemDtos;
using KafeAPI.Application.Dtos.ResponseDtos;

namespace KafeAPI.Application.Services.Abstract
{
    public interface IMenuItemServices
    {
        Task<ResponseDto<List<ResultMenuItemDto>>> GetAllMenuItems();
        Task<ResponseDto<DetailMenuItemDto>> GetByIdMenuItem(int id);
        Task<ResponseDto<Object>> AddMenuItem(CreateMenuItemDto dto);
        Task<ResponseDto<Object>> UpdateMenuItem(UpdateMenuItemDto dto);
        Task<ResponseDto<Object>> DeleteMenuItem(int id);
    }
}
