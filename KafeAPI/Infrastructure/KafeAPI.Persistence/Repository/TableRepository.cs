using KafeAPI.Application.Interfaces;
using KafeAPI.Domain.Entities;
using KafeAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace KafeAPI.Persistence.Repository
{
    public class TableRepository : ITableRespository
    {
        private readonly AppDbContext _context;

        public TableRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Table>> GetAllActiveTablesAsync()
        {
            var result = await _context.Tables.Where(x => x.IsActive == true).ToListAsync();
            return result;
        }

        public async Task<Table> GetByTableNumberAsync(int tableNumber)
        {
            var result = await _context.Tables.FirstOrDefaultAsync(x => x.TableNumber == tableNumber);
            return result;
        }
    }
}
