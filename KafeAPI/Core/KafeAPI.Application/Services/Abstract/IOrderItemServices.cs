using KafeAPI.Application.Dtos.OrderItemDtos;
using KafeAPI.Application.Dtos.ResponseDtos;

namespace KafeAPI.Application.Services.Abstract
{
    public interface IOrderItemServices
    {
        Task<ResponseDto<List<ResultOrderItemDto>>> GetAllOrderItems();
        Task<ResponseDto<DetailOrderItemDto>> GetOrderItemById(int id);
        Task<ResponseDto<Object>> AddOrderItem(CreateOrderItemDto dto);
        Task<ResponseDto<Object>> UpdateOrderItem(UpdateOrderItemDto dto);
        Task<ResponseDto<Object>> DeleteOrderItem(int id);
    }
}




