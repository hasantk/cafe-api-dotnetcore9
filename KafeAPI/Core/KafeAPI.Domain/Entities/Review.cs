namespace KafeAPI.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; } // 1-5 arasında bir değer validator eklenecek.
        public DateTime CreatedAt { get; set; }
    }
}
