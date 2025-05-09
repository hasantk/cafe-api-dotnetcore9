using AutoMapper;
using FluentValidation;
using KafeAPI.Application.Dtos.CategoryDtos;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Services.Abstract;
using KafeAPI.Domain.Entities;

namespace KafeAPI.Application.Services.Concrete
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCategoryDto> _createCategoryValidator;
        private readonly IValidator<UpdateCategoryDto> _updateCategoryValidator;

        public CategoryServices(IGenericRepository<Category> categoryRepository, IMapper mapper, IValidator<CreateCategoryDto> createCategoryValidator, IValidator<UpdateCategoryDto> updateCategoryValidator)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _createCategoryValidator = createCategoryValidator;
            _updateCategoryValidator = updateCategoryValidator;
        }

        public async Task<ResponseDto<Object>>AddCategory(CreateCategoryDto dto)
        {
            try
            {
                var validate = await _createCategoryValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<Object> { Success = false, Data = null,
                    Message = string.Join(" , ", validate.Errors.Select(x => x.ErrorMessage)),
                    ErrorCode = ErrorCodes.ValidationError};
                }
                var category = _mapper.Map<Category>(dto);
                await _categoryRepository.AddAsync(category);
                return new ResponseDto<Object> { Success = true, Data= null, Message="Kategori Oluşturuldu." };
            }
            catch (Exception ex)
            {
                return new ResponseDto<Object> { Success = false, Data = null, Message = "Bir Hata Oluştu.", ErrorCode = ErrorCodes.Exception };
            }
            
        }

        public async Task<ResponseDto<Object>> DeleteCategory(int id)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    return new ResponseDto<Object>
                    {
                        Success = false,
                        Data = null,
                        Message = "Kategori Bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                await _categoryRepository.DeleteAsync(category);
                return new ResponseDto<object> { Success = true, Data = null, Message="Kategori Silindi." };
            }
            catch (Exception ex)
            {

                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir Hata Oluştu.",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<List<ResultCategoryDto>>> GetAllCategories()
        {
            try
            {
                var categories = await _categoryRepository.GetAllAsync();
                if (categories.Count == 0)
                {
                    return new ResponseDto<List<ResultCategoryDto>>
                    {
                        Success = false,
                        Message = "Kategori Bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map<List<ResultCategoryDto>>(categories);
                return new ResponseDto<List<ResultCategoryDto>> { Success = true, Data = result };
            }
            catch (Exception ex)
            {

                return new ResponseDto<List<ResultCategoryDto>>
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCodes.Exception
                };
            }
           
        }

        public async Task<ResponseDto<DetailCategoryDto>> GetByIdCategory(int id)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);
                if(category == null) 
                {
                    return new ResponseDto<DetailCategoryDto> { Success = false, Message = "Kategori Bulunamadı.", ErrorCode = ErrorCodes.NotFound };
                }
                var result = _mapper.Map<DetailCategoryDto>(category);
                return new ResponseDto<DetailCategoryDto> { Success = true, Data = result };
            }
            catch (Exception ex)
            {

                return new ResponseDto<DetailCategoryDto> { Success = false, Message ="Bir Hata Oluştu.", ErrorCode = ErrorCodes.Exception };
            }
           
        }

        public async Task<ResponseDto<Object>> UpdateCategory(UpdateCategoryDto dto)
        {
            try
            {
                var validate = await _updateCategoryValidator.ValidateAsync(dto);
                var categorydb = await _categoryRepository.GetByIdAsync(dto.Id);
                if (!validate.IsValid)
                {
                        return new ResponseDto<Object>
                        {
                            Success = false,
                            Data = null,
                            Message = string.Join(" , ", validate.Errors.Select(x => x.ErrorMessage)),
                            ErrorCode = ErrorCodes.ValidationError
                        };
                }
                if (categorydb == null)
                {
                    return new ResponseDto<object> { Success = false, Message = "Kategori Bulunamadı.", ErrorCode = ErrorCodes.NotFound };
                }
                var category = _mapper.Map(dto,categorydb);
                await _categoryRepository.UpdateAsync(category);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Kategori Güncellendi." };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Hata Oluştu.", ErrorCode = ErrorCodes.Exception };
            }
            
        }
    }
}
