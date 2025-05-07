using KafeAPI.Domain.Entities;

namespace KafeAPI.Application.Interfaces
{
    public interface ITableRespository
    {
        Task<Table> GetByTableNumberAsync(int tableNumber);
    }
}
