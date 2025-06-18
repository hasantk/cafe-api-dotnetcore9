namespace KafeAPI.Application.Dtos.CafeInfoDtos
{
    public class UpdateCafeInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? InstagramUrl { get; set; }
        public string? YoutubeUrl { get; set; }
        public string OpeningHourse { get; set; }
        public string ImageUrl { get; set; }
    }
}
