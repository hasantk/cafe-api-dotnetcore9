using KafeAPI.Application.Dtos.OrderDtos;
using KafeAPI.Application.Dtos.ResponseDtos;

namespace KafeAPI.Application.Services.Abstract
{
    public interface IOrderServices
    {
        Task<ResponseDto<List<ResultOrderDto>>> GetAllOrder();
        Task<ResponseDto<DetailOrderDto>> GetByIdOrder(int orderId);
        Task<ResponseDto<Object>> AddOrder(CreateOrderDto dto);
        Task<ResponseDto<Object>> UpdateOrder(UpdateOrderDto dto);
        Task<ResponseDto<Object>> DeleteOrder(int orderId);
    }
}
