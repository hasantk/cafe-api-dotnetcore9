using KafeAPI.Application.Interfaces;
using KafeAPI.Domain.Entities;
using KafeAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace KafeAPI.Persistence.Repository
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly AppDbContext _context;

        public MenuItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<MenuItem>> GetMenuItemFilterByCategoryId(int categoryId)
        {
            var result = await _context.MenuItems.Where(x => x.CategoryId == categoryId).ToListAsync();
            return result;
        }
    }
}
