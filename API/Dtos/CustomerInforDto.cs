namespace API.Dto
{
    public class CustomerInforDto
    {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public DateTime DayOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string CityOfResidence { get; set; }
        public string ImageBase64 { get; set; }
        public bool IsActive { get; set; }
        public long Age { get; set; }
        public string Email { get; set; }
        public long Role { get; set; }
    }
}
