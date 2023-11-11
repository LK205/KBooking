namespace API.Dto
{
    public class HotelInforDto
    {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public string HotelName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address_City { get; set; }
        public string Address_District { get; set; }
        public string Address_Ward { get; set; }
        public string Address_Specifically { get; set; }
        public string Avatar { get; set; }
        public string Website { get; set; }
        public string LocationDescription { get; set; }
        public string GeneralDescription { get; set; }
        public string Email { get; set; }
        public long Role { get; set; }
    }
}
