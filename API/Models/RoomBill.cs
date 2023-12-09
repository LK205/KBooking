namespace API.Models
{
    public class RoomBill
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public long HotelId { get; set; }
        public long RoomId { get; set; }
        public string Status { get; set; }
        public long PriceTotal { get; set; }
        public string AdditionalServices { get; set; }
        public DateTime FromBokDate { get; set; }
        public DateTime ToBokDate { get; set; }
        public DateTime CraetionTime { get; set; }
    }
}
