using KafeAPI.Application.Dtos.OrderDtos;
using KafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KafeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseController
    {
        private readonly IOrderServices _orderServices;
        public OrdersController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(CreateOrderDto dto) 
        {
            var result = await _orderServices.AddOrder(dto);
            return CreateResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        {
            var result = await _orderServices.GetAllOrder();
            return CreateResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdOrder(int id) 
        {
            var result = await _orderServices.GetByIdOrder(id);
            return CreateResponse(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder(UpdateOrderDto dto)
        {
            var result = await _orderServices.UpdateOrder(dto);
            return CreateResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderServices.DeleteOrder(id);
            return CreateResponse(result);
        }

        [HttpGet("getAllOrdersWithDetail")]
        public async Task<IActionResult> GetAllOrdersWithDetail() 
        {
            var result = await _orderServices.GetAllOrderWithDetail();
            return CreateResponse(result);
        }

        [HttpPut("updateOrderStatusHazir")]
        public async Task<IActionResult> UpdateOrderStatusHazir(int orderId) 
        {
            var result = await _orderServices.UpdateOrderStatusHazir(orderId);
            return CreateResponse(result);
        }

        [HttpPut("updateOrderStatusIptalEdildi")]
        public async Task<IActionResult> UpdateOrderStatusIptalEdildi(int orderId)
        {
            var result = await _orderServices.UpdateOrderStatusIptalEdildi(orderId);
            return CreateResponse(result);
        }

        [HttpPut("updateOrderStatusTeslimEdildi")]
        public async Task<IActionResult> UpdateOrderStatusTeslimEdildi(int orderId)
        {
            var result = await _orderServices.UpdateOrderStatusTeslimEdildi(orderId);
            return CreateResponse(result);
        }

        [HttpPut("updateOrderStatusOdendi")]
        public async Task<IActionResult> UpdateOrderStatusOdendi(int orderId)
        {
            var result = await _orderServices.UpdateOrderStatusOdendi(orderId);
            return CreateResponse(result);
        }

        //[HttpPut("addOrderItemByOrder")]
        //public async Task<IActionResult> AddOrderItemByOrder(AddOrderItemByOrderDto dto)
        //{
        //    var result = await _orderServices.AddOrderItemByOrderId(dto);
        //    return CreateResponse(result);
        //}
    }
}
