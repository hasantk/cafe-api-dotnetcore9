using AutoMapper;
using FluentValidation;
using KafeAPI.Application.Dtos.OrderDtos;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Services.Abstract;
using KafeAPI.Domain.Entities;

namespace KafeAPI.Application.Services.Concrete
{
    public class OrderServices : IOrderServices
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<MenuItem> _menuItemRepository;
        private readonly IGenericRepository<OrderItem> _orderItemRepository;
        private readonly IOrderRepository orderRepository2;
        private readonly IValidator<CreateOrderDto> _createOrderValidator;
        private readonly IValidator<UpdateOrderDto> _updateOrderValidator;
        private readonly IMapper _mapper;


        public OrderServices(IGenericRepository<Order> orderRepository, IMapper mapper, IValidator<CreateOrderDto> createOrderValidator, IValidator<UpdateOrderDto> updateOrderValidator, IGenericRepository<OrderItem> orderItemRepository, IOrderRepository orderRepository2, IGenericRepository<MenuItem> menuItemRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _createOrderValidator = createOrderValidator;
            _updateOrderValidator = updateOrderValidator;
            _orderItemRepository = orderItemRepository;
            this.orderRepository2 = orderRepository2;
            _menuItemRepository = menuItemRepository;
        }

        public async Task<ResponseDto<object>> AddOrder(CreateOrderDto dto)
        {
            try
            {
                var validate = _createOrderValidator.Validate(dto);
                if (!validate.IsValid) 
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message=string.Join(",",validate.Errors.Select(x => x.ErrorMessage)),ErrorCode= ErrorCodes.ValidationError};
                }
                var result = _mapper.Map<Order>(dto);

                result.Status = OrderStatus.Hazirlaniyor;
                result.CreatedAt = DateTime.Now;
                decimal totalPrice = 0;
                foreach (var item in result.OrderItems)
                {
                    item.MenuItem = await _menuItemRepository.GetByIdAsync(item.MenuItemId);
                    item.Price = item.MenuItem.Price * item.Quantity;
                    totalPrice += item.Price;
                }
                result.TotalPrice = totalPrice;
                await _orderRepository.AddAsync(result);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Siparişiniz Oluşturuldu." };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Hata Oluştu.", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<object>> DeleteOrder(int orderId)
        {
            try
            {
                var checkOrder = await _orderRepository.GetByIdAsync(orderId);
                if (checkOrder == null) 
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = "Sipariş Bulunamadı.", ErrorCode = ErrorCodes.NotFound };
                }
                await _orderRepository.DeleteAsync(checkOrder);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Siparişiniz Silinmiştir."};
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Hata Oluştu.", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<List<ResultOrderDto>>> GetAllOrder()
        {
            try
            {
                var order = await _orderRepository.GetAllAsync();
                var orderItem = await _orderItemRepository.GetAllAsync();
                if (order.Count == 0) 
                {
                    return new ResponseDto<List<ResultOrderDto>> { Success = false, Data = null, Message = "Sipariş Bulunamadı.", ErrorCode = ErrorCodes.NotFound };
                }
                var result = _mapper.Map<List<ResultOrderDto>>(order);
                return new ResponseDto<List<ResultOrderDto>> { Success = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultOrderDto>> { Success = false, Data = null, Message = "Bir Hata Oluştu.", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<List<ResultOrderDto>>> GetAllOrderWithDetail()
        {
            try
            {
                var order = await _orderRepository.GetAllAsync();
                var orderItem = await _orderItemRepository.GetAllAsync();
                if (order.Count == 0)
                {
                    return new ResponseDto<List<ResultOrderDto>> { Success = false, Data = null, Message = "Sipariş Bulunamadı.", ErrorCode = ErrorCodes.NotFound };
                }
                var result = _mapper.Map<List<ResultOrderDto>>(order);
                return new ResponseDto<List<ResultOrderDto>> { Success = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultOrderDto>> { Success = false, Data = null, Message = "Bir Hata Oluştu.", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<DetailOrderDto>> GetByIdOrder(int orderId)
        {
            try
            {
                var checkOrder= await orderRepository2.GetOrderByIdWithDetailAsync(orderId);
                if (checkOrder == null) 
                {
                    return new ResponseDto<DetailOrderDto> { Success = false, Data = null, Message = "Sipariş Bulunamadı.", ErrorCode = ErrorCodes.NotFound };
                }
                
                var result = _mapper.Map<DetailOrderDto>(checkOrder);
                return new ResponseDto<DetailOrderDto> { Success = true, Data = result};
            }
            catch (Exception ex)
            {
                return new ResponseDto<DetailOrderDto> { Success = false, Data = null, Message = "Bir Hata Oluştu.", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<object>> UpdateOrder(UpdateOrderDto dto)
        {
            try
            {
                var validate = await _updateOrderValidator.ValidateAsync(dto);
                if (!validate.IsValid) 
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = string.Join(",", validate.Errors.Select(x => x.ErrorMessage)), ErrorCode = ErrorCodes.ValidationError };
                }
                var order = await _orderRepository.GetByIdAsync(dto.Id);
                if (order==null)
                {
                    return new ResponseDto<Object> { Success = false, Data = null, Message = "Sipariş Bulunamadı.", ErrorCode = ErrorCodes.NotFound };
                }
                var result = _mapper.Map(dto,order);
                await _orderRepository.UpdateAsync(result);
                return new ResponseDto<object> { Success=true, Data=null,Message="Siparişiniz Güncellendi."};
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Hata Oluştu.", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<object>> UpdateOrderStatusHazir(int orderId)
        {
            try
            {
               
                var order = await _orderRepository.GetByIdAsync(orderId);
                if (order == null)
                {
                    return new ResponseDto<Object> { Success = false, Data = null, Message = "Sipariş Bulunamadı.", ErrorCode = ErrorCodes.NotFound };
                }
                order.Status = OrderStatus.Hazir;
                await _orderRepository.UpdateAsync(order);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Siparişiniz Durumu Hazır Olarak Güncellendi." };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Hata Oluştu.", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<object>> UpdateOrderStatusIptalEdildi(int orderId)
        {
            try
            {

                var order = await _orderRepository.GetByIdAsync(orderId);
                if (order == null)
                {
                    return new ResponseDto<Object> { Success = false, Data = null, Message = "Sipariş Bulunamadı.", ErrorCode = ErrorCodes.NotFound };
                }
                order.Status = OrderStatus.IptalEdildi;
                await _orderRepository.UpdateAsync(order);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Siparişiniz Durumu İptal Edildi Olarak Güncellendi." };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Hata Oluştu.", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<object>> UpdateOrderStatusTeslimEdildi(int orderId)
        {
            try
            {

                var order = await _orderRepository.GetByIdAsync(orderId);
                if (order == null)
                {
                    return new ResponseDto<Object> { Success = false, Data = null, Message = "Sipariş Bulunamadı.", ErrorCode = ErrorCodes.NotFound };
                }
                order.Status = OrderStatus.TeslimEdildi;
                await _orderRepository.UpdateAsync(order);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Siparişiniz Durumu Teslim Edildi Olarak Güncellendi." };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Hata Oluştu.", ErrorCode = ErrorCodes.Exception };
            }
        }
    }
}
