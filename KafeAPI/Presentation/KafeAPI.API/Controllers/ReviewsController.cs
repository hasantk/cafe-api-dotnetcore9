using KafeAPI.Application.Dtos.ReviewDtos;
using KafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KafeAPI.API.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewsController : BaseController
    {
        private readonly IReviewServices _reviewServices;

        public ReviewsController(IReviewServices reviewServices)
        {
            _reviewServices = reviewServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReview() 
        {
            var result = await _reviewServices.GetAllReviews();
            return CreateResponse(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdReview(int id)
        {
            var result = await _reviewServices.GetByIdReview(id);
            return CreateResponse(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateReview(CreateReviewDto dto)
        {
            var result = await _reviewServices.CreateReview(dto);
            return CreateResponse(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateReview(UpdateReviewDto dto)
        {
            var result = await _reviewServices.UpdateReview(dto);
            return CreateResponse(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var result = await _reviewServices.DeleteReview(id);
            return CreateResponse(result);
        }
    }
}
