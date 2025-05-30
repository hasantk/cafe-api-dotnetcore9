//using KafeAPI.Application.Dtos.OrderItemDtos;
//using KafeAPI.Application.Services.Abstract;
//using Microsoft.AspNetCore.Mvc;

//namespace KafeAPI.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class OrderItemController : BaseController
//    {
//        private readonly IOrderItemServices _orderItemServices;

//        public OrderItemController(IOrderItemServices orderItemServices)
//        {
//            _orderItemServices = orderItemServices;
//        }

//        //[HttpGet]
//        //public async Task<IActionResult> GetAllOrderItems()
//        //{
//        //    var result = await _orderItemServices.GetAllOrderItems();
//        //    return CreateResponse(result);
//        //}

//        //[HttpGet("{id}")]
//        //public async Task<IActionResult> GetOrderItemById(int id) 
//        //{
//        //    var result = await _orderItemServices.GetOrderItemById(id);
//        //    return CreateResponse(result);
//        //}

//        //[HttpPost]
//        //public async Task<IActionResult> AddOrderItem(CreateOrderItemDto dto) 
//        //{
//        //    var result = await _orderItemServices.AddOrderItem(dto);
//        //    return CreateResponse(result);
//        //}

//        //[HttpPut]
//        //public async Task<IActionResult> UpdateOrderItem(UpdateOrderItemDto dto) 
//        //{
//        //    var result = await _orderItemServices.UpdateOrderItem(dto);
//        //    return CreateResponse(result);
//        //}

//        //[HttpDelete("{id}")]
//        //public async Task<IActionResult> DeleteOrderItem(int id) 
//        //{
//        //    var result = await _orderItemServices.DeleteOrderItem(id);
//        //    return CreateResponse(result);
//        //}
//    }
//}
