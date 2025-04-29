using AutoMapper;
using KafeAPI.Application.Dtos.CategoryDtos;
using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Services.Abstract;
using KafeAPI.Domain.Entities;

namespace KafeAPI.Application.Services.Concrete
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryServices(IGenericRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task AddCategory(CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            await _categoryRepository.AddAsync(category);
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            await _categoryRepository.DeleteAsync(category);
        }

        public async Task<List<ResultCategoryDto>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var result = _mapper.Map<List<ResultCategoryDto>>(categories);
            return result;
        }

        public async Task<DetailCategoryDto> GetByIdCategory(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            var result = _mapper.Map<DetailCategoryDto>(category);
            return result;
        }

        public async Task UpdateCategory(UpdateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            await _categoryRepository.UpdateAsync(category);
        }
    }
}
