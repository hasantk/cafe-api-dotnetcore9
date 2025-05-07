using AutoMapper;
using FluentValidation;
using KafeAPI.Application.Dtos.MenuItemDtos;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Services.Abstract;
using KafeAPI.Domain.Entities;

namespace KafeAPI.Application.Services.Concrete
{
    public class MenuItemServices : IMenuItemServices
    {
        private readonly IGenericRepository<MenuItem> _menuItemRepository;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateMenuItemDto> _createMenuItemValidator;
        private readonly IValidator<UpdateMenuItemDto> _updateMenuItemValidator;
        public MenuItemServices(IGenericRepository<MenuItem> menuItemRepository, IMapper mapper, IValidator<CreateMenuItemDto> createMenuItemValidator, IGenericRepository<Category> categoryRepository)
        {
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
            _createMenuItemValidator = createMenuItemValidator;
            _categoryRepository = categoryRepository;
        }

        public async Task<ResponseDto<Object>> AddMenuItem(CreateMenuItemDto dto)
        {
            try
            {
                var validate = await _createMenuItemValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object> { Success = false, Data=null, Message= string.Join(",",validate.Errors.Select(x => x.ErrorMessage)), ErrorCodes= ErrorCodes.ValidationError };
                }
                var checkcategory = await _categoryRepository.GetByIdAsync(dto.CategoryId);
                if (checkcategory == null) 
                {
                    return new ResponseDto<object> { Success = false, Data = dto, Message = "Eklemek İstediğiniz Kategori Bulunamadı.", ErrorCodes = ErrorCodes.NotFound };
                }
                var menuItem = _mapper.Map<MenuItem>(dto);
                await _menuItemRepository.AddAsync(menuItem);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Menu Item Başarılı Bir Şekilde Eklendi." };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir Hata Oluştu.",
                    ErrorCodes = ErrorCodes.Exception
                };
            }
            
        }

        public async Task<ResponseDto<Object>> DeleteMenuItem(int id)
        {
            try
            {
                var menuItem = await _menuItemRepository.GetByIdAsync(id);
                if (menuItem == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = "Menu Item Bulunamadı.",
                        ErrorCodes = ErrorCodes.NotFound
                    };
                }
                await _menuItemRepository.DeleteAsync(menuItem);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Menu Item Başarılı Bir Şekilde Silindi." };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Hata Oluştu.", ErrorCodes = ErrorCodes.Exception };
            }
            
        }

        public async Task<ResponseDto<List<ResultMenuItemDto>>> GetAllMenuItems()
        {
            try
            {
                var menuItems = await _menuItemRepository.GetAllAsync();
                var category = await _categoryRepository.GetAllAsync();
                if (menuItems.Count == 0)
                {
                    return new ResponseDto<List<ResultMenuItemDto>>
                    {
                        Success = false,
                        Data = null,
                        Message = "Menu Items Bulunamadı.",
                        ErrorCodes = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map<List<ResultMenuItemDto>>(menuItems);
                return new ResponseDto<List<ResultMenuItemDto>> { Success = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultMenuItemDto>> { Success = false, Data=null, Message ="Bir Hata Oluştu.",
                ErrorCodes = ErrorCodes.Exception};
            }
        }

        public async Task<ResponseDto<DetailMenuItemDto>> GetByIdMenuItem(int id)
        {
            var menuItem = await _menuItemRepository.GetByIdAsync(id);
            var category = await _categoryRepository.GetByIdAsync(menuItem.CategoryId);
            if (menuItem == null)
            {
                return new ResponseDto<DetailMenuItemDto>
                {
                    Success = false,
                    Data = null,
                    Message = "Menu Item Bulunamadı.",
                    ErrorCodes = ErrorCodes.NotFound
                };
            }
            var result = _mapper.Map<DetailMenuItemDto>(menuItem);
            return new ResponseDto<DetailMenuItemDto> { Success=true,Data=result};
        }

        public async Task<ResponseDto<Object>> UpdateMenuItem(UpdateMenuItemDto dto)
        {
            try
            {
                var validate = await _updateMenuItemValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object> { Success = false, Data = null, Message = string.Join(",", validate.Errors.Select(x => x.ErrorMessage)), ErrorCodes = ErrorCodes.ValidationError };
                }
                var menuItem = await _menuItemRepository.GetByIdAsync(dto.Id);
                if (menuItem == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = "Menu Item Bulunamadı.",
                        ErrorCodes = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map(dto, menuItem);
                await _menuItemRepository.UpdateAsync(result);
                return new ResponseDto<object> { Success = true, Data =null,Message="Menu Item Başarılı Bir Şekilde Oluşturuldu."};
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Hata Oluştu", ErrorCodes = ErrorCodes.Exception };
            }
            
        }
    }
}
