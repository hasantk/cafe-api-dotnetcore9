﻿namespace KafeAPI.Application.Dtos.ReviewDtos
{
    public class UpdateReviewDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
