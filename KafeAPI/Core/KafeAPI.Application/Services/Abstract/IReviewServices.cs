using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Dtos.ReviewDtos;

namespace KafeAPI.Application.Services.Abstract
{
    public interface IReviewServices
    {
        Task<ResponseDto<List<ResultReviewDto>>> GetAllReviews();
        Task<ResponseDto<DetailReviewDto>> GetByIdReview(int id);
        Task<ResponseDto<Object>> CreateReview(CreateReviewDto dto);
        Task<ResponseDto<Object>> UpdateReview(UpdateReviewDto dto);
        Task<ResponseDto<Object>> DeleteReview(int id);
    }
}
