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

        public ReviewServices(IGenericRepository<Review> reviewRepo, IMapper mapper, IValidator<CreateReviewDto> createValidator)
        {
            _reviewRepo = reviewRepo;
            _mapper = mapper;
            _createValidator = createValidator;
        }

        public async Task<ResponseDto<object>> CreateReview(CreateReviewDto dto)
        {
            try
            {

            }
            catch (Exception ex)
            {
                return new ResponseDto<object> { Success = false, Data = null, Message = "Bir Hata Oluştu.", ErrorCode = ErrorCodes.Exception };
            }
        }

        public Task<ResponseDto<object>> DeleteReview(int id)
        {
            throw new NotImplementedException();
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

        public Task<ResponseDto<object>> UpdateReview(UpdateReviewDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
