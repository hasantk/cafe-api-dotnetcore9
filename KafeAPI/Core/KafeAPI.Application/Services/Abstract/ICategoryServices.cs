using KafeAPI.Application.Dtos.CategoryDtos;
using KafeAPI.Application.Dtos.ResponseDtos;

namespace KafeAPI.Application.Services.Abstract
{
    public interface ICategoryServices
    {
        Task<ResponseDto<List<ResultCategoryDto>>> GetAllCategories();
        Task<ResponseDto<DetailCategoryDto>> GetByIdCategory(int id);
        Task<ResponseDto<object>>AddCategory(CreateCategoryDto dto);
        Task<ResponseDto<Object>>UpdateCategory(UpdateCategoryDto dto);
        Task<ResponseDto<Object>>DeleteCategory(int id);
    }
}
