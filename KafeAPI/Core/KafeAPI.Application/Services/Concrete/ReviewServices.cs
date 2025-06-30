using AutoMapper;
using FluentValidation;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Dtos.ReviewDtos;
using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Services.Abstract;
using KafeAPI.Domain.Entities;

namespace KafeAPI.Application.Services.Concrete
{
    public class ReviewServices : IReviewServices
    {
        private readonly IGenericRepository<Review> _reviewRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateReviewDto> _createValidator;
        private readonly IValidator<UpdateReviewDto> _updateValidator;

        public ReviewServices(IGenericRepository<Review> reviewRepo, IMapper mapper, IValidator<CreateReviewDto> createValidator, IValidator<UpdateReviewDto> updateValidator)
        {
            _reviewRepo = reviewRepo;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<ResponseDto<object>> CreateReview(CreateReviewDto dto)
        {
            try
            {
                var validate = await _createValidator.ValidateAsync(dto);
                if (!validate.IsValid) 
                {
                    return new ResponseDto<object> { Success=false,Data=null,ErrorCode=ErrorCodes.ValidationError,Message=validate.Errors.Select(x => x.ErrorMessage).FirstOrDefault()};
                }
                var result = _mapper.Map<Review>(dto);
                await _reviewRepo.AddAsync(result);
                return new ResponseDto<object> { Success=true, Data=null, Message="Yorum Oluşturuldu."};
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Hata Oluştu.", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<object>> DeleteReview(int id)
        {
            try
            {
                var review = await _reviewRepo.GetByIdAsync(id);
                if (review == null) 
                    return new ResponseDto<object> { Success = false, Data = null, ErrorCode = ErrorCodes.NotFound, Message = "Yorum Bulunamadı." };
                await _reviewRepo.DeleteAsync(review);
                return new ResponseDto<object> { Success = true, Data = null, Message = "Yorum Silindi." };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Hata Oluştu.", ErrorCode = ErrorCodes.Exception };
            }
        }

        public async Task<ResponseDto<List<ResultReviewDto>>> GetAllReviews()
        {
            try
            {
                var reviews = await _reviewRepo.GetAllAsync();
                var result = _mapper.Map<List<ResultReviewDto>>(reviews);
                return new ResponseDto<List<ResultReviewDto>> { Success = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultReviewDto>> { Success = false , Data = null , Message="Bir Hata Oluştu.", ErrorCode=ErrorCodes.Exception};
            }
        }

        public async Task<ResponseDto<DetailReviewDto>> GetByIdReview(int id)
        {
            try
            {
                var review = await _reviewRepo.GetByIdAsync(id);
                if (review == null)
                    return new ResponseDto<DetailReviewDto> { Success = false, Data = null, ErrorCode = ErrorCodes.NotFound, Message = "Yorum Bulunamadı." };
                var result = _mapper.Map<DetailReviewDto>(review);
                return new ResponseDto<DetailReviewDto> { Success = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ResponseDto<DetailReviewDto> { Success = false, Data = null, Message = "Bir Hata Oluştu.", ErrorCode=ErrorCodes.Exception};
            }
        }

        public async Task<ResponseDto<object>> UpdateReview(UpdateReviewDto dto)
        {
            try
            {
                var validate = await _updateValidator.ValidateAsync(dto);
                if (!validate.IsValid) 
                    return new ResponseDto<object> { Success = false, Data = null, ErrorCode = ErrorCodes.ValidationError, Message = validate.Errors.Select(x => x.ErrorMessage).FirstOrDefault() };
                var review = await _reviewRepo.GetByIdAsync(dto.Id);
                if (review == null) 
                {
                    return new ResponseDto<object> { Success = false, Data = null, ErrorCode = ErrorCodes.NotFound, Message = "Yorum Bulunamadı." };
                }
                var result = _mapper.Map(dto, review);
                await _reviewRepo.UpdateAsync(result);
                return new ResponseDto<object> { Success=true, Data=null, Message="Yorum Güncellendi."};
            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Hata Oluştu.", ErrorCode = ErrorCodes.Exception };
            }
        }
    }
}
