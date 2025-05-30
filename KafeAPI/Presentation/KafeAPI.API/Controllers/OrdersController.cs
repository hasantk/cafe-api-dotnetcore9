using KafeAPI.Application.Dtos.OrderDtos;
using KafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KafeAPI.API.Controllers
{
    [Route("api/orders")]
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

        [Authorize(Roles = "admin,employe")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        {
            var result = await _orderServices.GetAllOrder();
            return CreateResponse(result);
        }

        [Authorize(Roles = "admin,employe")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdOrder(int id) 
        {
            var result = await _orderServices.GetByIdOrder(id);
            return CreateResponse(result);
        }

        [Authorize(Roles = "admin,employe")]
        [HttpPut]
        public async Task<IActionResult> UpdateOrder(UpdateOrderDto dto)
        {
            var result = await _orderServices.UpdateOrder(dto);
            return CreateResponse(result);
        }

        [Authorize(Roles = "admin,employe")]
        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderServices.DeleteOrder(id);
            return CreateResponse(result);
        }

        [Authorize(Roles = "admin,employe")]
        [HttpGet("withdetails")]
        public async Task<IActionResult> GetAllOrdersWithDetail() 
        {
            var result = await _orderServices.GetAllOrderWithDetail();
            return CreateResponse(result);
        }

        [Authorize(Roles = "admin,employe")]
        [HttpPut("status/hazir")]
        public async Task<IActionResult> UpdateOrderStatusHazir(int orderId) 
        {
            var result = await _orderServices.UpdateOrderStatusHazir(orderId);
            return CreateResponse(result);
        }

        [Authorize(Roles = "admin,employe")]
        [HttpPut("status/iptaledildi")]
        public async Task<IActionResult> UpdateOrderStatusIptalEdildi(int orderId)
        {
            var result = await _orderServices.UpdateOrderStatusIptalEdildi(orderId);
            return CreateResponse(result);
        }

        [Authorize(Roles = "admin,employe")]
        [HttpPut("status/teslimedildi")]
        public async Task<IActionResult> UpdateOrderStatusTeslimEdildi(int orderId)
        {
            var result = await _orderServices.UpdateOrderStatusTeslimEdildi(orderId);
            return CreateResponse(result);
        }

        [Authorize(Roles = "admin,employe")]
        [HttpPut("status/odendi")]
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
