namespace API.Dto
{
    public class AccountDto
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public DateTime CreationTime { get; set; }
        public long Role { get; set; }



        //cus 
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public int Gender { get; set; }
        public DateTime DayOfBirth { get; set; }
        public string? CityOfResidence { get; set; }
        public string? ImageBase64 { get; set; }
        public bool IsActive { get; set; }




        //hotel
        public string? HotelName { get; set; }
        public string? Address_City { get; set; }
        public string? Address_District { get; set; }
        public string? Address_Ward { get; set; }
        public string? Address_Specifically { get; set; }
        public string? Avatar { get; set; }
        public string? Website { get; set; }
        public string? LocationDescription { get; set; }
        public string? GeneralDescription { get; set; }
    }
}
