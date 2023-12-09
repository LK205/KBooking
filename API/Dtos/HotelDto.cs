namespace API.Dtos
{
    public class HotelDto
    {
        public long Id { get; set; }
        public string? HotelName { get; set; }
        public string? Address_City { get; set; }
        public string? Address_District { get; set; }
        public string? Address_Ward { get; set; }
        public string Avatar { get; set; }
    }
}
