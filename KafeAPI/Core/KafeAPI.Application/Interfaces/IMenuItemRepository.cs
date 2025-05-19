using KafeAPI.Domain.Entities;

namespace KafeAPI.Application.Interfaces
{
    public interface IMenuItemRepository
    {
        Task<List<MenuItem>> GetMenuItemFilterByCategoryId(int categoryId);
    }
}
