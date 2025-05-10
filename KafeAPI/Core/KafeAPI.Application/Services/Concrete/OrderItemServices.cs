using AutoMapper;
using FluentValidation;
using KafeAPI.Application.Dtos.OrderItemDtos;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Services.Abstract;
using KafeAPI.Domain.Entities;

namespace KafeAPI.Application.Services.Concrete
{
    public class OrderItemServices : IOrderItemServices
    {
        private readonly IGenericRepository<OrderItem> _orderItemRepository;
        private readonly IValidator<CreateOrderItemDto> _createOrderItemValidator;
        private readonly IMapper _mapper;

        public OrderItemServices(IGenericRepository<OrderItem> orderItemRepository, IMapper mapper, IValidator<CreateOrderItemDto> createOrderItemValidator)
        {
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
            _createOrderItemValidator = createOrderItemValidator;
        }

        public async Task<ResponseDto<object>> AddOrderItem(CreateOrderItemDto dto)
        {
            try
            {
                var validate = await _createOrderItemValidator.ValidateAsync(dto);
                if (!validate.IsValid) 
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = string.Join(",", validate.Errors.Select(x => x.ErrorMessage)), ErrorCode = ErrorCodes.ValidationError };
                }
                var result = _mapper.Map<OrderItem>(dto);
                await _orderItemRepository.AddAsync(result);
                return new ResponseDto<object> { Success=true, Data=null, Message="Sipariş İteminiz Oluşturuldu."};
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Hata Oluştu.", ErrorCode = ErrorCodes.Exception};
            }
        }

        public Task<ResponseDto<object>> DeleteOrderItem(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto<List<ResultOrderItemDto>>> GetAllOrderItems()
        {
            try
            {
                var db = await _orderItemRepository.GetAllAsync();
                if (db.Count == 0)
                {
                    return new ResponseDto<List<ResultOrderItemDto>>{ Success=false, Data=null, Message="Sipariş Bulunamadı.", ErrorCode=ErrorCodes.NotFound};
                }
                var result = _mapper.Map<List<ResultOrderItemDto>>(db);
                return new ResponseDto<List<ResultOrderItemDto>> { Success=true, Data=result};
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultOrderItemDto>> { Success=false, Data = null, Message="Bir Hata Oluştu.", ErrorCode = ErrorCodes.Exception};
            }
        }

        public async Task<ResponseDto<DetailOrderItemDto>> GetOrderItemById(int id)
        {
            try
            {
                var db = await _orderItemRepository.GetByIdAsync(id);
                if (db == null)
                {
                    return new ResponseDto<DetailOrderItemDto> { Success = false, Data = null, Message = "Sipariş İtemi Bulunamadı.", ErrorCode = ErrorCodes.NotFound };
                }
                var result = _mapper.Map<DetailOrderItemDto>(db);
                return new ResponseDto<DetailOrderItemDto> { Success = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ResponseDto<DetailOrderItemDto> { Success = false, Data = null, Message = "Bir Hata Oluştu.", ErrorCode = ErrorCodes.Exception };
            }
        }

        public Task<ResponseDto<object>> UpdateOrderItem(UpdateOrderItemDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
