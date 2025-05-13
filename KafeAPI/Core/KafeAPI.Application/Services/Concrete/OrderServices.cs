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
        private readonly IGenericRepository<OrderItem> _orderItemRepository;
        private readonly IValidator<CreateOrderDto> _createOrderValidator;
        private readonly IValidator<UpdateOrderDto> _updateOrderValidator;
        private readonly IMapper _mapper;


        public OrderServices(IGenericRepository<Order> orderRepository, IMapper mapper, IValidator<CreateOrderDto> createOrderValidator, IValidator<UpdateOrderDto> updateOrderValidator, IGenericRepository<OrderItem> orderItemRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _createOrderValidator = createOrderValidator;
            _updateOrderValidator = updateOrderValidator;
            _orderItemRepository = orderItemRepository;
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

        public async Task<ResponseDto<DetailOrderDto>> GetByIdOrder(int orderId)
        {
            try
            {
                var checkOrder= await _orderRepository.GetByIdAsync(orderId);
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
    }
}
